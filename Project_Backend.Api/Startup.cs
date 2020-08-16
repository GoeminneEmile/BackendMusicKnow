using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project_Backend.Models.Data;
using Project_Backend.Models.Models;
using Project_Backend.Models.Repositories;

namespace Project_Backend.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<IScoreRepository, ScoreRepository>();
            services.AddControllers();
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<MusicKnowDbContext>();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "Quiz_API", Version = "v1.0" }); });
            services.AddDbContext<MusicKnowDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc(options => { options.EnableEndpointRouting = false; options.RespectBrowserAcceptHeader = true; options.ReturnHttpNotAcceptable = true; }).AddNewtonsoftJson(options => // options.Filters.Add(new ConsumesAttribute("application/json"));
            {
                //camelcase de kolom namen
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //circulaire referenties verhinderen door navigatie props
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
               options =>
               {
                   options.Cookie.SameSite = SameSiteMode.None;
                   options.Events =
                     new CookieAuthenticationEvents()
                     {
                         OnRedirectToLogin = (ctx) =>
                         {
                             if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200) //redirect naar loginURL is 200
                             {
                                 //doe geen redirect naar een loginpagina bij een api call 
                                 //maar geef een 401 (unauthorized) als authenticatie faalt 
                                 ctx.Response.StatusCode = 401;
                                 ctx.Response.WriteAsync("{\"error\": " + ctx.Response.StatusCode + " Geen toegang}");
                             }

                             return Task.CompletedTask;
                         }
                     };
               });
            services.AddCors();
            if (!_env.IsDevelopment())
            {
                services.AddHttpsRedirection(options =>
                {
                    //default: 307 redirect
                    // options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                    options.HttpsPort = 443;
                });

                services.AddHsts(options =>
                {
                    options.MaxAge = TimeSpan.FromDays(40); //default 30
                });
        }
    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //In productie 
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(cfg => { cfg.AllowAnyHeader().AllowAnyMethod(); });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(); //enable swagger 
            app.UseSwaggerUI(c => {
                c.RoutePrefix = "swagger"; //path naar de UI pagina: /swagger/index.html    
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "MuiscKnow_API v1.0");
            });
        }
    }
}

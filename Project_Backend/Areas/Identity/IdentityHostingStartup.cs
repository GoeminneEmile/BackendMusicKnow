using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project_Backend.Models.Models;
using Project_Backend.Models.Data;
using Project_Backend.Models.Models;

[assembly: HostingStartup(typeof(WickedQuiz.Web.Areas.Identity.IdentityHostingStartup))]
namespace WickedQuiz.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<MusicKnowDbContext>();
            });
        }
    }
}
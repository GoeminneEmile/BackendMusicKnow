using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Backend.Api.ApiModels;
using Project_Backend.Models.Models;

namespace Project_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInMgr;

        public AuthController(SignInManager<AppUser> signInMgr)
        {
            _signInMgr = signInMgr;
        }

        // GET: api/Auth
        [AllowAnonymous]
        [HttpPost]
        [Route("login")] //vult de controller basis route aan [AllowAnonymous] 
        public async Task<IActionResult> Login([FromBody] Login_DTO loginDTO)
        { //LoginViewModel met (Required) UserName en Password aanbrengen. 
            var returnMessage = "";
            if (!ModelState.IsValid)
                return BadRequest("Onvolledige gegevens.");
            try
            { //geen persistence, geen lockout -> via false, false 
                var result = await _signInMgr.PasswordSignInAsync(loginDTO.UserName, loginDTO.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok("Welkom " + loginDTO.UserName);
                }
                throw new Exception("User of paswoord niet gevonden."); //zo algemeen mogelijk response. Vertelt niet dat het pwd niet juist is. 
            }
            catch (Exception ex)
            {
                returnMessage = $"Foutief of ongeldig request: {ex.Message}"; ModelState.AddModelError("", returnMessage);
            }
            return BadRequest(returnMessage); //zo weinig mogelijk  info 
        }
    }
}

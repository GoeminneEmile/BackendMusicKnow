using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Project_Backend.Models.Models;

namespace Project_Backend.Models.Data
{
    public class MusicKnowDbContextExtensions
    {
        public async static Task SeedRoles(RoleManager<IdentityRole> RoleMgr)
        {
            IdentityResult roleResult;
            string[] roleNames = { "Administrator", "User" };
            foreach (var roleName in roleNames)
            { //verhinderen dat continue dezelfde rollen worden toegvoegd. 
                var roleExist = await RoleMgr.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleMgr.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public async static Task SeedUsers(UserManager<AppUser> userMgr)
        {
            //1. Admin aanmaken ---------------------------------------------------
            if (await userMgr.FindByNameAsync("Docent@MCT") == null)  //controleer de UserName
            {
                var user = new AppUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "Docent@MCT",
                    FirstName = "Docent",
                    Email = "Docent@MCT",
                };

                var userResult = await userMgr.CreateAsync(user, "Docent@1");
                var roleResult = await userMgr.AddToRoleAsync(user, "Administrator");
                // var claimResult = await userMgr.AddClaimAsync(user, new Claim("DocentWeb", "True"));

                if (!userResult.Succeeded || !roleResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }
            }

            //1. User aanmaken ---------------------------------------------------
            if (await userMgr.FindByNameAsync("QuizUser@MCT") == null)  //controleer de UserName
            {
                var user = new AppUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "Student@MCT",
                    FirstName = "Student",
                    Email = "Student@MCT",
                };

                var userResult = await userMgr.CreateAsync(user, "Student@1");
                var roleResult = await userMgr.AddToRoleAsync(user, "User");

                if (!userResult.Succeeded || !roleResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }
            }
        }
    }
}

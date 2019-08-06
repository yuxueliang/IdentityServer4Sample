using IdentityServer4Mvc.Data;
using IdentityServer4Mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4Mvc
{
    public class ApplicationDbContextSeed
    {
        private UserManager<ApplicationUser> _userManager;
        public async Task SeedAsync(ApplicationDbContext contest, IServiceProvider serviceProvider)
        {
            if (!contest.Users.Any())
            {
                _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var defultUser = new ApplicationUser
                {
                    UserName = "yxl",
                    Email = "1027206433@qq.com",
                    NormalizedUserName = "yuxueliang"
                };
                var resilt = await _userManager.CreateAsync(defultUser, "Password$123");
                if (!resilt.Succeeded)
                {
                    throw new Exception("初始化用户信息失败!");
                }
            }
        }
    }
}

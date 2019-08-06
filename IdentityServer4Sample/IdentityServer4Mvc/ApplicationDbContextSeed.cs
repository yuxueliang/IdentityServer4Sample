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

        private RoleManager<ApplicationUserRole> _roleManager;

        public async Task SeedAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
        {

            if (!context.Roles.Any())
            {
                _roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationUserRole>>();

                var role = new ApplicationUserRole()
                {
                    Name = "Administrators",
                    NormalizedName = "Administrators"
                };

                var result = await _roleManager.CreateAsync(role);


                if (!result.Succeeded)
                {
                    throw new Exception("初始化角色信息失败!");
                }
            }


            if (!context.Users.Any())
            {
                _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var defultUser = new ApplicationUser
                {
                    UserName = "yuxueliang",
                    Email = "1027206433@qq.com",
                    NormalizedUserName = "yxl",
                    Avatar = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1565001673221&di=5bdebf57dd6c781a0e47652207b171da&imgtype=0&src=http%3A%2F%2Fpngc.mypng.cn%2F1928%2Ficon_lovely.png.1.png"
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

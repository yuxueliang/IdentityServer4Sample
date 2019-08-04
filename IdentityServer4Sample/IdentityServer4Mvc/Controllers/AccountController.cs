using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using IdentityServer4Mvc.Models;
using IdentityServer4.Test;

namespace IdentityServer4Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly TestUserStore _users;

        public AccountController(TestUserStore users)
        {
            _users = users;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model,string returnUrl)
        {
            var user = _users.FindByUsername(model.UserName);
            if (user==null)
            {
                throw new Exception("用户名不存在");
            }
            else
            {
                if (_users.ValidateCredentials(model.UserName, model.Password))
                {

                    var props = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))
                    };
                    await Microsoft.AspNetCore.Http.AuthenticationManagerExtensions.SignInAsync(HttpContext, user.SubjectId, user.Username, props);
                    return Redirect(returnUrl??"/");
                }
                else
                {
                    throw new Exception("密码错误");
                }
            }
         
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/");
        }
    }
}
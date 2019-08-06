using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using IdentityServer4Mvc.Models;
using IdentityServer4.Test;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer4Mvc.Controllers
{
    public class AccountController : Controller
    {
       // private readonly TestUserStore _users;

        private UserManager<ApplicationUser> _userManager;

        private SignInManager<ApplicationUser> _signInManager;

        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        

        public AccountController(//TestUserStore users, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService identityServerInteractionService)
        {
           // _users = users;
            _identityServerInteractionService = identityServerInteractionService;

            _userManager = userManager;
            _signInManager = signInManager;
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
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            // var user = _users.FindByUsername(model.Email);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                throw new Exception("用户名不存在");
            }
            else
            {
                //if (_users.ValidateCredentials(model.Email, model.Password))
                //{

                //    var props = new AuthenticationProperties
                //    {
                //        IsPersistent = true,
                //        ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))
                //    };
                //    await Microsoft.AspNetCore.Http.AuthenticationManagerExtensions.SignInAsync(HttpContext, user.SubjectId, user.Username, props);
                //    return Redirect(returnUrl??"/");
                //}
                //else
                //{
                //    throw new Exception("密码错误");
                //}

                if (await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    AuthenticationProperties props = null;

                    if (model.RememberMe)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))
                        };
                    }
                    
                    await _signInManager.SignInAsync(user, props);

                    if (_identityServerInteractionService.IsValidReturnUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return Redirect("/");

                }
                else
                {
                    throw new Exception("密码错误");
                }


            }

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Redirect("/");
        }
    }
}
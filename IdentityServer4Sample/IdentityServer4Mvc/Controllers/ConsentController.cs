using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4;
using IdentityServer4.Stores;
using IdentityServer4.Services;
using IdentityServer4.Models;
using IdentityServer4Mvc.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityServer4Mvc.Controllers
{
    public class ConsentController : Controller
    {

        private readonly ConsentService _consentService;

      
        public ConsentController(ConsentService consentService)
        {
            _consentService = consentService;
        }

      
       
        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var model = await _consentService.BuildConsentViewModelAsync(returnUrl);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(InputConsentViewModel model)
        {
            var result = await _consentService.ProcessConsentAsync(model);
            if (result.IsRedirect)
            {
                return Redirect(result.RedirectUrl);
            }
            return View(result.ConsentViewModel);
        }
    }
}

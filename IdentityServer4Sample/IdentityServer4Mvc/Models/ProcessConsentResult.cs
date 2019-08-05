using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4Mvc.Models
{
    public class ProcessConsentResult
    {
        public string RedirectUrl { get; set; }

        public bool IsRedirect => RedirectUrl != null;

        public ConsentViewModel ConsentViewModel { get; set; }
    }
}

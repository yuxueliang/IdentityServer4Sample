using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Test;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;

namespace IdentityServer4Mvc
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1","Api Application")
            };
            
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                     ClientId="mvc",
                     AllowedGrantTypes=GrantTypes.Implicit,
                     ClientSecrets = { new Secret("secret".Sha256()) },
                     AllowedScopes={
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                    },
                     RequireConsent=false,
                     RedirectUris={ "http://localhost:5001/signin-oidc"},//固定地址，不能修改
                     PostLogoutRedirectUris={"http://localhost:5001/signout-callback-oidc" }//固定地址，不能修改
                },
               
            };

        }

        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                     SubjectId="1",
                     Username = "yxl",
                     Password ="123456",
                    
                }
            };
        }
    }
}

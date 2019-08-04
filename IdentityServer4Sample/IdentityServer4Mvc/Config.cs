using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Test;
using System.Security.Claims;
using IdentityModel;

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
                     AllowedScopes={ "api1" }//和ApiResource 中的保持一致
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

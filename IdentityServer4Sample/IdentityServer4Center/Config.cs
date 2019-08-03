using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Test;

namespace IdentityServer4Center
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api","My Api")
            };
            
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                     ClientId="client",
                     AllowedGrantTypes=GrantTypes.ClientCredentials,
                     ClientSecrets = { new Secret("secret".Sha256()) },
                     AllowedScopes={ "api" }//和ApiResource 中的保持一致
                },
                new Client
                {
                     ClientId="pwdClient",
                     AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                     ClientSecrets = { new Secret("secret".Sha256()) },
                     RequireClientSecret=false,
                     AllowedScopes={ "api" }//和ApiResource 中的保持一致
                }
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
                     Password ="123456"
                }
            };
        }
    }
}

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
                     ClientName = "Mvc Client",
                     ClientUri = "http://localhost:5001",
                     LogoUri = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1565001673221&di=5bdebf57dd6c781a0e47652207b171da&imgtype=0&src=http%3A%2F%2Fpngc.mypng.cn%2F1928%2Ficon_lovely.png.1.png",
                     AllowRememberConsent = true,

                     AllowedGrantTypes=GrantTypes.Implicit,
                     ClientSecrets = { new Secret("secret".Sha256()) },
                     AllowedScopes={
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess
                    },
                     RequireConsent=true,
                     RedirectUris={ "http://localhost:5001/signin-oidc"},//固定地址，不能修改
                     PostLogoutRedirectUris={"http://localhost:5001/signout-callback-oidc" },//固定地址，不能修改
                    AllowAccessTokensViaBrowser = true ,// can return access_token to this client
                   AllowOfflineAccess=true,
                   AlwaysIncludeUserClaimsInIdToken=true
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
                      Claims = {
                         new Claim("gender","男"),
                         new Claim("website","httt://www.baidu.com")
                    }
                    
                }
            };
        }
    }
}

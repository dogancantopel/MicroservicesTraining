// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
             new ApiResource("resource_catalog"){Scopes={ "catalogAPI_FullPermission" } },
             new ApiResource("resource_photoStock"){Scopes={ "photoStockAPI_FullPermission" } },
             new ApiResource("resource_basket"){Scopes={ "basketAPI_FullPermission" } },
             new ApiResource("resource_discount"){Scopes={ "discountAPI_FullPermission" } },
             new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        /// <summary>
        /// Token içinde gözükecek bilgiler
        /// </summary>
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.OpenId(),
                       new IdentityResources.Email(),
                       new IdentityResources.Profile(),
                       new IdentityResource("roles","User Role",new List<string>{"role"})
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalogAPI_FullPermission","Full Permission For Catalog API"),
                new ApiScope("photoStockAPI_FullPermission","Full Permission For Photo Stock API"),
                 new ApiScope("basketAPI_FullPermission","Full Permission For Basket API"),
                  new ApiScope("discountAPI_FullPermission","Full Permission For Discount API"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId="WebMvcClient",
                    ClientName="ASP.Net.Core MVC",
                    ClientSecrets={new Secret ("secret".Sha256()) },
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AllowedScopes={
                        "catalogAPI_FullPermission",
                        "photoStockAPI_FullPermission",
                        IdentityServerConstants.LocalApi.ScopeName
                    }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId="WebMvcClientForUser",
                    ClientName="ASP.Net.Core MVC",
                    ClientSecrets={new Secret ("secret".Sha256()) },
                    AllowOfflineAccess=true,
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    AllowedScopes={
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,//access token bitince refresh tokenla yeniden login etmeden token alınmasını sağlar
                        
                        IdentityServerConstants.LocalApi.ScopeName,
                        "roles",
                        "basketAPI_FullPermission",
                         "discountAPI_FullPermission"
                    },
                    AccessTokenLifetime=1*60*60,//1 saat accesstoken
                    RefreshTokenExpiration=TokenExpiration.Absolute,//refresh token kalıcı değil
                    AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds, //60 günlük refresh token
                    RefreshTokenUsage=TokenUsage.ReUse//refresh token tekrar kullanılabilir
                },
            };
    }
}
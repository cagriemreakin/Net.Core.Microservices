// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace Course.IdentityServer {
    public static class Config {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] {
         new ApiResource("resource_catalog"){Scopes = { "catolog_fullpermission" } },
         new ApiResource("resource_photo_storage"){Scopes = { "photo_storage_fullpermission" } },
         new ApiResource("resource_basket"){Scopes = { "basket_fullpermission" } },
         new ApiResource("resource_discount"){Scopes = { "discount_fullpermission" } },
         new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource(){
                           Name="roles",
                           DisplayName="Roles",
                           Description="User Roles",
                           UserClaims= new []{"role"}
                       }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catolog_fullpermission","Catalog Api Permissions"),
                new ApiScope("photo_storage_fullpermission","Photo Storage Api Permissions"),
                new ApiScope("basket_fullpermission","Basket Api Permissions"),
                new ApiScope("discount_fullpermission","Discount Api Permissions"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]{
                new Client{
                    ClientName="Web",
                    ClientId="Web-Client",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    AllowedScopes={ "catolog_fullpermission", "photo_storage_fullpermission",IdentityServerConstants.LocalApi.ScopeName }
                },
                  new Client{
                    ClientName="Web",
                    ClientId="Web-Client-User",
                    AllowOfflineAccess=true,
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ResourceOwnerPassword,
                    AllowedScopes={
                         IdentityServerConstants.StandardScopes.Email,
                         IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                         IdentityServerConstants.StandardScopes.OfflineAccess,
                         IdentityServerConstants.LocalApi.ScopeName,
                         "roles",
                         "basket_fullpermission",
                         "discount_fullpermission"
                    },
                    AccessTokenLifetime=3600,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                  }
            };
    }
}
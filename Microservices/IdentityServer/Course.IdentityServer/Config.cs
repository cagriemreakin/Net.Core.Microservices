﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace Course.IdentityServer {
    public static class Config {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] {
         new ApiResource("catalog"){Scopes = { "catolog_fullpermission" } },
         new ApiResource("photo_stock"){Scopes = { "photo_stock_fullpermission" } },
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
                new ApiScope("photo_stock_fullpermission","Photo Stock Api Permissions"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]{
                new Client{
                    ClientName="Web",
                    ClientId="Web-Client",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    AllowedScopes={ "catolog_fullpermission", "photo_stock_fullpermission",IdentityServerConstants.LocalApi.ScopeName }
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
                         "roles"
                    },
                    AccessTokenLifetime=3600,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                  }
            };
    }
}
// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;
using Microsoft.Extensions.Configuration;

namespace Chess.Identity
{
    public class Config
    {
        private readonly IConfiguration _configuration;
        public Config(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public IEnumerable<ApiScope> ApiScopes =>
            new []
            {
                new ApiScope("ChessScope")
            };

        public IEnumerable<Client> Clients =>
            new []
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientName = "Chess Engine Client",
                    ClientId = "chess-engine-client",
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "ChessScope" }
                },
                new Client
                {
                    ClientName = "Statistics Api Client",
                    ClientId = "chess-engine-statistics",
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = _configuration.GetValue<ICollection<string>>("Oidc:StatisticsApi:RedirectUris"),
                    PostLogoutRedirectUris = _configuration.GetValue<ICollection<string>>("Oidc:StatisticsApi:PostLogoutRedirectUris"),

                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile }
                }
            };
    }
}
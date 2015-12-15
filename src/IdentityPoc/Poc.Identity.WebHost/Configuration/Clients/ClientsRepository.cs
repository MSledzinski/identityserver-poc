namespace Poc.Identity.WebHost.Configuration.Clients
{
    using System.Collections.Generic;

    using Thinktecture.IdentityServer.Core.Models;

    public static class ClientsRepository
    {
        public static IEnumerable<Client> GetAll()
        {
            var resourceClientSecret = new ClientSecret("secret".Sha256());

            
            return new List<Client>
                       {
                           new Client
                               {
                                   ClientName = "Some MVC app to test",
                                   ClientId = "implicitclient",
                                   ClientSecrets = new List<ClientSecret>
                                                       {
                                                           new ClientSecret("super_secret".Sha256())
                                                       },
                                   Flow = Flows.Implicit,
                                   AccessTokenType = AccessTokenType.Jwt,

                                   RedirectUris = new List<string>
                                                      {
                                                          "http://localhost:2671/"
                                                      },

                                   PostLogoutRedirectUris = new List<string>()
                                                                {
                                                                     "http://localhost:2671/"
                                                                },
                                   RequireConsent = false
                               },
                               new Client
                                   {

                                       ClientName = "Web Mgmt UI",
                                       Enabled = true,
                                       ClientId = "identityWebUi",
                                       ClientSecrets = new List<ClientSecret>(new []
                                                                                  {
                                                                                      resourceClientSecret
                                                                                  }),
                                        Flow = Flows.ResourceOwner,
                                        AlwaysSendClientClaims = true,
                                        AccessTokenType = AccessTokenType.Jwt,
                                          IdentityTokenLifetime = 3000,
                                        AccessTokenLifetime = 3600,
                                        AuthorizationCodeLifetime = 300

                                   }

                       };
        }
    }
}
namespace Poc.Identity.WebHost.Configuration.Scopes
{
    using System.Collections.Generic;

    using Thinktecture.IdentityServer.Core.Models;

    public class ScopesRepository
    {
        public static IEnumerable<Scope> GetAll()
        {
            var scopes = new List<Scope>
                       {
                           new Scope
                               {
                                   Name = "roles",
                                   Type = ScopeType.Identity,
                                   Enabled = true,
                                   Claims = new List<ScopeClaim>()
                                                {
                                                    new ScopeClaim("role")
                                                }
                               },
                            new Scope
                               {
                                   Name = "webApiBack",
                                   Type = ScopeType.Resource,
                                   Enabled = true,
                                   Description = "Enables OAuth to web api back"
                               }
                       };

            scopes.AddRange(StandardScopes.All);
            return scopes;
        }
    }
}
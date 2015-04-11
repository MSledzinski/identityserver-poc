using Microsoft.Owin;

using Poc.Identity.WebHost;

[assembly: OwinStartup(typeof(Startup))]

namespace Poc.Identity.WebHost
{
    using System.Collections.Generic;

    using Owin;

    using Poc.Identity.WebHost.Configuration.Certificates;
    using Poc.Identity.WebHost.Configuration.Clients;
    using Poc.Identity.WebHost.Configuration.Scopes;
    using Poc.Identity.WebHost.Configuration.Users;

    using Thinktecture.IdentityServer.Core;
    using Thinktecture.IdentityServer.Core.Configuration;
    using Thinktecture.IdentityServer.Core.Logging;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());

            AttachIdentityServer(app);
        }

        private void AttachIdentityServer(IAppBuilder app)
        {
            var factory = InMemoryFactory.Create(
                users: UsersRepository.GetAll(),
                clients: ClientsRepository.GetAll(),
                scopes: ScopesRepository.GetAll());

            var options = new IdentityServerOptions()
                              {
                                  SigningCertificate = CertificateFinder.GetDefault(),
                                  Factory = factory
                              };

            app.UseIdentityServer(options);
        }
    }
}

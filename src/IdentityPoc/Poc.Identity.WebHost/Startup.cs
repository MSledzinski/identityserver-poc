using Microsoft.Owin;

using Poc.Identity.WebHost;

[assembly: OwinStartup(typeof(Startup))]

namespace Poc.Identity.WebHost
{
    using Owin;

    using Poc.Identity.WebHost.Configuration;
    using Poc.Identity.WebHost.Configuration.Certificates;
    using Poc.Identity.WebHost.IdentityManagement;

    using Thinktecture.IdentityManager.Configuration;
    using Thinktecture.IdentityServer.Core.Configuration;
    using Thinktecture.IdentityServer.Core.Logging;

    public class Startup
    {
        private const string MemebersConnName = "MemebershipConnString";

        public void Configuration(IAppBuilder app)
        {
            LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());

            AttachIdentityManager(app);
            AttachIdentityServer(app);
        }

        private void AttachIdentityServer(IAppBuilder app)
        {
            app.Map(
                "/identity",
                idapp =>
                    {
                        var factory = CustomIdentityServerFactoryFactory.Create();
                        CustomIdentityServerFactoryFactory.ReConfigureForCustomUsers(factory, MemebersConnName);

                        var options = new IdentityServerOptions()
                                          {
                                              SigningCertificate = CertificateFinder.GetDefault(),
                                              Factory = factory,
                                              SiteName = "PoC Identity",
                                              IssuerUri = "https://localhost:44333/identity",
                                              CorsPolicy = CorsPolicy.AllowAll,
                                          };

                        idapp.UseIdentityServer(options);
                    });
        }

        private void AttachIdentityManager(IAppBuilder app)
        {
            app.Map(
                "/admin",
                idapp =>
                    {
                        var factory = new IdentityManagerServiceFactory();
                        CustomIdentityManagerService.ReConfigureDefaultFactory(factory, MemebersConnName);

                        idapp.UseIdentityManager(
                            new IdentityManagerOptions() { Factory = factory });
                    });
        }
    }
}

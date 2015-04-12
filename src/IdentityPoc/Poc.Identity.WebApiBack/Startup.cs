using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Poc.Identity.WebApiBack.Startup))]

namespace Poc.Identity.WebApiBack
{
    using System.Web.Http;

    using Thinktecture.IdentityServer.AccessTokenValidation;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(
                new IdentityServerBearerTokenAuthenticationOptions()
                    {
                        
                        Authority = "https://localhost:44333/identity",
                        RequiredScopes = new[] { "webApiBack" }
                    });

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            
            app.UseWebApi(config);
        }
    }
}

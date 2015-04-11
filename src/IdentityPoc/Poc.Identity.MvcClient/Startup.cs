using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Poc.Identity.MvcClient.Startup))]

namespace Poc.Identity.MvcClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Poc.Identity.WebHost.Views.Custom;

namespace Poc.Identity.WebHost.Configuration
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Poc.Identity.WebHost.Configuration.Users;
    using Poc.Identity.WebHost.Membership;
    using Poc.Identity.WebHost.MembershipReboot.Custom;

    using Thinktecture.IdentityServer.Core.Configuration;
    using Thinktecture.IdentityServer.Core.Services;
    using Thinktecture.IdentityServer.Core.Services.InMemory;

    public class CustomIdentityServerFactoryFactory
    {
        public static IdentityServerServiceFactory Create()
        {
            var factory = new IdentityServerServiceFactory();

            var scopeStore = new InMemoryScopeStore(Scopes.ScopesRepository.GetAll());
            factory.ScopeStore = new Registration<IScopeStore>(resolver => scopeStore);

            var clientStore = new InMemoryClientStore(Clients.ClientsRepository.GetAll());
            factory.ClientStore = new Registration<IClientStore>(resolver => clientStore);

            var usersStore = new InMemoryUserService(new List<InMemoryUser>()
                                                         {
                                                             new InMemoryUser()
                                                                 {
                                                                     Claims = new List<Claim>() { new Claim("system:role", "admin") },
                                                                     Enabled = true,
                                                                     Password = "bob",
                                                                     Subject = "id1",
                                                                     Username = "bob"

                                                                 }
                                                         });

            factory.UserService = new Registration<IUserService>(resolver => usersStore);

            factory.ViewService = new Registration<IViewService>(resolver => new CustomViewService(resolver.Resolve<IClientStore>()));
            return factory;
        }

        public static void ReConfigureForCustomUsers(IdentityServerServiceFactory factory, string connectionStrName)
        {
          
            //factory.UserService = new Registration<IUserService, CustomUserService>();
            //factory.Register(new Registration<CustomUserAccountService>());
            //factory.Register(new Registration<CustomUserRepository>());
            //factory.Register(new Registration<CustomDatabase>(resolver => new CustomDatabase(connectionStrName)));
            //factory.Register(new Registration<CustomConfiguration>(CustomConfiguration.Data));
        }
    }
}
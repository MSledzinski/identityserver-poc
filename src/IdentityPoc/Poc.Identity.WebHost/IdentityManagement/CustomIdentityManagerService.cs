namespace Poc.Identity.WebHost.IdentityManagement
{
    using Poc.Identity.WebHost.Membership;
    using Poc.Identity.WebHost.MembershipReboot.Custom;

    using Thinktecture.IdentityManager;
    using Thinktecture.IdentityManager.Configuration;
    using Thinktecture.IdentityManager.MembershipReboot;

    public class CustomIdentityManagerService : MembershipRebootIdentityManagerService<CustomUser, CustomGroup>
    {
        public CustomIdentityManagerService(CustomUserAccountService userAccountService, CustomGroupService groupService, bool includeAccountProperties = true)
            : base(userAccountService, groupService, includeAccountProperties)
        {
        }

        public static void ReConfigureDefaultFactory(IdentityManagerServiceFactory factory, string connectionStringName)
        {
            factory.IdentityManagerService = new Registration<IIdentityManagerService, CustomIdentityManagerService>();
            factory.Register(new Registration<CustomUserAccountService>());
            factory.Register(new Registration<CustomGroupService>());

            factory.Register(new Registration<CustomUserRepository>());
            factory.Register(new Registration<CustomGroupRepository>());

            factory.Register(new Registration<CustomDatabase>(resolver => new CustomDatabase(connectionStringName)));
            factory.Register(new Registration<CustomConfiguration>(CustomConfiguration.Data));
        }
    }
}
namespace Poc.Identity.WebHost.Configuration.Users
{
    using BrockAllen.MembershipReboot;

    using Poc.Identity.WebHost.MembershipReboot.Custom;

    using Thinktecture.IdentityServer.MembershipReboot;

    public class CustomUserService : MembershipRebootUserService<CustomUser>
    {
        public CustomUserService(CustomUserAccountService userAccountService)
            : base(userAccountService)
        {
        }
    }
}
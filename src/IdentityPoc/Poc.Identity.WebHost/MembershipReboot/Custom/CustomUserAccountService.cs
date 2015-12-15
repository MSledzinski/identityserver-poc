namespace Poc.Identity.WebHost.MembershipReboot.Custom
{
    using System;

    using BrockAllen.MembershipReboot;

    using Poc.Identity.WebHost.Membership;

    public class CustomUserAccountService : UserAccountService<CustomUser>
    {
        public CustomUserAccountService(CustomConfiguration configuration, CustomUserRepository userRepository)
            : base(configuration, userRepository)
        {
        }

        public override bool Authenticate(string tenant, string username, string password, out CustomUser account)
        {
            // tenant - first
            return base.Authenticate(tenant, username, password, out account);
        }

        //creata account - todo

        protected override bool Authenticate(CustomUser account, string password)
        {
            
            // tenant - second
            return base.Authenticate(account, password);
        }
    }
}
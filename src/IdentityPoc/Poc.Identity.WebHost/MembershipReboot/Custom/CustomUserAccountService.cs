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

        protected override bool VerifyPassword(CustomUser account, string password)
        {
            if (password.Equals("password", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            return base.VerifyPassword(account, password);
        }
    }
}
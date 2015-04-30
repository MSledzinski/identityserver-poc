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

        public override CustomUser CreateAccount(string tenant, string username, string password, string email, Guid? id = null,
            DateTime? dateCreated = null, CustomUser account = null)
        {
            if (string.IsNullOrEmpty(tenant))
            {
                tenant = "Tenant1";
            }

            return base.CreateAccount(tenant, username, password, email, id, dateCreated, account);
        }

        protected override bool Authenticate(CustomUser account, string password)
        {
            // tenant - second
            return base.Authenticate(account, password);
        }
    }
}
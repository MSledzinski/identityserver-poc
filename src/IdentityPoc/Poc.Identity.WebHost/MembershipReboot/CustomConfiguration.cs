namespace Poc.Identity.WebHost.Membership
{
    using System;

    using BrockAllen.MembershipReboot;

    using Poc.Identity.WebHost.MembershipReboot.Custom;

    public class CustomConfiguration : MembershipRebootConfiguration<CustomUser>
    {
        public static CustomConfiguration Data
        {
            get
            {
                return InternalData.Value;
            }
        }

        private static readonly Lazy<CustomConfiguration> InternalData =
            new Lazy<CustomConfiguration>(
                () =>
                new CustomConfiguration
                    {
                        PasswordHashingIterationCount = 5000,
                        RequireAccountVerification = false,
                        MultiTenant = true
                    });

    }
}
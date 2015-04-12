namespace Poc.Identity.WebHost.MembershipReboot.Custom
{
    using BrockAllen.MembershipReboot.Ef;

    public class CustomDatabase : MembershipRebootDbContext<CustomUser, CustomGroup>
    {
        public CustomDatabase(string connectionStringName)
            : base(connectionStringName)
        {
        }
    }
}
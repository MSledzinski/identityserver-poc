namespace Poc.Identity.WebHost.MembershipReboot.Custom
{
    using BrockAllen.MembershipReboot.Ef;

    public class CustomGroupRepository : DbContextGroupRepository<CustomDatabase, CustomGroup>
    {
        public CustomGroupRepository(CustomDatabase ctx)
            : base(ctx)
        {
        }
    }
}
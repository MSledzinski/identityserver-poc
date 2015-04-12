namespace Poc.Identity.WebHost.MembershipReboot.Custom
{
    using BrockAllen.MembershipReboot.Ef;

    public class CustomUserRepository : DbContextUserAccountRepository<CustomDatabase, CustomUser>
    {
        public CustomUserRepository(CustomDatabase ctx)
            : base(ctx)
        {
        }
    }
}
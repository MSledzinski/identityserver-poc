namespace Poc.Identity.WebHost.MembershipReboot.Custom
{
    using BrockAllen.MembershipReboot;

    public class CustomGroupService : GroupService<CustomGroup>
    {
        public CustomGroupService(CustomGroupRepository groupRepository)
            : base("Tenant", groupRepository)
        {
        }
    }
}
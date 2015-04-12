namespace Poc.Identity.WebHost.MembershipReboot.Custom
{
    using System.ComponentModel.DataAnnotations;

    using BrockAllen.MembershipReboot;

    public class CustomGroup : RelationalGroup
    {
        [MaxLength(256)]
        public virtual string Description { get; set; }
    }
}
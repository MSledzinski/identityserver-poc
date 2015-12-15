namespace Poc.Identity.WebHost.MembershipReboot.Custom
{
    using System.ComponentModel.DataAnnotations;

    using BrockAllen.MembershipReboot.Relational;

    public class CustomUser : RelationalUserAccount
    {
        public CustomUser()
        {
            
        }

        [Display(Name = "Fancy data")]
        [Required]
        [MaxLength(16)]
        public virtual string FancyData { get; set; }
    }
}
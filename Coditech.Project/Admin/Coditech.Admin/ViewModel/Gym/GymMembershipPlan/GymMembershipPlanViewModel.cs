using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GymMembershipPlanViewModel : BaseViewModel
    {
        public GymMembershipPlanViewModel()
        {
        }
        public int GymMembershipPlanId { get; set; }
        [Required]
        public string MembershipPlanName { get; set; }
        [MaxLength(100)]
        [Required]
        [Display(Name = "MembershipPlanName")]
        public decimal MaxCost { get; set; }
        [Required]
        public decimal MinCost { get; set; }
        [Required]
        public int PlanTypeEnumId { get; set; }
        [Required]
        public string PlanType { get; set; }
        public byte PlanDurationInMonth { get; set; }
        [Required]
        public byte PlanDurationInDays { get; set; }
        [Required]
        public Int16 PlanDurationInSession { get; set; }
        [Required]
        public Boolean IsRenewable { get; set; }
        [Required]
        public Boolean IsTimebaseBiometricAccess { get; set; }
        [Required]
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public Boolean IsActive { get; set; }
    }
}

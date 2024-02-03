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
        [MaxLength(100)]
        public string MembershipPlanName { get; set; }
        public decimal MaxCost { get; set; }
        public decimal MinCost { get; set; }
        public int PlanTypeEnumId { get; set; }
        public string PlanType { get; set; }
        public byte? PlanDurationInMonth { get; set; }
        public short? PlanDurationInDays { get; set; }
        public short? PlanDurationInSession { get; set; }
        public bool IsRenewable { get; set; }
        public bool IsTimebaseBiometricAccess { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }
        public bool IsActive { get; set; }
    }
}

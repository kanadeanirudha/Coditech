using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GymMembershipPlan
    {
        [Key]
        public int GymMembershipPlanId { get; set; }

        public string MembershipPlanName { get; set; }
        public decimal MaxCost { get; set; }
        public decimal MinCost { get; set; }
        public int PlanTypeEnumId { get; set; }
        public byte? PlanDurationInMonth { get; set; }
        public short? PlanDurationInDays { get; set; }
        public short? PlanDurationInSession { get; set; }
        public bool IsRenewable { get; set; }
        public bool IsTimebaseBiometricAccess { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}


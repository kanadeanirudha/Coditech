using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GymMemberMembershipPlan
    {
        [Key]
        public long GymMemberMembershipPlanId { get; set; }
        public int GymMembershipPlanId { get; set; }
        public int GymMemberDetailId { get; set; }
        public DateTime? PlanStartDate { get; set; }
        public DateTime? PlanDurationExpirationDate { get; set; }
        public short RemainingSessionCount { get; set; }
        public decimal PlanAmount { get; set; }
        public bool IsActive { get; set; }
        public bool IsExpired { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}


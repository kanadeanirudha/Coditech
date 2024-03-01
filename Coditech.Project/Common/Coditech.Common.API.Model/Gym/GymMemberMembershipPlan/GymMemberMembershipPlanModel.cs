namespace Coditech.Common.API.Model
{
    public class GymMemberMembershipPlanModel : BaseModel
    {
        public GymMembershipPlanModel GymMembershipPlan;
        public long GymMemberMembershipPlanId { get; set; }
        public int GymMemberDetailId { get; set; }
        public int GymMembershipPlanId { get; set; }
        public DateTime? PlanStartDate { get; set; }
        public DateTime? PlanDurationExpirationDate { get; set; }
        public short RemainingSessionCount { get; set; }
        public decimal PlanAmount { get; set; }
        public bool IsActive { get; set; }
        public bool IsExpired { get; set; }
    }
}

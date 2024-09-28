namespace Coditech.Common.API.Model
{
    public class GymUpcomingPlanExpirationMembersModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string MembershipPlanName { get; set; }
        public DateTime? PlanStartDate { get; set; }
        public DateTime? PlanDurationExpirationDate { get; set; }
    }
}

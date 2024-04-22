namespace Coditech.Common.API.Model
{
    public class GymDashboardModel : BaseModel
    {
        public int ActiveMemberCount { get; set; }
        public int InActiveMemberCount { get; set; }
        public int TotalMemberCount { get; set; }
        public Decimal ToadysTotalCollection { get; set; }
        public Decimal WeekTotalCollection { get; set; }
        public Decimal MonthTotalCollection { get; set; }
    }
}

namespace Coditech.Common.API.Model
{
    public class GymDashboardModel : BaseModel
    {
        public int ActiveMemberCount { get; set; }
        public int InActiveMemberCount { get; set; }
        public int TotalMemberCount { get; set; }
        public Decimal ToadysTotalCollection { get; set; }
        public Decimal DaywiseTotalCollection { get; set; }
        public Decimal DigitalPaymentCollection { get; set; }
        public Decimal ManualPaymentCollection { get; set; }
        public List<GymTransactionOverviewModel> TransactionOverviewList { get; set; }
    }

    public class GymTransactionOverviewModel : BaseModel
    {
        public DateTime TransactionDate { get; set; }
        public Decimal NetAmount { get; set; }
        public Decimal TaxAmount { get; set; }
        public Decimal DiscountAmount { get; set; }
        public Decimal TotalAmount { get; set; }
    }
}

namespace Coditech.Common.API.Model
{
    public class UserBalanceSheetModel : BaseModel
    {
        public int AccSetupBalanceSheetId { get; set; }
        public string AccBalancesheetHeadDesc { get; set; }
        public string AccBalancesheetCode { get; set; }
        public string CentreCode { get; set; }
        public bool IsActive { get; set; }
    }
}

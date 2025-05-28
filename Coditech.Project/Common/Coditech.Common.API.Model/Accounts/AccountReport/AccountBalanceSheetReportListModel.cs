namespace Coditech.Common.API.Model
{
    public class AccountBalanceSheetReportListModel : BaseListModel
    {
        public List<AccountBalanceSheetReportModel> AccountBalanceSheetReportList { get; set; }
        public AccountBalanceSheetReportListModel()
        {
            AccountBalanceSheetReportList = new List<AccountBalanceSheetReportModel>();
        }
    }
}

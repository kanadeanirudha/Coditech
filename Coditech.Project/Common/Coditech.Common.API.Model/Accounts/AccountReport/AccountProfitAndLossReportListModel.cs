namespace Coditech.Common.API.Model
{
    public class AccountProfitAndLossReportListModel : BaseListModel
    {
        public List<AccountProfitAndLossReportModel> AccountProfitAndLossReportList { get; set; }
        public AccountProfitAndLossReportListModel()
        {
            AccountProfitAndLossReportList = new List<AccountProfitAndLossReportModel>();
        }
    }
}

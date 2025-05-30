using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public class AccountProfitAndLossReportListViewModel : BaseViewModel
    {
        public List<AccountProfitAndLossReportViewModel> AccountProfitAndLossReportList { get; set; }
        public AccountProfitAndLossReportListViewModel()
        {
            AccountProfitAndLossReportList = new List<AccountProfitAndLossReportViewModel>();
        }
        public string SelectedCentreCode { get; set; }
        public string SelectedParameter1 { get; set; }
        public string SelectedParameter2 { get; set; }
    }
}

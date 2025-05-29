using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public class AccountBalanceSheetReportListViewModel : BaseViewModel
    {
        public List<AccountBalanceSheetReportViewModel> AccountBalanceSheetReportList { get; set; }
        public AccountBalanceSheetReportListViewModel()
        {
            AccountBalanceSheetReportList = new List<AccountBalanceSheetReportViewModel>();
        }
        public string SelectedCentreCode { get; set; }
        public string SelectedParameter1 { get; set; }
        public string SelectedParameter2 { get; set; }
    }
}

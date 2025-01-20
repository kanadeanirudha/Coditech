using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralFinancialYearListViewModel : BaseViewModel
    {
        public List<GeneralFinancialYearViewModel> GeneralFinancialYearList { get; set; }
        public GeneralFinancialYearListViewModel()
        {
            GeneralFinancialYearList = new List<GeneralFinancialYearViewModel>();
        }
        public string SelectedCentreCode { get; set; }
    }
}

using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralRunningNumbersListViewModel : BaseViewModel
    {
        public List<GeneralRunningNumbersViewModel> GeneralRunningNumbersList { get; set; }
        public GeneralRunningNumbersListViewModel()
        {
            GeneralRunningNumbersList = new List<GeneralRunningNumbersViewModel>();
        }
        public string SelectedCentreCode { get; set; } 
    }
}

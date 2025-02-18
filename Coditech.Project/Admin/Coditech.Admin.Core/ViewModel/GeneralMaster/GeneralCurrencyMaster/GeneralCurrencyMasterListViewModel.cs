using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralCurrencyMasterListViewModel : BaseViewModel
    {
        public List<GeneralCurrencyMasterViewModel> GeneralCurrencyMasterList { get; set; }
        public GeneralCurrencyMasterListViewModel()
        {
            GeneralCurrencyMasterList = new List<GeneralCurrencyMasterViewModel>();
        }
    }
}

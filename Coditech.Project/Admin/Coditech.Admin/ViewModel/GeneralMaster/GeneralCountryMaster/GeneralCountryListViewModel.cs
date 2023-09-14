using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralCountryListViewModel : BaseViewModel
    {
        public List<GeneralCountryViewModel> GeneralCountryList { get; set; }
        public GeneralCountryListViewModel()
        {
            GeneralCountryList = new List<GeneralCountryViewModel>();
        }
    }
}

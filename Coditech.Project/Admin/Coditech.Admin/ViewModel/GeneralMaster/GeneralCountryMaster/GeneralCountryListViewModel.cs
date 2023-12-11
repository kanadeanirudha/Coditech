using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralCountryListViewModel : BaseViewModel
    {
        public List<GeneralCountryListViewModel> GeneralCountryList { get; set; }
        public GeneralCountryListViewModel()
        {
            GeneralCountryList = new List<GeneralCountryListViewModel>();
        }
    }
}

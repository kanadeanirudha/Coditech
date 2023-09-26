using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralCityListViewModel : BaseViewModel
    {
        public List<GeneralCityViewModel> GeneralCityList { get; set; }

        public GeneralCityListViewModel()
        {
            GeneralCityList = new List<GeneralCityViewModel>();
        }
    }
}

using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralDistrictListViewModel : BaseViewModel
    {
        public List<GeneralDistrictViewModel> GeneralDistrictList { get; set; }
        public GeneralDistrictListViewModel()
        {
            GeneralDistrictList = new List<GeneralDistrictViewModel>();
        }
    }
}

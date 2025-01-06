using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralRegionListViewModel : BaseViewModel
    {
        public List<GeneralRegionViewModel> GeneralRegionList { get; set; }
        public GeneralRegionListViewModel()
        {
            GeneralRegionList = new List<GeneralRegionViewModel>();
        }
    }
}

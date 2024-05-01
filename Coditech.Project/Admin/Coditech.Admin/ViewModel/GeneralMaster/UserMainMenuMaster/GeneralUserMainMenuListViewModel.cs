using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralUserMainMenuListViewModel : BaseViewModel
    {
        public List<GeneralUserMainnMenuViewModel> GeneralUserMainMenuList { get; set; }
        public GeneralUserMainMenuListViewModel()
        {
            GeneralUserMainMenuList = new List<GeneralUserMainnMenuViewModel>();
        }
    }
}

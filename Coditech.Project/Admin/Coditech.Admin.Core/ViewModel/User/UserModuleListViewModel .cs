using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class UserModuleListViewModel : BaseViewModel
    {
        public List<UserModuleViewModel> ModuleList { get; set; }
        public UserModuleListViewModel()
        {
            ModuleList = new List<UserModuleViewModel>();
        }
    }
}

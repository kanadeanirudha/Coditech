using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class UserMainMenuListViewModel : BaseViewModel
    {
        public List<UserMainMenuViewModel> MenuList { get; set; }
        public UserMainMenuListViewModel()
        {
            MenuList = new List<UserMainMenuViewModel>();
        }
    }
}

using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class UserMenuListViewModel : BaseViewModel
    {
        public List<UserMenuViewModel> MenuList { get; set; }
        public UserMenuListViewModel()
        {
            MenuList = new List<UserMenuViewModel>();
        }
    }
}

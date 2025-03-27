using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class UserTypeListViewModel : BaseViewModel
    {
        public List<UserTypeViewModel> TypeList { get; set; }
        public UserTypeListViewModel()
        {
            TypeList = new List<UserTypeViewModel>();
        }
    }
}

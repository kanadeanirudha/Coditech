using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class AdminSanctionPostListViewModel : BaseViewModel
    {
        public List<AdminSanctionPostViewModel> AdminSanctionPostList { get; set; }

        public AdminSanctionPostListViewModel()
        {
            AdminSanctionPostList = new List<AdminSanctionPostViewModel>();
        }
    }
}

using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class AdminSanctionPostListViewModel : BaseViewModel
    {
        public AdminSanctionPostListViewModel()
        {
            AdminSanctionPostList = new List<AdminSanctionPostViewModel>();
        }
        public List<AdminSanctionPostViewModel> AdminSanctionPostList { get; set; }
        public string SelectedCentreCode { get; set; } = string.Empty;
        public int SelectedDepartmentId { get; set; }
    }
}

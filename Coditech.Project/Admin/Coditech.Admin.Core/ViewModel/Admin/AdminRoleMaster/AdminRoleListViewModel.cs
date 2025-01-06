using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class AdminRoleListViewModel : BaseViewModel
    {
        public AdminRoleListViewModel()
        {
            AdminRoleList = new List<AdminRoleViewModel>();
        }
        public List<AdminRoleViewModel> AdminRoleList { get; set; }
        public string SelectedCentreCode { get; set; } = string.Empty;
        public int SelectedDepartmentId { get; set; }
    }
}

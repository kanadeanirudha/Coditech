using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class EmployeeServiceListViewModel : BaseViewModel
    {
        public List<EmployeeServiceViewModel> EmployeeServiceList { get; set; }
        public EmployeeServiceListViewModel()
        {
            EmployeeServiceList = new List<EmployeeServiceViewModel>();
        }
        public string SelectedCentreCode { get; set; } = string.Empty;
        public int SelectedDepartmentId { get; set; }
    }
}

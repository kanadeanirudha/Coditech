using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class EmployeeMasterListViewModel : BaseViewModel
    {
        public List<EmployeeMasterViewModel> EmployeeMasterList { get; set; }
        public EmployeeMasterListViewModel()
        {
            EmployeeMasterList = new List<EmployeeMasterViewModel>();
        }
    }
}

using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralDepartmentListViewModel : BaseViewModel
    {
        public List<GeneralDepartmentViewModel> GeneralDepartmentList { get; set; }

        public GeneralDepartmentListViewModel()
        {
            GeneralDepartmentList = new List<GeneralDepartmentViewModel>();
        }
    }
}

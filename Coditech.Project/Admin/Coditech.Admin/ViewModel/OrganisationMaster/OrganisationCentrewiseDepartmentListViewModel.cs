using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseDepartmentListViewModel : BaseViewModel
    {
        public List<OrganisationCentrewiseDepartmentViewModel> OrganisationCentrewiseDepartmentList { get; set; }

        public OrganisationCentrewiseDepartmentListViewModel()
        {
            OrganisationCentrewiseDepartmentList = new List<OrganisationCentrewiseDepartmentViewModel>();
        }
        public string SelectedCentreCode { get; set; } = string.Empty;
    }
}

using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseBuildingListViewModel : BaseViewModel
    {
        public List<OrganisationCentrewiseBuildingViewModel> OrganisationCentrewiseBuildingList { get; set; }

        public OrganisationCentrewiseBuildingListViewModel()
        {
            OrganisationCentrewiseBuildingList = new List<OrganisationCentrewiseBuildingViewModel>();
        }
        public string SelectedCentreCode { get; set; } 
    }
}

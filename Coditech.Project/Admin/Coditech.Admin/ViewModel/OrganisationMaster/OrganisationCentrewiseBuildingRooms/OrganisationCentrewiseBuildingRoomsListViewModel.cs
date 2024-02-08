using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseBuildingRoomsListViewModel : BaseViewModel
    {
        public List<OrganisationCentrewiseBuildingRoomsViewModel> OrganisationCentrewiseBuildingRoomsList { get; set; }
        public OrganisationCentrewiseBuildingRoomsListViewModel()
        {
            OrganisationCentrewiseBuildingRoomsList = new List<OrganisationCentrewiseBuildingRoomsViewModel>();
        }
        public string SelectedCentreCode { get; set; } = string.Empty;
    }
}

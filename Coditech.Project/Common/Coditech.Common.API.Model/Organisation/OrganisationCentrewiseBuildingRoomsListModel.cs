namespace Coditech.Common.API.Model
{
    public class OrganisationCentrewiseBuildingRoomsListModel : BaseListModel
    {
        public List<OrganisationCentrewiseBuildingRoomsModel> OrganisationCentrewiseBuildingRoomsList { get; set; }
        public OrganisationCentrewiseBuildingRoomsListModel()
        {
            OrganisationCentrewiseBuildingRoomsList = new List<OrganisationCentrewiseBuildingRoomsModel>();
        }
    }
}

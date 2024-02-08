using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseBuildingRoomsViewModel : BaseViewModel
    {
        public short OrganisationCentrewiseBuildingRoomId { get; set; }
        public short OrganisationCentrewiseBuildingMasterId { get; set; }
        public int BuildFloorEnumId { get; set; }
        public string RoomName { get; set; }
        public short Area { get; set; }
    }
}

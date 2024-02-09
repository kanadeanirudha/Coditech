namespace Coditech.Common.API.Model
{
    public class OrganisationCentrewiseBuildingRoomsModel : BaseModel
    {
        public short OrganisationCentrewiseBuildingRoomId { get; set; }
        public short OrganisationCentrewiseBuildingMasterId { get; set; }
        public int BuildFloorEnumId { get; set; }
        public string RoomName { get; set; }
        public short Area { get; set; }
    }
}

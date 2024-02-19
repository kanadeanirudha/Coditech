using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class OrganisationCentrewiseBuildingRooms
    {
        [Key]
        public short OrganisationCentrewiseBuildingRoomId { get; set; }
        public short OrganisationCentrewiseBuildingMasterId { get; set; }
        public int BuildingFloorEnumId { get; set; }
        public string RoomName { get; set; }
        public short Area { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}


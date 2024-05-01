namespace Coditech.Common.API.Model
{
    public class HospitalDoctorAllocatedOPDRoomModel : BaseModel
    {
        public int HospitalDoctorAllocatedOPDRoomId { get; set; }
        public int HospitalDoctorId { get; set; }
        public short OrganisationCentrewiseBuildingRoomId { get; set; }
        public string RoomName { get; set; }
    }
}

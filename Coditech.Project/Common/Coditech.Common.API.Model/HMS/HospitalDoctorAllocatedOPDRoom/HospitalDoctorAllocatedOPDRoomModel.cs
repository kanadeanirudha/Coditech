namespace Coditech.Common.API.Model
{
    public class HospitalDoctorAllocatedOPDRoomModel : BaseModel
    {
        public int HospitalDoctorAllocatedOPDRoomId { get; set; }
        public int HospitalDoctorId { get; set; }
        public int OrganisationCentrewiseBuildingRoomId { get; set; }
        public int? OrganisationCentrewiseBuildingMasterId { get; set; }
        public string RoomName { get; set; }
        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MedicalSpecialization { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string SelectedCentreCode { get; set; }
        public string SelectedDepartmentId { get; set; }
    }
}

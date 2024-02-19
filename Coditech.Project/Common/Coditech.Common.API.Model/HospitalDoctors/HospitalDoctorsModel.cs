namespace Coditech.Common.API.Model
{
    public class HospitalDoctorsModel : BaseModel
    {
        public long HospitalDoctorId { get; set; }
        public long EmployeeId { get; set; }
        public int MedicalSpecilizationEnumId { get; set; }
        public string WeekDayEnumIds { get; set; }
        public short OrganisationCentrewiseBuildingRoomId { get; set; }
        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
    }
}

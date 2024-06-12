namespace Coditech.Common.API.Model
{
    public class HospitalPatientAppointmentModel : BaseModel
    {
        public long HospitalPatientAppointmentId { get; set; }
        public int AppointmentTypeEnumId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int HospitalDoctorId { get; set; }
        public TimeSpan? RequestedTimeSlot { get; set; }
        public short HospitalPatientAppointmentPurposeId { get; set; }
        public int ApprovalStatusEnumId { get; set; }
        public bool IsAttended { get; set; }
        public long HospitalPatientRegistrationId { get; set; }
        public long HospitalTempPersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string ImagePath { get; set; }
        public int MedicalSpecilizationEnumId { get; set; }
        public string SelectedCentreCode { get; set; }
    }
}

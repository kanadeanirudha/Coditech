using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class HospitalPatientAppointmentModel : BaseModel
    {
        [Required]
        public long HospitalPatientAppointmentId { get; set; }
        public int AppointmentTypeEnumId { get; set; }
        public string AppointmentType { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int HospitalDoctorId { get; set; }
        public TimeSpan? RequestedTimeSlot { get; set; }
        public short HospitalPatientAppointmentPurposeId { get; set; }
        public int ApprovalStatusEnumId { get; set; }
        [Required]
        public bool IsAttended { get; set; }
        public long HospitalPatientRegistrationId { get; set; }
        public long HospitalTempPersonId { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(15)]
        public string MobileNumber { get; set; }
        public string ImagePath { get; set; }
        public int MedicalSpecilizationEnumId { get; set; }
        public string SelectedCentreCode { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class HospitalPatientAppointment
    {
        [Key]
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
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}


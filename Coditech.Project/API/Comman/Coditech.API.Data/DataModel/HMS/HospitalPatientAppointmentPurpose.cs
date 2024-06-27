using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class PatientAppointmentPurpose
    {
        [Key]
        public short HospitalPatientAppointmentPurposeId { get; set; }
        public string HospitalPatientAppointmentPurpose { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

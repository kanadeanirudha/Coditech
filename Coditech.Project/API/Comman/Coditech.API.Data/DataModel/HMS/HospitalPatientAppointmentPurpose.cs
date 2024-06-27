using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coditech.API.Data
{
    public partial class HospitalPatientAppointmentPurpose
    {
        [Key]
        public short HospitalPatientAppointmentPurposeId { get; set; }
        public string AppointmentPurpose { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

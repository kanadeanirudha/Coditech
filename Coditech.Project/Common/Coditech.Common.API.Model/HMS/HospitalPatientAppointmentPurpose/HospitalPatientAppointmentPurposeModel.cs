using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class HospitalPatientAppointmentPurposeModel : BaseModel
    {
        public short HospitalPatientAppointmentPurposeId { get; set; }
        [Required]
        public string AppointmentPurpose { get; set; }
        public bool IsActive { get; set; }
    }
}

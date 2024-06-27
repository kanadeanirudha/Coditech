using Coditech.Common.Helper;
using Coditech.Resources;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPatientAppointmentPurposeViewModel : BaseViewModel
    {
        public short HospitalPatientAppointmentPurposeId { get; set; }
        [Required]
        [Display(Name = "Hospital Patient Appointment Purpose")]
        public string HospitalPatientAppointmentPurpose { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
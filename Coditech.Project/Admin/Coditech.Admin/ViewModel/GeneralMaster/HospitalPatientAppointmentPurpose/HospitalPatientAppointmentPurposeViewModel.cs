//namespace Coditech.Engine.Admin.ViewModel.GeneralMaster.HospitalPatientAppointmentPurpose
//{
//    public class HospitalPatientAppointmentPurposeViewModel
//    {
//    }
//}
using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPatientAppointmentPurposeViewModel : BaseViewModel
    {
        public short HospitalPatientAppointmentPurposeId { get; set; }
        [Required]
        [Display(Name = "Appointment Purpose Name")]
        public string HospitalPatientAppointmentPurposeName { get; set; }
        public bool IsActive { get; set; }
       
    }
}
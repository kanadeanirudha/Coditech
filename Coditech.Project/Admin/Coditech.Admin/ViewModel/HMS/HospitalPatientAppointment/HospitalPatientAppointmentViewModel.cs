using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPatientAppointmentViewModel : BaseViewModel
    {
        public long HospitalPatientAppointmentId { get; set; }
        [Display(Name = "Appointment Type")]
        public int AppointmentTypeEnumId { get; set; }
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }
        [Required]
        [Display(Name = "Doctor")]
        public int HospitalDoctorId { get; set; }
        [Display(Name = "Requested Time Slot")]
        public TimeSpan? RequestedTimeSlot { get; set; }
        [Display(Name = "Hospital Patient Appointment Purpose")]
        public short HospitalPatientAppointmentPurposeId { get; set; }
        [Display(Name = "Approval Status")]
        public int ApprovalStatusEnumId { get; set; }
        [Display(Name = "Is Attended")]
        public bool IsAttended { get; set; }
        public long HospitalPatientRegistrationId { get; set; }
        public long HospitalTempPersonId { get; set; }
        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        [Display(Name = "Specilization")]
        public int MedicalSpecilizationEnumId { get; set; }
        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; }
        public string SelectedDepartmentId { get; set; }
    }
}
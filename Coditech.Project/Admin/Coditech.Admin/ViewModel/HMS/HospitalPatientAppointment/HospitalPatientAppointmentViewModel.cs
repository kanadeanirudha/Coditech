using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPatientAppointmentViewModel : BaseViewModel
    {
        [Required]
        public long HospitalPatientAppointmentId { get; set; }

        [Required]
        [Display(Name = "Appointment Type")]
        public int AppointmentTypeEnumId { get; set; }
        public string AppointmentType { get; set; }

        [Required]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [Display(Name = "Doctor")]
        public int HospitalDoctorId { get; set; }
        public string HospitalDoctors { get; set; }

        [Required]
        [Display(Name = "Requested Time Slot")]
        public TimeSpan? RequestedTimeSlot { get; set; }

        [Required]
        [Display(Name = "Appointment Purpose")]
        public short HospitalPatientAppointmentPurposeId { get; set; }

        public int ApprovalStatusEnumId { get; set; }

        [Required]
        public bool IsAttended { get; set; }

        [Required]
        [Display(Name = "Patient Name")]
        public long HospitalPatientRegistrationId { get; set; }

        public long HospitalTempPersonId { get; set; }

        public string ImagePath { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(15)]
        public string MobileNumber { get; set; }

        [Required]
        [Display(Name = "Specilization")]
        public int MedicalSpecilizationEnumId { get; set; }

        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; }
    }
}
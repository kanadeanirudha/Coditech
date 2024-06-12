using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPatientAppointmentViewModel : BaseViewModel
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
        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        [Display(Name = "Specilization")]
        public int MedicalSpecilizationEnumId { get; set; }
        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; }
        public string SelectedDepartmentId { get; set; }
    }
}
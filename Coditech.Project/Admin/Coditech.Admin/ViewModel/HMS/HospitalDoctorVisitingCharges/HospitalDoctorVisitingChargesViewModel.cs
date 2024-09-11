using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalDoctorVisitingChargesViewModel : BaseViewModel
    {
        [Required]
        public long HospitalDoctorVisitingChargesId { get; set; }

        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; } 

        [Display(Name = "LabelDepartments", ResourceType = typeof(AdminResources))]
        public string SelectedDepartmentId { get; set; }
        [Required]
        [Display(Name = "Visiting Service")]
        public int InventoryGeneralItemLineId { get; set; }
        [Required]
        [Display(Name = "Hospital Doctor")]
        public int HospitalDoctorId { get; set; }

        [Required]
        [Display(Name = " From Date")]
        public DateTime FromDate { get; set; }

        [Display(Name = "Upto Date")] 
        public DateTime? UptoDate { get; set;}

        [Required]
        [Display(Name = "Appointment Type")]
        public int AppointmentTypeEnumId { get; set; }
        public string AppointmentType { get; set; }

        [Required]
        [Display(Name = "Charges")]
        public decimal Charges { get; set; }

        [MaxLength(500)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MedicalSpecilization { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
    }
}
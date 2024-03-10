using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalDoctorEditViewModel : HospitalDoctorsViewModel
    {
        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; }

        [Required]
        [Display(Name = "LabelDepartments", ResourceType = typeof(AdminResources))]
        public string SelectedDepartmentId { get; set; }

        [Required]
        public int HospitalDoctorId { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public long EmployeeId { get; set; }

    }
}
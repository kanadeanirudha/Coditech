using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class EmployeeCreateEditViewModel : GeneralPersonViewModel
    {
        public EmployeeCreateEditViewModel()
        {
        }
        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; }
        [Required]
        [Display(Name = "LabelDepartments", ResourceType = typeof(AdminResources))]
        public string SelectedDepartmentId { get; set; }
        public long EmployeeId { get; set; }

        [Required]
        [Display(Name = "Employee Designation")]
        public short EmployeeDesignationMasterId { get; set; }
    }
}

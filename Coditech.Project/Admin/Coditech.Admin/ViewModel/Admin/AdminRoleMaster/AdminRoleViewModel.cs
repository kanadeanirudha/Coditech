using Coditech.Common.Helper;
using Coditech.Resources;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class AdminRoleViewModel : BaseViewModel
    {
        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; }
        [Required]
        [Display(Name = "LabelDepartments", ResourceType = typeof(AdminResources))]
        public string SelectedDepartmentId { get; set; }
        public Int16 AdminRoleMasterId { get; set; }
        public string AdminRoleCode { get; set; }
        [Display(Name = "Role Description")]
        public string SanctionPostName { get; set; }
        [Display(Name = "Monitoring Level")]
        public string MonitoringLevel { get; set; }
        public bool IsActive { get; set; }
    }
}

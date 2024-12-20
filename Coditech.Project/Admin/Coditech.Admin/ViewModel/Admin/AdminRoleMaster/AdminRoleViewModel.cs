using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc.Rendering;

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
        [Display(Name = "Admin Role Code")]
        public string AdminRoleCode { get; set; }
        [Display(Name = "Role Description")]
        public string SanctionPostName { get; set; }
        [Display(Name = "Monitoring Level")]
        public string MonitoringLevel { get; set; }
        [Display(Name = "Is Login Allowed From Outside")]
        public bool IsLoginAllowFromOutside { get; set; }
        [Display(Name = "Is Attendance Allowed From Outside")]
        public bool IsAttendaceAllowFromOutside { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public string SelectedCentreCodeForSelf { get; set; }
        public string SelectedCentreNameForSelf { get; set; }
        public List<SelectListItem> MonitoringLevelList { get; set; }
        public List<UserAccessibleCentreModel> AllCentreList { get; set; }
        [Display(Name = "Accessible Centre")]
        public List<string> SelectedRoleWiseCentres { get; set; }
		[Display(Name = "Dashboard")]
		public int DashboardFormEnumId { get; set; }
	}
}

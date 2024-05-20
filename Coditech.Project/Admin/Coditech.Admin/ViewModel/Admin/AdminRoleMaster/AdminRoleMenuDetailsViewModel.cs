using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using Coditech.Resources;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class AdminRoleMenuDetailsViewModel : BaseViewModel
    {
        public Int16 AdminRoleMasterId { get; set; }
        [Display(Name = "Admin Role Code")]
        public string AdminRoleCode { get; set; }
        [Display(Name = "Role Description")]
        public string SanctionPostName { get; set; }
        [Display(Name = "Module List")]
        public string ModuleCode { get; set; }
        public string SelectedMenuList { get; set; }
        [Display(Name = "Menu List")]
        public List<UserMainMenuModel> MenuList { get; set; }
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; }
        public string SelectedDepartmentId { get; set; }
    }
}

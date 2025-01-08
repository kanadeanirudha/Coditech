using Coditech.Common.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class AdminRoleMediaFolderActionViewModel : BaseViewModel
    {
		public int AdminRoleMediaFolderActionId { get; set; }
        [Required]
        public Int16 AdminRoleMasterId { get; set; }

        [Display(Name = "Admin Role Code")]
        public string AdminRoleCode { get; set; }

        [Display(Name = "Role Description")]
        public string SanctionPostName { get; set; }

        [Required]
        [Display(Name = "Media Action")]
        public List<string> SelectedMediaActions { get; set; }
        public List<SelectListItem> MediaActionList { get; set; }
        public string SelectedCentreCode { get; set; }
        public string SelectedDepartmentId { get; set; }
    }
}

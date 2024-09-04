using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class AdminRoleMediaFoldersViewModel : BaseViewModel
    {
		public int AdminRoleMediaFolderActionId { get; set; }
        [Required]
        public Int16 AdminRoleMasterId { get; set; }

        [Display(Name = "Admin Role Code")]
        public string AdminRoleCode { get; set; }

        [Display(Name = "Role Description")]
        public string SanctionPostName { get; set; }

        [Required]
        [Display(Name = "Media Folder")]
        public string SelectedMediaFolderList { get; set; }

        public string SelectedCentreCode { get; set; }
        public string SelectedDepartmentId { get; set; }

        [Display(Name = "Media Folder")]
        public List<TreeViewModel> TreeViewList { get; set; }
        public string TreeViewJson { get; set; }
    }
}

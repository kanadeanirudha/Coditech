using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class AdminRoleMediaFolderActionModel : BaseModel
    {
        public int AdminRoleMediaFolderActionId { get; set; }
        [Required]
        public int AdminRoleMasterId { get; set; }
        public string AdminRoleCode { get; set; }
        public string SanctionPostName { get; set; }
        public string SelectedCentreCode { get; set; }
        public string SelectedDepartmentId { get; set; }
        [Required]
        public List<string> SelectedMediaActions { get; set; }
    }
}

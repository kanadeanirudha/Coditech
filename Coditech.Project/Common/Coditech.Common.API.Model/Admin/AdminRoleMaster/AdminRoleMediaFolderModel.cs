namespace Coditech.Common.API.Model
{
    public class AdminRoleMediaFoldersModel : BaseModel
    {
        public int AdminRoleMasterId { get; set; }
        public string AdminRoleCode { get; set; }
        public string SanctionPostName { get; set; }
        public string SelectedCentreCode { get; set; }
        public string SelectedDepartmentId { get; set; }
        public string SelectedMediaFolderList { get; set; }
        public List<TreeViewModel> TreeViewList { get; set; }
    }
}

namespace Coditech.Common.API.Model
{
    public class AdminRoleMenuDetailsModel : BaseModel
    {
        public int AdminRoleMasterId { get; set; }
        public string AdminRoleCode { get; set; }
        public string SanctionPostName { get; set; }
        public string SelectedMenuList { get; set; }
        public List<UserMenuModel> MenuList { get; set; }

    }
}

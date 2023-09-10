namespace Coditech.Common.API.Model
{
    public class AdminRoleMasterListModel : BaseListModel
    {
        public List<AdminRoleModel> AdminRoleMasterList { get; set; }
        public AdminRoleMasterListModel()
        {
            AdminRoleMasterList = new List<AdminRoleModel>();
        }
    }
}

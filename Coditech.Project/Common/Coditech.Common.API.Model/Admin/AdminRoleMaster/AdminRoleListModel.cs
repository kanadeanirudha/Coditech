namespace Coditech.Common.API.Model
{
    public class AdminRoleListModel : BaseListModel
    {
        public List<AdminRoleModel> AdminRoleList { get; set; }
        public AdminRoleListModel()
        {
            AdminRoleList = new List<AdminRoleModel>();
        }
    }
}

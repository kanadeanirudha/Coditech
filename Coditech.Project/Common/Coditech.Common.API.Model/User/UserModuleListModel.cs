namespace Coditech.Common.API.Model
{
    public class UserModuleListModel : BaseListModel
    {
        public List<UserModuleModel> ModuleList { get; set; }
        public UserModuleListModel()
        {
           ModuleList = new List<UserModuleModel>();
        }
    }
}

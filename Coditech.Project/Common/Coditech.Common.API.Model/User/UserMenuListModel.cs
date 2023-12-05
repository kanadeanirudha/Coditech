namespace Coditech.Common.API.Model
{
    public class UserMenuListModel : BaseListModel
    {
        public List<UserMenuModel> MenuList { get; set; }
        public UserMenuListModel()
        {
            MenuList = new List<UserMenuModel>();
        }

    }
}

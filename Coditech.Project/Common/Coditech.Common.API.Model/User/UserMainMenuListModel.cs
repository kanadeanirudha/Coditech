namespace Coditech.Common.API.Model
{
    public class UserMainMenuListModel : BaseListModel
    {
        public List<UserMainMenuModel> GeneralUserMainMenuList { get; set; }
        public UserMainMenuListModel()
        {
            GeneralUserMainMenuList = new List<UserMainMenuModel>();
        }

    }
}

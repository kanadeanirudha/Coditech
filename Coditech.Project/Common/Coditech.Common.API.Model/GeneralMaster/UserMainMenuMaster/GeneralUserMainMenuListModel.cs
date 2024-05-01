namespace Coditech.Common.API.Model
{
    public class GeneralUserMainMenuListModel : BaseListModel
    {
        public List<GeneralUserMainMenuModel> GeneralUserMainMenuList { get; set; }
        public GeneralUserMainMenuListModel()
        {
            GeneralUserMainMenuList = new List<GeneralUserMainMenuModel>();
        }

    }
}

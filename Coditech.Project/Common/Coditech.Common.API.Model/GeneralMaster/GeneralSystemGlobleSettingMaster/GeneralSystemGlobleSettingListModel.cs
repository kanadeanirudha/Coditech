namespace Coditech.Common.API.Model
{
    public class GeneralSystemGlobleSettingListModel : BaseListModel
    {
        public List<GeneralSystemGlobleSettingModel> GeneralSystemGlobleSettingList { get; set; }
        public GeneralSystemGlobleSettingListModel()
        {
            GeneralSystemGlobleSettingList = new List<GeneralSystemGlobleSettingModel>();
        }

    }
}

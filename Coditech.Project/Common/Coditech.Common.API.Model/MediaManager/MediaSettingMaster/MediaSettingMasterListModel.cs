namespace Coditech.Common.API.Model
{
    public class MediaSettingMasterListModel : BaseListModel
    {
        public List<MediaSettingMasterModel> MediaSettingMasterList { get; set; }
        public MediaSettingMasterListModel()
        {
            MediaSettingMasterList = new List<MediaSettingMasterModel>();
        }

    }
}

namespace Coditech.Common.API.Model
{
    public class CoditechApplicationSettingListModel : BaseListModel
    {
        public List<CoditechApplicationSettingModel> CoditechApplicationSettingList { get; set; }
        public CoditechApplicationSettingListModel()
        {
            CoditechApplicationSettingList = new List<CoditechApplicationSettingModel>();
        }

    }
}

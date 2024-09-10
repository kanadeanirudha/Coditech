namespace Coditech.Common.API.Model
{
    public class GeneralNotificationListModel : BaseListModel
    {
        public List<GeneralNotificationModel> GeneralNotificationList { get; set; }
        public GeneralNotificationListModel()
        {
            GeneralNotificationList = new List<GeneralNotificationModel>();
        }
    }
}

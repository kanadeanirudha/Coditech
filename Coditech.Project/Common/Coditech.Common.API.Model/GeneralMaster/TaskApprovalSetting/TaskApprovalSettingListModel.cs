namespace Coditech.Common.API.Model
{
    public class TaskApprovalSettingListModel : BaseListModel
    {
        public List<TaskApprovalSettingModel> TaskApprovalSettingList { get; set; }
        public TaskApprovalSettingListModel()
        {
            TaskApprovalSettingList = new List<TaskApprovalSettingModel>();
        }

    }
}

namespace Coditech.Common.API.Model
{
    public class GeneralBatchUserListModel : BaseListModel
    {
        public List<GeneralBatchUserModel> GeneralBatchUserList { get; set; }
        public GeneralBatchUserListModel()
        {
            GeneralBatchUserList = new List<GeneralBatchUserModel>();
        }
        public long EntityId { get; set; }
        public string BatchName { get; set; }
        public int GeneralBatchMasterId { get; set; }
    }
}

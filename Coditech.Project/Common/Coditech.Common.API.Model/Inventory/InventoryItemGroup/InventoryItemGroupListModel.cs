namespace Coditech.Common.API.Model
{
    public class InventoryItemGroupListModel : BaseListModel
    {
        public List<InventoryItemGroupModel> InventoryItemGroupList { get; set; }
        public InventoryItemGroupListModel()
        {
            InventoryItemGroupList = new List<InventoryItemGroupModel>();
        }
    }
}

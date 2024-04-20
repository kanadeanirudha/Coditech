namespace Coditech.Common.API.Model
{
    public class InventoryItemModelGroupListModel : BaseListModel
    {
        public List<InventoryItemModelGroupModel> InventoryItemModelGroupList { get; set; }
        public InventoryItemModelGroupListModel()
        {
            InventoryItemModelGroupList = new List<InventoryItemModelGroupModel>();
        }

    }
}

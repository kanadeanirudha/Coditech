namespace Coditech.Common.API.Model
{
    public class InventoryItemTrackingDimensionGroupListModel : BaseListModel
    {
        public List<InventoryItemTrackingDimensionGroupModel> InventoryItemTrackingDimensionGroupList { get; set; }
        public InventoryItemTrackingDimensionGroupListModel()
        {
            InventoryItemTrackingDimensionGroupList = new List<InventoryItemTrackingDimensionGroupModel>();
        }

    }
}

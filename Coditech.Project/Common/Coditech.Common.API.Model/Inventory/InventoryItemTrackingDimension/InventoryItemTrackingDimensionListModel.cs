namespace Coditech.Common.API.Model
{
    public class InventoryItemTrackingDimensionListModel : BaseListModel
    {
        public List<InventoryItemTrackingDimensionModel> InventoryItemTrackingDimensionList { get; set; }
        public InventoryItemTrackingDimensionListModel()
        {
            InventoryItemTrackingDimensionList = new List<InventoryItemTrackingDimensionModel>();
        }

    }
}

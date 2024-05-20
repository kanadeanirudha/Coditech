namespace Coditech.Common.API.Model
{
    public class InventoryItemTrackingDimensionGroupModel : BaseModel
    {
        public InventoryItemTrackingDimensionGroupModel()
        {
            InventoryItemTrackingDimensionGroupMapperList = new List<InventoryItemTrackingDimensionGroupMapperModel>();
        }

        public List<InventoryItemTrackingDimensionGroupMapperModel> InventoryItemTrackingDimensionGroupMapperList { get; set; }
        public int InventoryItemTrackingDimensionGroupId { get; set; }
        public string ItemTrackingDimensionGroupName { get; set; }
        public string ItemTrackingDimensionGroupCode { get; set; }
    }
}

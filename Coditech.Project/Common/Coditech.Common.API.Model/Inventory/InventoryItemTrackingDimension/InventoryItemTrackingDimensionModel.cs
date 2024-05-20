namespace Coditech.Common.API.Model
{
    public class InventoryItemTrackingDimensionModel : BaseModel
    {
        public short InventoryItemTrackingDimensionId { get; set; }
        public string TrackingDimensionName { get; set; }
        public string TrackingDimensionCode { get; set; }
    }
}

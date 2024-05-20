namespace Coditech.API.Data.DataModel.Inventory
{
    public class InventoryItemTrackingDimension
    {
        public short InventoryItemTrackingDimensionId { get; set; }
        public string TrackingDimensionName { get; set; }
        public string TrackingDimensionCode { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

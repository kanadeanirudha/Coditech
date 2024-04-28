namespace Coditech.API.Data
{
    public class InventoryItemTrackingDimension
    {
        public short InventoryItemTrackingDimensionId { get; set; }
        public string TrackingDimensionName { get; set; }
        public string TrackingDimensionCode { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

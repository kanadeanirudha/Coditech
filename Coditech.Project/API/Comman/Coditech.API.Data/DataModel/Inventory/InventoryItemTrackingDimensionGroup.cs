using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class InventoryItemTrackingDimensionGroup
    {
        [Key]
        public int InventoryItemTrackingDimensionGroupId { get; set; }
        public string ItemTrackingDimensionGroupName { get; set; }
        public string ItemTrackingDimensionGroupCode { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}


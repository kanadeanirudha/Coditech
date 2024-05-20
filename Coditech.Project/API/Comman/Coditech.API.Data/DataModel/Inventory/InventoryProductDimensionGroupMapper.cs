using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class InventoryProductDimensionGroupMapper
    {
        [Key]
        public int InventoryProductDimensionGroupMapperId { get; set; }
        public int InventoryProductDimensionGroupId { get; set; }
        public byte InventoryProductDimensionId { get; set; }
        public bool ForPurchase { get; set; }
        public bool ForSale { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}


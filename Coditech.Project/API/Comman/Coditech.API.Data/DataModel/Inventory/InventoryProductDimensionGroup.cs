using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class InventoryProductDimensionGroup
    {
        [Key]
        public int InventoryProductDimensionGroupId { get; set; }
        public string ProductDimensionGroupName { get; set; }
        public string ProductDimensionGroupCode { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}


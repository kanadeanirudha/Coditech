using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class InventoryGeneralItemLine
    {
        [Key]
        public long InventoryGeneralItemLineId { get; set; }
        public int InventoryGeneralItemMasterId { get; set; }
        public string SKU { get; set; }
        public string ItemName { get; set; }
        public string BarCode { get; set; }
        public decimal Price { get; set; }
        public bool IsBaseUom { get; set; }
        public string UomCode { get; set; }
        public string LowerLevelUomCode { get; set; }
        public decimal ConversionFactor { get; set; }
        public bool IsOrderingUnit { get; set; }
        public bool IsSaleUnit { get; set; }
        public bool IsIssueUnit { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Volume { get; set; }
        public string InventoryUOM { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}


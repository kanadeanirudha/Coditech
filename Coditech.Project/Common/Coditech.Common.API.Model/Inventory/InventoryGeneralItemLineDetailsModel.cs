namespace Coditech.Common.API.Model
{
    public class InventoryGeneralItemLineDetailsModel : BaseModel
    {
        public int InventoryGeneralItemMasterId { get; set; }
        public short InventoryCategoryId { get; set; }
        public string ItemNumber { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string HSNSACCode { get; set; }
        public int ProductSubTypeEnumId { get; set; }
        public byte GeneralTaxGroupMasterId { get; set; }
        public long InventoryGeneralItemLineId { get; set; }
        public string SKU { get; set; }
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
    }
}

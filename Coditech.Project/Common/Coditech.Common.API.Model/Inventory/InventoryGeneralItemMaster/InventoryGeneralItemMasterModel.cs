namespace Coditech.Common.API.Model
{
    public class InventoryGeneralItemMasterModel : BaseModel
    {
        public int InventoryGeneralItemMasterId { get; set; }
        public long InventoryGeneralItemLineId { get; set; }
        public short InventoryCategoryId { get; set; }
        public string ItemNumber { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string HSNSACCode { get; set; }
        public int ProductTypeEnumId { get; set; }
        public int ProductSubTypeEnumId { get; set; }
        public byte GeneralTaxGroupMasterId { get; set; }
        public int InventoryModelEnumId { get; set; }
        public int InventoryProductDimentionGroupId { get; set; }
        public int InventoryItemGroupId { get; set; }
        public int InventoryStorageDimentionGroupId { get; set; }
        public int InventoryTrackingDimentionGroupId { get; set; }
        public short InventoryBaseUoMMasterId { get; set; }
        public bool IsActive { get; set; }
        public bool IsBaseUom { get; set; }
    }
}


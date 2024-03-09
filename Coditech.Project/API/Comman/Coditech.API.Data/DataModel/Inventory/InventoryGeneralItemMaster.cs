using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class InventoryGeneralItemMaster
    {
        [Key]
        public int InventoryGeneralItemMasterId { get; set; }
        public short InventoryCategoryId { get; set; }
        public string ItemNumber { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string HSNSACCode { get; set; }
        public int ProductSubTypeEnumId { get; set; }
        public byte GeneralTaxGroupMasterId { get; set; }
        public int InventoryModelEnumId { get; set; }
        public int InventoryProductDimentionGroupId { get; set; }
        public int InventoryItemGroupId { get; set; }
        public int InventoryStorageDimentionGroupId { get; set; }
        public int InventoryTrackingDimentionGroupId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}


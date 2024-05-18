using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class InventoryStorageDimensionGroupMapper
    {
        [Key]
        public int InventoryStorageDimensionGroupMapperId { get; set; }
        public int InventoryStorageDimensionGroupId { get; set; }
        public short InventoryStorageDimensionId { get; set; }
        public bool Active { get; set; }
        public bool BlankReceiptAllowed { get; set; }
        public bool BlankIssueAllowed { get; set; }
        public bool CoveragePlanByDimension { get; set; }
        public bool FinancialInventory { get; set; }
        public bool ForPurchasePrices { get; set; }
        public bool ForSalePrices { get; set; }
        public bool PhysicalInventory { get; set; }
        public bool PrimaryStocking { get; set; }
        public string Reference { get; set; }
        public bool Transfer { get; set; }
        public int DisplayOrder { get; set; }
        public string StorageDimensionName { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

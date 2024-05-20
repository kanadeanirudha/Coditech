using System;
using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class InventoryItemTrackingDimensionGroupMapper
    {
        [Key]
        public int InventoryItemTrackingDimensionGroupMapperId { get; set; }
        public int InventoryItemTrackingDimensionGroupId { get; set; }
        public short InventoryItemTrackingDimensionId { get; set; }
        public bool Active { get; set; }
        public bool ActiveInSalesProcess { get; set; }
        public bool PrimaryStocking { get; set; }
        public bool BlankReceiptAllowed { get; set; }
        public bool BlankIssueAllowed { get; set; }
        public bool PhysicalInventory { get; set; }
        public bool FinancialInventory { get; set; }
        public bool CoveragePlanByDimension { get; set; }
        public bool ForPurchasePrices { get; set; }
        public bool ForSalePrices { get; set; }
        public bool Transfer { get; set; }
        public int DisplayOrder { get; set; } = 0;
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

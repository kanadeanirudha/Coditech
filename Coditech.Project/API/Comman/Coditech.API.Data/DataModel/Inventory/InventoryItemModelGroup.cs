using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class InventoryItemModelGroup
    {
        [Key]
        public short InventoryItemModelGroupId { get; set; }
        public string ItemModelGroupName { get; set; }
        public string ItemModelGroupCode { get; set; }
        public int InventoryModelEnumId { get; set; }
        public bool StockedProduct { get; set; }
        public bool PostPhysicalInventory { get; set; }
        public bool PostFinancialInventory { get; set; }
        public bool IsIncludePhysicalValue { get; set; }
        public bool IsFixedReceiptPrice { get; set; }
        public bool PostDeferredRevenueAccountOnSale { get; set; }
        public bool AccrueLiabilityOnProductReceipt { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

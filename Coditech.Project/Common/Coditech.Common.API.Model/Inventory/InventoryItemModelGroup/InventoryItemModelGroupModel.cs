namespace Coditech.Common.API.Model
{
    public class InventoryItemModelGroupModel : BaseModel
    {
        public InventoryItemModelGroupModel()
        {

        }
        public short InventoryItemModelGroupId { get; set; }
        public string ItemModelGroupName { get; set; }
        public string ItemModelGroupCode { get; set; }
        public int InventoryModelEnum { get; set; }
        public bool StockedProduct { get; set; }
        public bool PostPhysicalInventory { get; set; }
        public bool PostFinancialInventory { get; set; }
        public bool IsIncludePhysicalValue { get; set; }
        public bool IsFixedReceiptPrice { get; set; }
        public bool PostDeferredRevenueAccountOnSale { get; set; }
        public bool AccrueLiabilityOnProductReceipt { get; set; }
    }
}

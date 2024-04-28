namespace Coditech.Common.API.Model
{
    public class InventoryProductDimensionGroupModel : BaseModel
    {
        public InventoryProductDimensionGroupModel()
        {

        }

        public int InventoryProductDimensionGroupId { get; set; }
        public string ProductDimensionGroupName { get; set; }
        public string ProductDimensionGroupCode { get; set; }
        public byte InventoryProductDimensionId { get; set; }
        public bool ForPurchase { get; set; }
        public bool ForSale { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}

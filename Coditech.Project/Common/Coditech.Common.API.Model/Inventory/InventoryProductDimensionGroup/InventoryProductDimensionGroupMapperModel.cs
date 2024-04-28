namespace Coditech.Common.API.Model
{
    public class InventoryProductDimensionGroupMapperModel : BaseModel
    {
        public InventoryProductDimensionGroupMapperModel()
        {
        }
        public int InventoryProductDimensionGroupMapperId { get; set; }
        public int InventoryProductDimensionGroupId { get; set; }
        public byte InventoryProductDimensionId { get; set; }
        public string ProductDimensionName { get; set; }
        public bool ForPurchase { get; set; }
        public bool ForSale { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}

namespace Coditech.Common.API.Model
{
    public class InventoryProductDimensionModel : BaseModel
    {
        public byte InventoryProductDimensionId { get; set; }
        public string ProductDimensionName { get; set; }
        public string ProductDimensionCode { get; set; }
    }
}

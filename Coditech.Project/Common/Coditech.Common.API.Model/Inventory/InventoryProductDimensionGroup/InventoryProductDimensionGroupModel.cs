namespace Coditech.Common.API.Model
{
    public class InventoryProductDimensionGroupModel : BaseModel
    {
        public InventoryProductDimensionGroupModel()
        {
            InventoryProductDimensionGroupMapperList = new List<InventoryProductDimensionGroupMapperModel>();
        }

        public List<InventoryProductDimensionGroupMapperModel> InventoryProductDimensionGroupMapperList { get; set; }
        public int InventoryProductDimensionGroupId { get; set; }
        public string ProductDimensionGroupName { get; set; }
        public string ProductDimensionGroupCode { get; set; }
    }
}

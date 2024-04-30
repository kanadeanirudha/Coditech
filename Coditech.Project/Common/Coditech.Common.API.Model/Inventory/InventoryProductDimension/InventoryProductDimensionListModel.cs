namespace Coditech.Common.API.Model
{
    public class InventoryProductDimensionListModel : BaseListModel
    {
        public List<InventoryProductDimensionModel> InventoryProductDimensionList { get; set; }
        public InventoryProductDimensionListModel()
        {
            InventoryProductDimensionList = new List<InventoryProductDimensionModel>();
        }
    }
}

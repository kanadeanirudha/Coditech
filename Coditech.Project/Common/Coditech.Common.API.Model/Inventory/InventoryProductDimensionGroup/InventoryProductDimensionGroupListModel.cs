namespace Coditech.Common.API.Model
{
    public class InventoryProductDimensionGroupListModel : BaseListModel
    {
        public List<InventoryProductDimensionGroupModel> InventoryProductDimensionGroupList { get; set; }
        public InventoryProductDimensionGroupListModel()
        {
            InventoryProductDimensionGroupList = new List<InventoryProductDimensionGroupModel>();
        }

    }
}

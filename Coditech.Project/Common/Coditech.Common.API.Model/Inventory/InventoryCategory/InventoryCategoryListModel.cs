namespace Coditech.Common.API.Model
{
    public class InventoryCategoryListModel : BaseListModel
    {
        public List<InventoryCategoryModel> InventoryCategoryList { get; set; }
        public InventoryCategoryListModel()
        {
           InventoryCategoryList = new List<InventoryCategoryModel>();
        }

    }
}

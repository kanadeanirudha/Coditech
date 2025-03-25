namespace Coditech.Common.API.Model
{
    public class InventoryCategoryTypeListModel : BaseListModel
    {
        public List<InventoryCategoryTypeModel> InventoryCategoryTypeList { get; set; }
        public InventoryCategoryTypeListModel()
        {
            InventoryCategoryTypeList = new List<InventoryCategoryTypeModel>();
        }
    }
}

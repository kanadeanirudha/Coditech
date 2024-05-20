namespace Coditech.Common.API.Model
{
    public class InventoryUoMMasterListModel : BaseListModel
    {
        public List<InventoryUoMMasterModel> InventoryUoMMasterList { get; set; }
        public InventoryUoMMasterListModel()
        {
            InventoryUoMMasterList = new List<InventoryUoMMasterModel>();
        }
    }
}

namespace Coditech.Common.API.Model
{
    public class InventoryGeneralItemMasterListModel : BaseListModel
    {
        public List<InventoryGeneralItemMasterModel> InventoryGeneralItemMasterList { get; set; }
        public InventoryGeneralItemMasterListModel()
        {
            InventoryGeneralItemMasterList = new List<InventoryGeneralItemMasterModel>();
        }
    }
}

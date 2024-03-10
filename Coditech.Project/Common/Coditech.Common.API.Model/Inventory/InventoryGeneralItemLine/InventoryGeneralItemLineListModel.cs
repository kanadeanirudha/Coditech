namespace Coditech.Common.API.Model
{
    public class InventoryGeneralItemLineListModel : BaseListModel
    {
        public List<InventoryGeneralItemLineModel> InventoryGeneralItemLineList { get; set; }
        public InventoryGeneralItemLineListModel()
        {
            InventoryGeneralItemLineList = new List<InventoryGeneralItemLineModel>();
        }
    }
}

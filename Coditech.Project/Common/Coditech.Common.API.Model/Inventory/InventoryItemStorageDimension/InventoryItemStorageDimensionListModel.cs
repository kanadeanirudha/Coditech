namespace Coditech.Common.API.Model
{
    public class InventoryItemStorageDimensionListModel : BaseListModel
    {
        public List<InventoryItemStorageDimensionModel> InventoryItemStorageDimensionList { get; set; }
        public InventoryItemStorageDimensionListModel()
        {
           InventoryItemStorageDimensionList = new List<InventoryItemStorageDimensionModel>();
        }

    }
}

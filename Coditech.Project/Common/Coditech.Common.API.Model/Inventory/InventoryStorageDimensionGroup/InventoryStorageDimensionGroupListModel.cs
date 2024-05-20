namespace Coditech.Common.API.Model
{
    public class InventoryStorageDimensionGroupListModel : BaseListModel
    {
        public List<InventoryStorageDimensionGroupModel> InventoryStorageDimensionGroupList { get; set; }
        public InventoryStorageDimensionGroupListModel()
        {
            InventoryStorageDimensionGroupList = new List<InventoryStorageDimensionGroupModel>();
        }

    }
}

using Coditech.Common.API.Model.Response;

namespace Coditech.Common.API.Model.Responses.Inventory.InventoryGeneralItemMaster
{
    public class InventoryGeneralItemMasterListResponse : BaseListResponse
    {
        public List<InventoryGeneralItemMasterModel> InventoryGeneralItemMasterList { get; set; }
    }
}

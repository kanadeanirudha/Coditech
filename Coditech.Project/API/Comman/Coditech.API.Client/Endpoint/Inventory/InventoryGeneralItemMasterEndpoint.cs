using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryGeneralItemMasterEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryGeneralItemMaster/GetInventoryGeneralItemMasterList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryGeneralItemMasterAsync() =>
            $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryGeneralItemMaster/CreateInventoryGeneralItemMaster";

        public string GetInventoryGeneralItemMasterAsync(int inventoryGeneralItemMasterId) =>
            $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryGeneralItemMaster/GetInventoryGeneralItemMaster?inventoryGeneralItemMasterId={inventoryGeneralItemMasterId}";
       
        public string UpdateInventoryGeneralItemMasterAsync() =>
               $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryGeneralItemMaster/UpdateInventoryGeneralItemMaster";

        public string DeleteInventoryGeneralItemMasterAsync() =>
                  $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryGeneralItemMaster/DeleteInventoryGeneralItemMaster";

        public string GetGeneralServicesList(string searchText)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryGeneralItemMaster/GetGeneralServicesList?searchText={searchText}";
            return endpoint;
        }
    }
}

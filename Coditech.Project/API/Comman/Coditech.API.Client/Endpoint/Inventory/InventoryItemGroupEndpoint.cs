using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryItemGroupEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryItemGroup/GetInventoryItemGroupList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryItemGroupAsync() =>
            $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryItemGroup/CreateInventoryItemGroup";

        public string GetInventoryItemGroupAsync(short inventoryItemGroupId) =>
            $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryItemGroup/GetInventoryItemGroup?inventoryItemGroupId={inventoryItemGroupId}";

        public string UpdateInventoryItemGroupAsync() =>
               $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryItemGroup/UpdateInventoryItemGroup";

        public string DeleteInventoryItemGroupAsync() =>
                  $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryItemGroup/DeleteInventoryItemGroup";
    }
}

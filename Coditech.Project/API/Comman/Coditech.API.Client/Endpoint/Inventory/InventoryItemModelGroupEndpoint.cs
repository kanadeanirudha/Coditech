using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryItemModelGroupEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryItemModelGroup/GetInventoryItemModelGroupList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryItemModelGroupAsync() =>
            $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryItemModelGroup/CreateInventoryItemModelGroup";

        public string GetInventoryItemModelGroupAsync(short inventoryItemModelGroupId) =>
            $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryItemModelGroup/GetInventoryItemModelGroup?inventoryItemModelGroupId={inventoryItemModelGroupId}";
       
        public string UpdateInventoryItemModelGroupAsync() =>
               $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryItemModelGroup/UpdateInventoryItemModelGroup";

        public string DeleteInventoryItemModelGroupAsync() =>
                  $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryItemModelGroup/DeleteInventoryItemModelGroup";
    }
}

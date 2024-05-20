using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryStorageDimensionGroupEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryStorageDimensionGroup/GetInventoryStorageDimensionGroupList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryStorageDimensionGroupAsync() =>
            $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryStorageDimensionGroup/CreateInventoryStorageDimensionGroup";

        public string GetInventoryStorageDimensionGroupAsync(int inventoryStorageDimensionGroupId) =>
            $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryStorageDimensionGroup/GetInventoryStorageDimensionGroup?inventoryStorageDimensionGroupId={inventoryStorageDimensionGroupId}";
       
        public string UpdateInventoryStorageDimensionGroupAsync() =>
               $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryStorageDimensionGroup/UpdateInventoryStorageDimensionGroup";

        public string DeleteInventoryStorageDimensionGroupAsync() =>
                  $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryStorageDimensionGroup/DeleteInventoryStorageDimensionGroup";
    }
}

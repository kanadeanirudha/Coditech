using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryCategoryEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryCategory/GetInventoryCategoryList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryCategoryAsync() =>
            $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryCategoryMaster/CreateInventoryCategory";

        public string GetInventoryCategoryAsync(short inventoryCategoryId) =>
            $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryCategoryMaster/GetInventoryCategory?InventoryCategoryId={inventoryCategoryId}";
       
        public string UpdateInventoryCategoryAsync() =>
               $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryCategory/UpdateInventoryCategory";

        public string DeleteInventoryCategoryAsync() =>
                  $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryCategory/DeleteInventoryCategory";
    }
}

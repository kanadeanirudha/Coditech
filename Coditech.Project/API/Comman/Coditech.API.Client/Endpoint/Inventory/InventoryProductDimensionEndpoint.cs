using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryProductDimensionEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryProductDimension/GetInventoryProductDimensionList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryProductDimensionAsync() =>
            $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryProductDimension/CreateInventoryProductDimension";

        public string GetInventoryProductDimensionAsync(short inventoryProductDimensionId) =>
            $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryProductDimension/GetInventoryProductDimension?inventoryProductDimensionId={inventoryProductDimensionId}";

        public string UpdateInventoryProductDimensionAsync() =>
               $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryProductDimension/UpdateInventoryProductDimension";

        public string DeleteInventoryProductDimensionAsync() =>
                  $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryProductDimension/DeleteInventoryProductDimension";
    }
}

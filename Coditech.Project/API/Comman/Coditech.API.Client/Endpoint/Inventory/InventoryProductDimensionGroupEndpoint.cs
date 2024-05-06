using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryProductDimensionGroupEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryProductDimensionGroup/GetInventoryProductDimensionGroupList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryProductDimensionGroupAsync() =>
            $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryProductDimensionGroup/CreateInventoryProductDimensionGroup";

        public string GetInventoryProductDimensionGroupAsync(int inventoryProductDimensionGroupId) =>
            $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryProductDimensionGroup/GetInventoryProductDimensionGroup?inventoryProductDimensionGroupId={inventoryProductDimensionGroupId}";
       
        public string UpdateInventoryProductDimensionGroupAsync() =>
               $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryProductDimensionGroup/UpdateInventoryProductDimensionGroup";

        public string DeleteInventoryProductDimensionGroupAsync() =>
                  $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryProductDimensionGroup/DeleteInventoryProductDimensionGroup";
    }
}

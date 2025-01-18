using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryProductDimensionGroupEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryProductDimensionGroup/GetInventoryProductDimensionGroupList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryProductDimensionGroupAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryProductDimensionGroup/CreateInventoryProductDimensionGroup";

        public string GetInventoryProductDimensionGroupAsync(int inventoryProductDimensionGroupId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryProductDimensionGroup/GetInventoryProductDimensionGroup?inventoryProductDimensionGroupId={inventoryProductDimensionGroupId}";
       
        public string UpdateInventoryProductDimensionGroupAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryProductDimensionGroup/UpdateInventoryProductDimensionGroup";

        public string DeleteInventoryProductDimensionGroupAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryProductDimensionGroup/DeleteInventoryProductDimensionGroup";
    }
}

using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryProductDimensionEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryProductDimension/GetInventoryProductDimensionList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryProductDimensionAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryProductDimension/CreateInventoryProductDimension";

        public string GetInventoryProductDimensionAsync(short inventoryProductDimensionId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryProductDimension/GetInventoryProductDimension?inventoryProductDimensionId={inventoryProductDimensionId}";

        public string UpdateInventoryProductDimensionAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryProductDimension/UpdateInventoryProductDimension";

        public string DeleteInventoryProductDimensionAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryProductDimension/DeleteInventoryProductDimension";
    }
}

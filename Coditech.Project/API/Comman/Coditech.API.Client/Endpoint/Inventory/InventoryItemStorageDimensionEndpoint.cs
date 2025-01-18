using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryItemStorageDimensionEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemStorageDimension/GetInventoryItemStorageDimensionList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryItemStorageDimensionAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemStorageDimension/CreateInventoryItemStorageDimension";

        public string GetInventoryItemStorageDimensionAsync(short inventoryItemStorageDimensionId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemStorageDimension/GetInventoryItemStorageDimension?InventoryItemStorageDimensionId={inventoryItemStorageDimensionId}";
       
        public string UpdateInventoryItemStorageDimensionAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemStorageDimension/UpdateInventoryItemStorageDimension";

        public string DeleteInventoryItemStorageDimensionAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemStorageDimension/DeleteInventoryItemStorageDimension";
    }
}

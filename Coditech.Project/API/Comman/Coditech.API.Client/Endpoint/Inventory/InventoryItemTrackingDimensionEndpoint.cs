using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryItemTrackingDimensionEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemTrackingDimension/GetInventoryItemTrackingDimensionList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryItemTrackingDimensionAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemTrackingDimension/CreateInventoryItemTrackingDimension";

        public string GetInventoryItemTrackingDimensionAsync(short inventoryItemTrackingDimensionId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemTrackingDimension/GetInventoryItemTrackingDimension?inventoryItemTrackingDimensionId={inventoryItemTrackingDimensionId}";

        public string UpdateInventoryItemTrackingDimensionAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemTrackingDimension/UpdateInventoryItemTrackingDimension";

        public string DeleteInventoryItemTrackingDimensionAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemTrackingDimension/DeleteInventoryItemTrackingDimension";
    }
}

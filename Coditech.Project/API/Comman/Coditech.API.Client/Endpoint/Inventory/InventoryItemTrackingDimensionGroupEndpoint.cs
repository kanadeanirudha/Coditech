using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryItemTrackingDimensionGroupEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemTrackingDimensionGroup/GetInventoryItemTrackingDimensionGroupList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryItemTrackingDimensionGroupAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemTrackingDimensionGroup/CreateInventoryItemTrackingDimensionGroup";

        public string GetInventoryItemTrackingDimensionGroupAsync(int inventoryItemTrackingDimensionGroupId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemTrackingDimensionGroup/GetInventoryItemTrackingDimensionGroup?inventoryItemTrackingDimensionGroupId={inventoryItemTrackingDimensionGroupId}";
       
        public string UpdateInventoryItemTrackingDimensionGroupAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemTrackingDimensionGroup/UpdateInventoryItemTrackingDimensionGroup";

        public string DeleteInventoryItemTrackingDimensionGroupAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemTrackingDimensionGroup/DeleteInventoryItemTrackingDimensionGroup";
    }
}

using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryItemTrackingDimensionGroupEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryItemTrackingDimensionGroup/GetInventoryItemTrackingDimensionGroupList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryItemTrackingDimensionGroupAsync() =>
            $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryItemTrackingDimensionGroup/CreateInventoryItemTrackingDimensionGroup";

        public string GetInventoryItemTrackingDimensionGroupAsync(int inventoryItemTrackingDimensionGroupId) =>
            $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryItemTrackingDimensionGroup/GetInventoryItemTrackingDimensionGroup?inventoryItemTrackingDimensionGroupId={inventoryItemTrackingDimensionGroupId}";
       
        public string UpdateInventoryItemTrackingDimensionGroupAsync() =>
               $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryItemTrackingDimensionGroup/UpdateInventoryItemTrackingDimensionGroup";

        public string DeleteInventoryItemTrackingDimensionGroupAsync() =>
                  $"{CoditechAdminSettings.CoditechInventoryApiRootUri}/InventoryItemTrackingDimensionGroup/DeleteInventoryItemTrackingDimensionGroup";
    }
}

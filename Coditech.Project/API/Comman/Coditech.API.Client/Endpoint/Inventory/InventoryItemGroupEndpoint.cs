using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryItemGroupEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemGroup/GetInventoryItemGroupList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryItemGroupAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemGroup/CreateInventoryItemGroup";

        public string GetInventoryItemGroupAsync(short inventoryItemGroupId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemGroup/GetInventoryItemGroup?inventoryItemGroupId={inventoryItemGroupId}";

        public string UpdateInventoryItemGroupAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemGroup/UpdateInventoryItemGroup";

        public string DeleteInventoryItemGroupAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemGroup/DeleteInventoryItemGroup";
    }
}

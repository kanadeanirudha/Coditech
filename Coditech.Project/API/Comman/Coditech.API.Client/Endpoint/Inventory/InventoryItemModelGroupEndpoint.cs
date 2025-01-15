using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryItemModelGroupEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemModelGroup/GetInventoryItemModelGroupList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryItemModelGroupAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemModelGroup/CreateInventoryItemModelGroup";

        public string GetInventoryItemModelGroupAsync(short inventoryItemModelGroupId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemModelGroup/GetInventoryItemModelGroup?inventoryItemModelGroupId={inventoryItemModelGroupId}";
       
        public string UpdateInventoryItemModelGroupAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemModelGroup/UpdateInventoryItemModelGroup";

        public string DeleteInventoryItemModelGroupAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryItemModelGroup/DeleteInventoryItemModelGroup";
    }
}

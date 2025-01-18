using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryStorageDimensionGroupEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryStorageDimensionGroup/GetInventoryStorageDimensionGroupList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryStorageDimensionGroupAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryStorageDimensionGroup/CreateInventoryStorageDimensionGroup";

        public string GetInventoryStorageDimensionGroupAsync(int inventoryStorageDimensionGroupId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryStorageDimensionGroup/GetInventoryStorageDimensionGroup?inventoryStorageDimensionGroupId={inventoryStorageDimensionGroupId}";
       
        public string UpdateInventoryStorageDimensionGroupAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryStorageDimensionGroup/UpdateInventoryStorageDimensionGroup";

        public string DeleteInventoryStorageDimensionGroupAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryStorageDimensionGroup/DeleteInventoryStorageDimensionGroup";
    }
}

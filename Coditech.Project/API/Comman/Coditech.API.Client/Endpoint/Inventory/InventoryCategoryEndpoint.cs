using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryCategoryEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryCategory/GetInventoryCategoryList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryCategoryAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryCategoryMaster/CreateInventoryCategory";

        public string GetInventoryCategoryAsync(short inventoryCategoryId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryCategoryMaster/GetInventoryCategory?InventoryCategoryId={inventoryCategoryId}";
       
        public string UpdateInventoryCategoryAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryCategory/UpdateInventoryCategory";

        public string DeleteInventoryCategoryAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryCategory/DeleteInventoryCategory";
    }
}

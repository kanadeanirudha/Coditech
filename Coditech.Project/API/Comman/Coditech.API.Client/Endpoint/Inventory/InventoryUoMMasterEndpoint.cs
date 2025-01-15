using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryUoMMasterEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryUoMMaster/GetInventoryUoMMasterList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateInventoryUoMMasterAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryUoMMaster/CreateInventoryUoMMaster";

        public string GetInventoryUoMMasterAsync(short inventoryUoMMasterId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryUoMMaster/GetInventoryUoMMaster?inventoryUoMMasterId={inventoryUoMMasterId}";

        public string UpdateInventoryUoMMasterAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryUoMMaster/UpdateInventoryUoMMaster";

        public string DeleteInventoryUoMMasterAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryUoMMaster/DeleteInventoryUoMMaster";
    }
}

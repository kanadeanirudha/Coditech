using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class InventoryCategoryTypeEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryCategoryType/GetInventoryCategoryTypeList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateInventoryCategoryTypeAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryCategoryType/CreateInventoryCategoryType";

        public string GetInventoryCategoryTypeAsync(byte inventoryCategoryTypeMasterId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryCategoryType/GetInventoryCategoryType?inventoryCategoryTypeMasterId={inventoryCategoryTypeMasterId}";

        public string UpdateInventoryCategoryTypeAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryCategoryType/UpdateInventoryCategoryType";

        public string DeleteInventoryCategoryTypeAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/InventoryCategoryType/DeleteInventoryCategoryType";
    }
}

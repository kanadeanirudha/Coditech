using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class AdminRoleMasterEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminRoleMaster/GetAdminRoleMasterList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateAdminRoleMasterAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminRoleMaster/CreateAdminRoleMaster";

        public string GetAdminRoleMasterAsync(int adminRoleMasterId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminRoleMaster/GetAdminRoleMaster?adminRoleMasterId={adminRoleMasterId}";
       
        public string UpdateAdminRoleMasterAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminRoleMaster/UpdateAdminRoleMaster";

        public string DeleteAdminRoleMasterAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminRoleMaster/DeleteAdminRoleMaster";
    }
}

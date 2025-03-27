using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralUserModuleEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralUserModuleMaster/GetUserModuleList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateUserModuleAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralUserModuleMaster/CreateUserModule";

        public string GetUserModuleAsync(short userModuleMasterId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralUserModuleMaster/GetUserModule?userModuleMasterId={userModuleMasterId}";
       
        public string UpdateUserModuleAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralUserModuleMaster/UpdateUserModule";

        public string DeleteUserModuleAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralUserModuleMaster/DeleteUserModule";
    }
}

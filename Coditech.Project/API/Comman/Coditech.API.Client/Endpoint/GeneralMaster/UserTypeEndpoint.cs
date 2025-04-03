using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.API.Data;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class UserTypeEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/UserType/GetUserTypeList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateUserTypeAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/UserType/CreateUserType";

        public string GetUserTypeAsync(short userTypeId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/UserType/GetUserType?userTypeId={userTypeId}";
       
        public string UpdateUserTypeAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/UserType/UpdateUserType";

        public string DeleteUserTypeAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/UserType/DeleteUserType";
    }
}

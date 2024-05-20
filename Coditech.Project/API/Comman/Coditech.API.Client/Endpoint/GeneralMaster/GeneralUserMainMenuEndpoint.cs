using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralUserMainMenuEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralUserMainMenuMaster/GetUserMainMenuList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateUserMainMenuAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralUserMainMenuMaster/CreateUserMainMenu";

        public string GetUserMainMenuAsync(short generalUserMainMenuId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralUserMainMenuMaster/GetUserMainMenu?generalUserMainMenuMasterId={generalUserMainMenuId}";
       
        public string UpdateUserMainMenuAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralUserMainMenuMaster/UpdateUserMainMenu";

        public string DeleteUserMainMenuAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralUserMainMenuMaster/DeleteUserMainMenu";
    }
}

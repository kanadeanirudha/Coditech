using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralEnumaratorGroupEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorGroup/GetEnumaratorGroupList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateEnumaratorGroupAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorGroup/CreateEnumaratorGroup";

        public string GetEnumaratorGroupAsync(int GeneralEnumaratorGroupId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorGroup/GetEnumaratorGroup?GeneralEnumaratorGroupId={GeneralEnumaratorGroupId}";
       
        public string UpdateEnumaratorGroupAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorGroup/UpdateEnumaratorGroup";

        public string DeleteEnumaratorGroupAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorGroup/DeleteEnumaratorGroup";
    }
}

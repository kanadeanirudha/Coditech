using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralEnumaratorGroupEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorGroupMaster/GetEnumaratorGroupList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateEnumaratorGroupAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorGroupMaster/CreateEnumaratorGroup";

        public string GetEnumaratorGroupAsync(short EnumaratorGroupId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorGroupMaster/GetEnumaratorGroup?generalEnumaratorGroupMasterId={EnumaratorGroupId}";

        public string UpdateEnumaratorGroupAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorGroupMaster/UpdateEnumaratorGroup";

        public string DeleteEnumaratorGroupAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorGroupMaster/DeleteEnumaratorGroup";
    }
}

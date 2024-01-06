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

        public string GetEnumaratorGroupAsync(int generalEnumaratorGroupId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorGroup/GetEnumaratorGroup?generalEnumaratorGroupId={generalEnumaratorGroupId}";
       
        public string UpdateEnumaratorGroupAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorGroup/UpdateEnumaratorGroup";

        public string DeleteEnumaratorGroupAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorGroup/DeleteEnumaratorGroup";

        public string GetEnumaratorAsync(int generalEnumaratorId) =>
           $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorGroup/GetEnumarator?generalEnumaratorId={generalEnumaratorId}";

        public string InsertUpdateEnumaratorAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorGroup/InsertUpdateEnumarator";

        public string DeleteEnumaratorAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorGroup/DeleteEnumarator";

    }
}

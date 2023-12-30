using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralEnumaratorEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorMaster/GetEnumaratorList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateEnumaratorAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorMaster/CreateEnumarator";

        public string GetEnumaratorAsync(int generalEnumaratorId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorMaster/GetEnumarator?generalEnumaratorMasterId={generalEnumaratorId}";

        public string UpdateEnumaratorAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorMaster/UpdateEnumarator";

        public string DeleteEnumaratorAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEnumaratorMaster/DeleteEnumarator";
    }
}

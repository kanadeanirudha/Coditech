using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralOccupationEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralOccupationMaster/GetOccupationList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateOccupationAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralOccupationMaster/CreateOccupation";

        public string GetOccupationAsync(short generalOccupationId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralOccupationMaster/GetOccupation?generalOccupationMasterId={generalOccupationId}";
       
        public string UpdateOccupationAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralOccupationMaster/UpdateOccupation";

        public string DeleteOccupationAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralOccupationMaster/DeleteOccupation";
    }
}

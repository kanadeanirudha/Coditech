using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralLeadGenerationEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralLeadGenerationMaster/GetLeadGenerationList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateLeadGenerationAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralLeadGenerationMaster/CreateLeadGeneration";

        public string GetLeadGenerationAsync(long generalLeadGenerationId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralLeadGenerationMaster/GetLeadGeneration?generalLeadGenerationMasterId={generalLeadGenerationId}";
       
        public string UpdateLeadGenerationAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralLeadGenerationMaster/UpdateLeadGeneration";

        public string DeleteLeadGenerationAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralLeadGenerationMaster/DeleteLeadGeneration";
    }
}

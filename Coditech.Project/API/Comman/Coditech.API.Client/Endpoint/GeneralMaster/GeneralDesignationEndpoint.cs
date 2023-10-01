using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralDesignationEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralDesignationMaster/GetDesignationList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateDesignationAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralDesignationMaster/CreateDesignation";

        public string GetDesignationAsync(short designationId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralDesignationMaster/GetDesignation?generalDesignationMasterId={designationId}";

        public string UpdateDesignationAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralDesignationMaster/UpdateDesignation";

        public string DeleteDesignationAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralDesignationMaster/DeleteDesignation";
    }
}

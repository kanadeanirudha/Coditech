using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralRegionEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralRegionMaster/GetRegionList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateRegionAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralRegionMaster/CreateRegion";

        public string GetRegionAsync(short generalRegionId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralRegionMaster/GetRegion?generalRegionMasterId={generalRegionId}";

        public string UpdateRegionAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralRegionMaster/UpdateRegion";

        public string DeleteRegionAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralRegionMaster/DeleteRegion";
    }
}

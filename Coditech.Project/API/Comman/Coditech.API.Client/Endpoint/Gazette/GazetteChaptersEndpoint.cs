using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GazetteChaptersEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechGazetteApiRootUri}/GazetteChapters/GetGazetteChaptersList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateGazetteChaptersAsync() =>
            $"{CoditechAdminSettings.CoditechGazetteApiRootUri}/GazetteChapters/CreateGazetteChapters";

        public string GetGazetteChaptersAsync(int gazetteChaptersId) =>
            $"{CoditechAdminSettings.CoditechGazetteApiRootUri}/GazetteChapters/GetGazetteChapters?gazetteChaptersId={gazetteChaptersId}";

        public string UpdateGazetteChaptersAsync() =>
               $"{CoditechAdminSettings.CoditechGazetteApiRootUri}/GazetteChapters/UpdateGazetteChapters";

        public string DeleteGazetteChaptersAsync() =>
                  $"{CoditechAdminSettings.CoditechGazetteApiRootUri}/GazetteChapters/DeleteGazetteChapters";

        public string GetGazetteChaptersByDistrictWise(Int16 generalDistrictMasterId)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechGazetteApiRootUri}/GazetteChapters/GetGazetteChaptersByDistrictWise?generalDistrictMasterId={generalDistrictMasterId}";
            return endpoint;
        }
    }
}

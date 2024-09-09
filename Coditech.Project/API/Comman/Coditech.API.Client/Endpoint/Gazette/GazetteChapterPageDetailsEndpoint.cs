using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GazetteChaptersPageDetailEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechGazetteApiRootUri}/GazetteChapterPageDetails/GetGazetteChaptersPageDetailList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateGazetteChaptersPageDetailAsync() =>
            $"{CoditechAdminSettings.CoditechGazetteApiRootUri}/GazetteChapterPageDetails/CreateGazetteChaptersPageDetail";

        public string GetGazetteChaptersPageDetailAsync(int gazetteChaptersPageDetailId) =>
            $"{CoditechAdminSettings.CoditechGazetteApiRootUri}/GazetteChapterPageDetails/GetGazetteChaptersPageDetail?gazetteChaptersPageDetailId={gazetteChaptersPageDetailId}";

        public string UpdateGazetteChaptersPageDetailAsync() =>
               $"{CoditechAdminSettings.CoditechGazetteApiRootUri}/GazetteChapterPageDetails/UpdateGazetteChaptersPageDetail";

        public string DeleteGazetteChaptersPageDetailAsync() =>
                  $"{CoditechAdminSettings.CoditechGazetteApiRootUri}/GazetteChapterPageDetails/DeleteGazetteChaptersPageDetail";

    }
}

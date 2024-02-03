using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralRunningNumbersEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralRunningNumbers/GetRunningNumbersList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateRunningNumbersAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralRunningNumbers/CreateRunningNumbers";

        public string GetRunningNumbersAsync(long generalRunningNumberId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralRunningNumbers/GetRunningNumbers?generalRunningNumberId={generalRunningNumberId}";

        public string UpdateRunningNumbersAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralRunningNumbers/UpdateRunningNumbers";

        public string DeleteRunningNumbersAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralRunningNumbers/DeleteRunningNumbers";
    }
}

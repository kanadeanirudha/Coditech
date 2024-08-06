using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class LogMessageEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/LogMessage/GetLogMessageList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string GetLogMessageAsync(long logMessageId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/LogMessage/GetLogMessage?logMessageId={logMessageId}";

        public string DeleteLogMessageAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/LogMessage/DeleteLogMessage";
    }
}

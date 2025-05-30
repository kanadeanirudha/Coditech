using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class AccountReportEndpoint : BaseEndpoint
    {
        public string GetBalanceSheetReportListAsync(string selectedCentreCode, string selectedParameter1, string selectedParameter2, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccountReport/GetBalanceSheetReportList?selectedCentreCode={selectedCentreCode}&selectedParameter1={selectedParameter1}&selectedParameter2={selectedParameter2}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string GetProfitAndLossReportListAsync(string selectedCentreCode, string selectedParameter1, string selectedParameter2, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccountReport/GetProfitAndLossReportList?selectedCentreCode={selectedCentreCode}&selectedParameter1={selectedParameter1}&selectedParameter2={selectedParameter2}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
    }
}

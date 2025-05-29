using Coditech.API.Endpoint;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Newtonsoft.Json;

namespace Coditech.API.Client
{
    public class AccountReportClient : BaseClient, IAccountReportClient
    {
        AccountReportEndpoint accountReportEndpoint = null;
        public AccountReportClient()
        {
            accountReportEndpoint = new AccountReportEndpoint();
        }
        public virtual AccountBalanceSheetReportListResponse GetBalanceSheetReportList(string SelectedCentreCode, string SelectedParameter1, string SelectedParameter2, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await GetBalanceSheetReportListAsync(SelectedCentreCode, SelectedParameter1, SelectedParameter2, expand, filter, sort, pageIndex, pageSize, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AccountBalanceSheetReportListResponse> GetBalanceSheetReportListAsync(string SelectedCentreCode, string SelectedParameter1, string SelectedParameter2, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = accountReportEndpoint.GetBalanceSheetReportListAsync(SelectedCentreCode, SelectedParameter1, SelectedParameter2, expand, filter, sort, pageIndex, pageSize);
            HttpResponseMessage response = null;
            var disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();

                response = await GetResourceFromEndpointAsync(endpoint, status, cancellationToken).ConfigureAwait(false);
                Dictionary<string, IEnumerable<string>> headers_ = BindHeaders(response);
                var status_ = (int)response.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse = await ReadObjectResponseAsync<AccountBalanceSheetReportListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new AccountBalanceSheetReportListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AccountBalanceSheetReportListResponse typedBody = JsonConvert.DeserializeObject<AccountBalanceSheetReportListResponse>(responseData);
                    UpdateApiStatus(typedBody, status, response);
                    throw new CoditechException(status.ErrorCode, status.ErrorMessage, status.StatusCode);
                }
            }
            finally
            {
                if (disposeResponse)
                    response.Dispose();
            }
        }
    }
}

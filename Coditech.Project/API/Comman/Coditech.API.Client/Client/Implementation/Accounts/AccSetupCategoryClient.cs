using Coditech.API.Endpoint;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Exceptions;
using Newtonsoft.Json;

namespace Coditech.API.Client
{
    public class AccSetupCategoryClient : BaseClient, IAccSetupCategoryClient
    {
        AccSetupCategoryEndpoint accSetupCategoryEndpoint = null;
        public AccSetupCategoryClient()
        {
            accSetupCategoryEndpoint = new AccSetupCategoryEndpoint();
        }

        public virtual AccSetupCategoryListResponse GetAccSetupCategory()
        {
            return Task.Run(async () => await GetAccSetupCategoryAsync(System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AccSetupCategoryListResponse> GetAccSetupCategoryAsync( System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = accSetupCategoryEndpoint.GetAccSetupCategoryAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<AccSetupCategoryListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new AccSetupCategoryListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AccSetupCategoryListResponse typedBody = JsonConvert.DeserializeObject<AccSetupCategoryListResponse>(responseData);
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

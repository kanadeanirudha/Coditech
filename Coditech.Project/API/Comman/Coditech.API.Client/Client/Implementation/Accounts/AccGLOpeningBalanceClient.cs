using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Newtonsoft.Json;

namespace Coditech.API.Client
{
    public class AccGLOpeningBalanceClient : BaseClient, IAccGLOpeningBalanceClient
    {
        AccGLOpeningBalanceEndpoint accGLOpeningBalanceEndpoint = null;
        public AccGLOpeningBalanceClient()
        {
            accGLOpeningBalanceEndpoint = new AccGLOpeningBalanceEndpoint();
        }

        public virtual ACCGLOpeningBalanceListResponse GetNonControlHeadType(int accSetupBalanceSheetId, short accSetupCategoryId, byte controlNonControl)
        {
            return Task.Run(async () => await GetNonControlHeadTypeAsync(accSetupBalanceSheetId, accSetupCategoryId, controlNonControl, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<ACCGLOpeningBalanceListResponse> GetNonControlHeadTypeAsync(int accSetupBalanceSheetId, short accSetupCategoryId, byte controlNonControl, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = accGLOpeningBalanceEndpoint.GetNonControlHeadTypeAsync(accSetupBalanceSheetId, accSetupCategoryId, controlNonControl);
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
                    var objectResponse = await ReadObjectResponseAsync<ACCGLOpeningBalanceListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new ACCGLOpeningBalanceListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    ACCGLOpeningBalanceListResponse typedBody = JsonConvert.DeserializeObject<ACCGLOpeningBalanceListResponse>(responseData);
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

        public virtual ACCGLOpeningBalanceResponse UpdateNonControlHeadType(ACCGLOpeningBalanceModel body)
        {
            return Task.Run(async () => await UpdateNonControlHeadTypeAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<ACCGLOpeningBalanceResponse> UpdateNonControlHeadTypeAsync(ACCGLOpeningBalanceModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = accGLOpeningBalanceEndpoint.UpdateNonControlHeadTypeAsync();
            HttpResponseMessage response = null;
            var disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();
                response = await PutResourceToEndpointAsync(endpoint, JsonConvert.SerializeObject(body), status, cancellationToken).ConfigureAwait(false);
                var headers_ = BindHeaders(response);
                var status_ = (int)response.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse = await ReadObjectResponseAsync<ACCGLOpeningBalanceResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<ACCGLOpeningBalanceResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    ACCGLOpeningBalanceResponse typedBody = JsonConvert.DeserializeObject<ACCGLOpeningBalanceResponse>(responseData);
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

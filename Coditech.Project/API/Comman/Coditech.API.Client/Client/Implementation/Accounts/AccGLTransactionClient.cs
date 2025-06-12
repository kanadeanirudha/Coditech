using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Coditech.API.Client
{
    public class AccGLTransactionClient : BaseClient, IAccGLTransactionClient
    {
        AccGLTransactionEndpoint accGLTransactionEndpoint = null;

        public AccGLTransactionClient()
        {
            accGLTransactionEndpoint = new AccGLTransactionEndpoint();
        }
        public AccGLTransactionListResponse GetAccSetupGLAccountList(string searchKeyword, int accSetupGLId, string userType, string transactionTypeCode, int balanceSheet)
        {
            return Task.Run(async () =>
                await GetAccSetupGLAccountListAsync(searchKeyword, accSetupGLId, userType, transactionTypeCode, balanceSheet, CancellationToken.None)
            ).GetAwaiter().GetResult();
        }
        public async Task<AccGLTransactionListResponse> GetAccSetupGLAccountListAsync(string searchKeyword, int accSetupGLId, string userType, string transactionTypeCode, int balanceSheet, CancellationToken cancellationToken)
        {
            string endpoint = accGLTransactionEndpoint.GetAccSetupGLAccountListAsync(searchKeyword, accSetupGLId, userType, transactionTypeCode, balanceSheet);
            HttpResponseMessage response = null;
            bool disposeResponse = true;

            try
            {
                ApiStatus status = new ApiStatus();
                response = await GetResourceFromEndpointAsync(endpoint, status, cancellationToken).ConfigureAwait(false);
                Dictionary<string, IEnumerable<string>> headers = BindHeaders(response);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var objectResponse = await ReadObjectResponseAsync<AccGLTransactionListResponse>(response, headers, cancellationToken).ConfigureAwait(false);
                    return objectResponse.Object ?? new AccGLTransactionListResponse();
                }
                else
                {
                    string responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AccGLTransactionListResponse typedBody = JsonConvert.DeserializeObject<AccGLTransactionListResponse>(responseData);
                    UpdateApiStatus(typedBody, status, response);
                    throw new CoditechException(status.ErrorCode, status.ErrorMessage, status.StatusCode);
                }
            }
            finally
            {
                if (disposeResponse)
                    response?.Dispose();
            }
        }
        public AccGLTransactionListResponse GetPersons(string searchKeyword, int userTypeId, int balanceSheet)
        {
            return Task.Run(async () =>
                await GetPersonsAsync(searchKeyword, userTypeId, balanceSheet, CancellationToken.None)
            ).GetAwaiter().GetResult();
        }
        public async Task<AccGLTransactionListResponse> GetPersonsAsync(string searchKeyword, int userTypeId, int balanceSheet, CancellationToken cancellationToken)
        {
            string endpoint = accGLTransactionEndpoint.GetPersonsAsync(searchKeyword, userTypeId, balanceSheet);
            HttpResponseMessage response = null;
            bool disposeResponse = true;

            try
            {
                ApiStatus status = new ApiStatus();
                response = await GetResourceFromEndpointAsync(endpoint, status, cancellationToken).ConfigureAwait(false);
                Dictionary<string, IEnumerable<string>> headers = BindHeaders(response);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var objectResponse = await ReadObjectResponseAsync<AccGLTransactionListResponse>(response, headers, cancellationToken).ConfigureAwait(false);
                    return objectResponse.Object ?? new AccGLTransactionListResponse();
                }
                else
                {
                    string responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AccGLTransactionListResponse typedBody = JsonConvert.DeserializeObject<AccGLTransactionListResponse>(responseData);
                    UpdateApiStatus(typedBody, status, response);
                    throw new CoditechException(status.ErrorCode, status.ErrorMessage, status.StatusCode);
                }
            }
            finally
            {
                if (disposeResponse)
                    response?.Dispose();
            }
        }
        public virtual AccGLTransactionResponse CreateGLTransaction(AccGLTransactionModel body)
        {
            return Task.Run(async () => await CreateGLTransactionAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AccGLTransactionResponse> CreateGLTransactionAsync(AccGLTransactionModel body, CancellationToken cancellationToken)
        {
            string endpoint = accGLTransactionEndpoint.CreateGLTransactionAsync();
            HttpResponseMessage response = null;
            bool disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();
                response = await PostResourceToEndpointAsync(endpoint, JsonConvert.SerializeObject(body), status, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                Dictionary<string, IEnumerable<string>> dictionary = BindHeaders(response);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        {
                            ObjectResponseResult<AccGLTransactionResponse> objectResponseResult2 = await ReadObjectResponseAsync<AccGLTransactionResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<AccGLTransactionResponse> objectResponseResult = await ReadObjectResponseAsync<AccGLTransactionResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            AccGLTransactionResponse result = JsonConvert.DeserializeObject<AccGLTransactionResponse>(value);
                            UpdateApiStatus(result, status, response);
                            throw new CoditechException(status.ErrorCode, status.ErrorMessage, status.StatusCode);
                        }
                }
            }
            finally
            {
                if (disposeResponse)
                {
                    response.Dispose();
                }
            }
        }



    }
}

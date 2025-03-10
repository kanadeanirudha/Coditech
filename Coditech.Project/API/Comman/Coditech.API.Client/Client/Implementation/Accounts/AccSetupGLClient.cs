using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Newtonsoft.Json;
using System.Net;
namespace Coditech.API.Client
{
    public class AccSetupGLClient : BaseClient, IAccSetupGLClient
    {
        AccSetupGLEndpoint accSetupGLEndpoint = null;
        public AccSetupGLClient()
        {
            accSetupGLEndpoint = new AccSetupGLEndpoint();
        }

        //Get AccSetupBalanceSheet
        public virtual AccSetupGLResponse GetAccSetupGLTree(string selectedcentreCode, byte accSetupBalanceSheetTypeId, int accSetupBalanceSheetId)
        {
            return Task.Run(async () => await GetAccSetupGLTreeAsync(selectedcentreCode, accSetupBalanceSheetTypeId, accSetupBalanceSheetId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AccSetupGLResponse> GetAccSetupGLTreeAsync(string selectedcentreCode, byte accSetupBalanceSheetTypeId, int accSetupBalanceSheetId, System.Threading.CancellationToken cancellationToken)
        {
            if (accSetupBalanceSheetId <= 0)
                throw new System.ArgumentNullException("AccSetupBalanceSheetId");

            string endpoint = accSetupGLEndpoint.GetAccSetupGLTreeAsync(selectedcentreCode, accSetupBalanceSheetTypeId, accSetupBalanceSheetId);
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
                    var objectResponse = await ReadObjectResponseAsync<AccSetupGLResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new AccSetupGLResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AccSetupGLResponse typedBody = JsonConvert.DeserializeObject<AccSetupGLResponse>(responseData);
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
        public virtual AccSetupGLResponse CreateAccountSetupGL(AccSetupGLModel body)
        {
            return Task.Run(async () => await CreateAccountSetupGLAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AccSetupGLResponse> CreateAccountSetupGLAsync(AccSetupGLModel body, CancellationToken cancellationToken)
        {
            string endpoint = accSetupGLEndpoint.CreateAccountSetupGLAsync();
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
                            ObjectResponseResult<AccSetupGLResponse> objectResponseResult2 = await ReadObjectResponseAsync<AccSetupGLResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<AccSetupGLResponse> objectResponseResult = await ReadObjectResponseAsync<AccSetupGLResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            AccSetupGLResponse result = JsonConvert.DeserializeObject<AccSetupGLResponse>(value);
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
        public virtual AccSetupGLResponse GetAccountSetupGL(int accSetupGLId)
        {
            return Task.Run(async () => await GetAccountSetupGLAsync(accSetupGLId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AccSetupGLResponse> GetAccountSetupGLAsync(int accSetupGLId, System.Threading.CancellationToken cancellationToken)
        {
            if (accSetupGLId <= 0)
                throw new System.ArgumentNullException("accSetupGLId");

            string endpoint = accSetupGLEndpoint.GetAccountSetupGLAsync(accSetupGLId);
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
                    var objectResponse = await ReadObjectResponseAsync<AccSetupGLResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new AccSetupGLResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AccSetupGLResponse typedBody = JsonConvert.DeserializeObject<AccSetupGLResponse>(responseData);
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

        public virtual AccSetupGLResponse UpdateAccountSetupGL(AccSetupGLModel body)
        {
            return Task.Run(async () => await UpdateAccountSetupGLAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AccSetupGLResponse> UpdateAccountSetupGLAsync(AccSetupGLModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = accSetupGLEndpoint.UpdateAccountSetupGLAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<AccSetupGLResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<AccSetupGLResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AccSetupGLResponse typedBody = JsonConvert.DeserializeObject<AccSetupGLResponse>(responseData);
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

        public virtual AccSetupGLResponse AddChild(AccSetupGLModel body)
        {
            return Task.Run(async () => await AddChildAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AccSetupGLResponse> AddChildAsync(AccSetupGLModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = accSetupGLEndpoint.AddChildAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<AccSetupGLResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<AccSetupGLResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AccSetupGLResponse typedBody = JsonConvert.DeserializeObject<AccSetupGLResponse>(responseData);
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
        public virtual TrueFalseResponse DeleteAccountSetupGL(ParameterModel body)
        {
            return Task.Run(async () => await DeleteAccountSetupGLAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteAccountSetupGLAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = accSetupGLEndpoint.DeleteAccountSetupGLAsync();
            HttpResponseMessage response = null;
            var disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();

                response = await PostResourceToEndpointAsync(endpoint, JsonConvert.SerializeObject(body), status, cancellationToken).ConfigureAwait(false);

                var headers_ = BindHeaders(response);
                var status_ = (int)response.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse = await ReadObjectResponseAsync<TrueFalseResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    TrueFalseResponse typedBody = JsonConvert.DeserializeObject<TrueFalseResponse>(responseData);
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

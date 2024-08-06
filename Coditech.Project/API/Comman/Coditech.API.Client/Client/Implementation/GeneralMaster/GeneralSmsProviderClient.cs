using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Newtonsoft.Json;
using System.Net;

namespace Coditech.API.Client
{
    public class GeneralSmsProviderClient : BaseClient, IGeneralSmsProviderClient
    {
        GeneralSmsProviderEndpoint generalSmsProviderEndPoint = null;

        public GeneralSmsProviderClient()
        {
            generalSmsProviderEndPoint = new GeneralSmsProviderEndpoint();
        }

        public virtual GeneralSmsProviderListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralSmsProviderListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageindex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = generalSmsProviderEndPoint.ListAsync(expand, filter, sort, pageindex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralSmsProviderListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new GeneralSmsProviderListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralSmsProviderListResponse typeBody = JsonConvert.DeserializeObject<GeneralSmsProviderListResponse>(responseData);
                    UpdateApiStatus(typeBody, status, response);
                    throw new CoditechException(status.ErrorCode, status.ErrorMessage, status.StatusCode);
                }
            }
            finally
            {
                if (disposeResponse)
                    response.Dispose();
            }

        }
        public virtual GeneralSmsProviderResponse CreateSmsProvider(GeneralSmsProviderModel body)
        {
            return Task.Run(async () => await CreateSmsProviderAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<GeneralSmsProviderResponse> CreateSmsProviderAsync(GeneralSmsProviderModel body, CancellationToken cancellationToken)
        {
            string endpoint = generalSmsProviderEndPoint.CreateSmsProviderAsync();
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
                            ObjectResponseResult<GeneralSmsProviderResponse> objectResponseResult2 = await ReadObjectResponseAsync<GeneralSmsProviderResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);

                            }
                            return objectResponseResult2.Object;

                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<GeneralSmsProviderResponse> objectResponseResult = await ReadObjectResponseAsync<GeneralSmsProviderResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);

                            }
                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            GeneralSmsProviderResponse result = JsonConvert.DeserializeObject<GeneralSmsProviderResponse>(value);
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

        public virtual GeneralSmsProviderResponse GetSmsProvider(short generalSmsProviderId)
        {
            return Task.Run(async () => await GetSmsProviderAsync(generalSmsProviderId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<GeneralSmsProviderResponse> GetSmsProviderAsync(short generalSmsProviderId, System.Threading.CancellationToken cancellationToken)
        {
            if (generalSmsProviderId <= 0)
                throw new System.ArgumentOutOfRangeException("generalSmsProviderId");

             String endpoint = generalSmsProviderEndPoint.GetSmsProviderAsync(generalSmsProviderId);
            HttpResponseMessage response = null;
            var disposeresponse = true;
            try
            {
                ApiStatus status = new ApiStatus();

                response = await GetResourceFromEndpointAsync(endpoint, status, cancellationToken).ConfigureAwait(false);
                Dictionary<string, IEnumerable<string>> headers_ = BindHeaders(response);
                var status_ = (int)response.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralSmsProviderResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new GeneralSmsProviderResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralSmsProviderResponse typeBody = JsonConvert.DeserializeObject<GeneralSmsProviderResponse>(responseData);
                    UpdateApiStatus(typeBody, status, response);
                    throw new CoditechException(status.ErrorCode, status.ErrorMessage, status.StatusCode);
                }
            }
            finally
            {
                if (disposeresponse)
                    response.Dispose();
            }

        }

        public virtual GeneralSmsProviderResponse UpdateSmsProvider(GeneralSmsProviderModel body)
        {
            return Task.Run(async () => await UpdateSmsProviderAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();

        }
        public virtual async Task<GeneralSmsProviderResponse> UpdateSmsProviderAsync(GeneralSmsProviderModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = generalSmsProviderEndPoint.UpdateSmsProviderAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralSmsProviderResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralSmsProviderResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralSmsProviderResponse typedBody = JsonConvert.DeserializeObject<GeneralSmsProviderResponse>(responseData);
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
        public virtual async Task<GeneralSmsProviderResponse> UpdateGeneralSmsProviderAsync(GeneralSmsProviderModel body, System.Threading.CancellationToken cancellationToken)
        {

            string endpoint = generalSmsProviderEndPoint.UpdateSmsProviderAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralSmsProviderResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralSmsProviderResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralSmsProviderResponse typedBody = JsonConvert.DeserializeObject<GeneralSmsProviderResponse>(responseData);
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
        public virtual TrueFalseResponse DeleteSmsProvider(ParameterModel body)
        {
            return Task.Run(async () => await DeleteSmsProviderAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteSmsProviderAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = generalSmsProviderEndPoint.DeleteSmsProviderAsync();
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

        


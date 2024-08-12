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
    public class GeneralWhatsAppProviderClient : BaseClient, IGeneralWhatsAppProviderClient
    {
        GeneralWhatsAppProviderEndpoint generalWhatsAppProviderEndPoint = null;
        public GeneralWhatsAppProviderClient()
        {
            generalWhatsAppProviderEndPoint = new GeneralWhatsAppProviderEndpoint();
        }
        public virtual GeneralWhatsAppProviderListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralWhatsAppProviderListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageindex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = generalWhatsAppProviderEndPoint.ListAsync(expand, filter, sort, pageindex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralWhatsAppProviderListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new GeneralWhatsAppProviderListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralWhatsAppProviderListResponse typeBody = JsonConvert.DeserializeObject<GeneralWhatsAppProviderListResponse>(responseData);
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
        public virtual GeneralWhatsAppProviderResponse CreateWhatsAppProvider(GeneralWhatsAppProviderModel body)
        {
            return Task.Run(async () => await CreateWhatsAppProviderAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<GeneralWhatsAppProviderResponse> CreateWhatsAppProviderAsync(GeneralWhatsAppProviderModel body, CancellationToken cancellationToken)
        {
            string endpoint = generalWhatsAppProviderEndPoint.CreateWhatsAppProviderAsync();
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
                            ObjectResponseResult<GeneralWhatsAppProviderResponse> objectResponseResult2 = await ReadObjectResponseAsync<GeneralWhatsAppProviderResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);

                            }
                            return objectResponseResult2.Object;

                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<GeneralWhatsAppProviderResponse> objectResponseResult = await ReadObjectResponseAsync<GeneralWhatsAppProviderResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);

                            }
                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            GeneralWhatsAppProviderResponse result = JsonConvert.DeserializeObject<GeneralWhatsAppProviderResponse>(value);
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
        public virtual GeneralWhatsAppProviderResponse GetWhatsAppProvider(short generalWhatsAppProviderId)
        {
            return Task.Run(async () => await GetWhatsAppProviderAsync(generalWhatsAppProviderId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<GeneralWhatsAppProviderResponse> GetWhatsAppProviderAsync(short generalWhatsAppProviderId, System.Threading.CancellationToken cancellationToken)
        {
            if (generalWhatsAppProviderId <= 0)
                throw new System.ArgumentOutOfRangeException("generalWhatsAppProviderId");

            String endpoint = generalWhatsAppProviderEndPoint.GetWhatsAppProviderAsync(generalWhatsAppProviderId);
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralWhatsAppProviderResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new GeneralWhatsAppProviderResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralWhatsAppProviderResponse typeBody = JsonConvert.DeserializeObject<GeneralWhatsAppProviderResponse>(responseData);
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

        public virtual GeneralWhatsAppProviderResponse UpdateWhatsAppProvider(GeneralWhatsAppProviderModel body)
        {
            return Task.Run(async () => await UpdateWhatsAppProviderAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();

        }
        public virtual async Task<GeneralWhatsAppProviderResponse> UpdateWhatsAppProviderAsync(GeneralWhatsAppProviderModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = generalWhatsAppProviderEndPoint.UpdateWhatsAppProviderAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralWhatsAppProviderResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralWhatsAppProviderResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralWhatsAppProviderResponse typedBody = JsonConvert.DeserializeObject<GeneralWhatsAppProviderResponse>(responseData);
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
        public virtual TrueFalseResponse DeleteWhatsAppProvider(ParameterModel body)
        {
            return Task.Run(async () => await DeleteWhatsAppProviderAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteWhatsAppProviderAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = generalWhatsAppProviderEndPoint.DeleteWhatsAppProviderAsync();
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

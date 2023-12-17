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
    public class GeneralSystemGlobleSettingClient : BaseClient, IGeneralSystemGlobleSettingClient
    {
        GeneralSystemGlobleSettingEndpoint generalSystemGlobleSettingClientEndpoint = null;
        public GeneralSystemGlobleSettingClient()
        {
            generalSystemGlobleSettingClientEndpoint = new GeneralSystemGlobleSettingEndpoint();
        }
        public virtual GeneralSystemGlobleSettingListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralSystemGlobleSettingListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = generalSystemGlobleSettingClientEndpoint.ListAsync(expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralSystemGlobleSettingListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new GeneralSystemGlobleSettingListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralSystemGlobleSettingListResponse typedBody = JsonConvert.DeserializeObject<GeneralSystemGlobleSettingListResponse>(responseData);
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

        public virtual GeneralSystemGlobleSettingResponse CreateSystemGlobleSetting(GeneralSystemGlobleSettingModel body)
        {
            return Task.Run(async () => await CreateSystemGlobleSettingAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralSystemGlobleSettingResponse> CreateSystemGlobleSettingAsync(GeneralSystemGlobleSettingModel body, CancellationToken cancellationToken)
        {
            string endpoint = generalSystemGlobleSettingClientEndpoint.CreateSystemGlobleSettingAsync();
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
                            ObjectResponseResult<GeneralSystemGlobleSettingResponse> objectResponseResult2 = await ReadObjectResponseAsync<GeneralSystemGlobleSettingResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<GeneralSystemGlobleSettingResponse> objectResponseResult = await ReadObjectResponseAsync<GeneralSystemGlobleSettingResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            GeneralSystemGlobleSettingResponse result = JsonConvert.DeserializeObject<GeneralSystemGlobleSettingResponse>(value);
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

        public virtual GeneralSystemGlobleSettingResponse GetSystemGlobleSetting(short generalSystemGlobleSettingId)
        {
            return Task.Run(async () => await GetSystemGlobleSettingAsync(generalSystemGlobleSettingId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralSystemGlobleSettingResponse> GetSystemGlobleSettingAsync(short generalSystemGlobleSettingId, System.Threading.CancellationToken cancellationToken)
        {
            if (generalSystemGlobleSettingId <= 0)
                throw new System.ArgumentNullException("generalSystemGlobleSettingId");

            string endpoint = generalSystemGlobleSettingClientEndpoint.GetSystemGlobleSettingAsync(generalSystemGlobleSettingId);
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralSystemGlobleSettingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new GeneralSystemGlobleSettingResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralSystemGlobleSettingResponse typedBody = JsonConvert.DeserializeObject<GeneralSystemGlobleSettingResponse>(responseData);
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

        public virtual GeneralSystemGlobleSettingResponse UpdateSystemGlobleSetting(GeneralSystemGlobleSettingModel body)
        {
            return Task.Run(async () => await UpdateSystemGlobleSettingAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralSystemGlobleSettingResponse> UpdateSystemGlobleSettingAsync(GeneralSystemGlobleSettingModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = generalSystemGlobleSettingClientEndpoint.UpdateSystemGlobleSettingAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralSystemGlobleSettingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralSystemGlobleSettingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralSystemGlobleSettingResponse typedBody = JsonConvert.DeserializeObject<GeneralSystemGlobleSettingResponse>(responseData);
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

        public virtual TrueFalseResponse DeleteSystemGlobleSetting(ParameterModel body)
        {
            return Task.Run(async () => await DeleteSystemGlobleSettingAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteSystemGlobleSettingAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = generalSystemGlobleSettingClientEndpoint.DeleteSystemGlobleSettingAsync();
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

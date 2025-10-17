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
    public class GeneralNotificationClient : BaseClient, IGeneralNotificationClient
    {
        GeneralNotificationEndpoint generalNotificationEndPoint = null;
        public GeneralNotificationClient()
        {
            generalNotificationEndPoint = new GeneralNotificationEndpoint();
        }
        public virtual GeneralNotificationListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
           return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralNotificationListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageindex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = generalNotificationEndPoint.ListAsync(expand, filter, sort, pageindex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralNotificationListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new GeneralNotificationListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralNotificationListResponse typeBody = JsonConvert.DeserializeObject<GeneralNotificationListResponse>(responseData);
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
        public virtual GeneralNotificationResponse CreateNotification(GeneralNotificationModel body)
        {
            return Task.Run(async () => await CreateNotificationAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<GeneralNotificationResponse> CreateNotificationAsync(GeneralNotificationModel body, CancellationToken cancellationToken)
        {
            string endpoint = generalNotificationEndPoint.CreateNotificationAsync();
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
                            ObjectResponseResult<GeneralNotificationResponse> objectResponseResult2 = await ReadObjectResponseAsync<GeneralNotificationResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);

                            }
                            return objectResponseResult2.Object;

                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<GeneralNotificationResponse> objectResponseResult = await ReadObjectResponseAsync<GeneralNotificationResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);

                            }
                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            GeneralNotificationResponse result = JsonConvert.DeserializeObject<GeneralNotificationResponse>(value);
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
        public virtual GeneralNotificationResponse GetNotification(long generalNotificationId)
        {
            return Task.Run(async () => await GetNotificationAsync(generalNotificationId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<GeneralNotificationResponse> GetNotificationAsync(long generalNotificationId, System.Threading.CancellationToken cancellationToken)
        {
            if (generalNotificationId <= 0)
                throw new System.ArgumentOutOfRangeException("GeneralNotificationId");

            String endpoint = generalNotificationEndPoint.GetNotificationAsync(generalNotificationId);
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralNotificationResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new GeneralNotificationResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralNotificationResponse typeBody = JsonConvert.DeserializeObject<GeneralNotificationResponse>(responseData);
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
        public virtual GeneralNotificationResponse UpdateNotification(GeneralNotificationModel body)
        {
            return Task.Run(async () => await UpdateNotificationAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();

        }
        public virtual async Task<GeneralNotificationResponse> UpdateNotificationAsync(GeneralNotificationModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = generalNotificationEndPoint.UpdateNotificationAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralNotificationResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralNotificationResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralNotificationResponse typedBody = JsonConvert.DeserializeObject<GeneralNotificationResponse>(responseData);
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
        public virtual TrueFalseResponse DeleteNotification(ParameterModel body)
        {
            return Task.Run(async () => await DeleteNotificationAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteNotificationAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = generalNotificationEndPoint.DeleteNotificationAsync();
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

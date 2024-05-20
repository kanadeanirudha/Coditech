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
    public class InventoryItemStorageDimensionClient : BaseClient, IInventoryItemStorageDimensionClient
    {
        InventoryItemStorageDimensionEndpoint inventoryItemStorageDimensionEndpoint = null;
        public InventoryItemStorageDimensionClient()
        {
            inventoryItemStorageDimensionEndpoint = new InventoryItemStorageDimensionEndpoint();
        }
        public virtual InventoryItemStorageDimensionListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<InventoryItemStorageDimensionListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = inventoryItemStorageDimensionEndpoint.ListAsync(expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<InventoryItemStorageDimensionListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new InventoryItemStorageDimensionListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    InventoryItemStorageDimensionListResponse typedBody = JsonConvert.DeserializeObject<InventoryItemStorageDimensionListResponse>(responseData);
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

        public virtual InventoryItemStorageDimensionResponse CreateInventoryItemStorageDimension(InventoryItemStorageDimensionModel body)
        {
            return Task.Run(async () => await CreateInventoryItemStorageDimensionAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<InventoryItemStorageDimensionResponse> CreateInventoryItemStorageDimensionAsync(InventoryItemStorageDimensionModel body, CancellationToken cancellationToken)
        {
            string endpoint = inventoryItemStorageDimensionEndpoint.CreateInventoryItemStorageDimensionAsync();
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
                            ObjectResponseResult<InventoryItemStorageDimensionResponse> objectResponseResult2 = await ReadObjectResponseAsync<InventoryItemStorageDimensionResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<InventoryItemStorageDimensionResponse> objectResponseResult = await ReadObjectResponseAsync<InventoryItemStorageDimensionResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            InventoryItemStorageDimensionResponse result = JsonConvert.DeserializeObject<InventoryItemStorageDimensionResponse>(value);
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

        public virtual InventoryItemStorageDimensionResponse GetInventoryItemStorageDimension(short inventoryItemStorageDimensionId)
        {
            return Task.Run(async () => await GetInventoryItemStorageDimensionAsync(inventoryItemStorageDimensionId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<InventoryItemStorageDimensionResponse> GetInventoryItemStorageDimensionAsync(short inventoryItemStorageDimensionId, System.Threading.CancellationToken cancellationToken)
        {
            if (inventoryItemStorageDimensionId <= 0)
                throw new System.ArgumentNullException("InventoryItemStorageDimensionId");

            string endpoint = inventoryItemStorageDimensionEndpoint.GetInventoryItemStorageDimensionAsync(inventoryItemStorageDimensionId);
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
                    var objectResponse = await ReadObjectResponseAsync<InventoryItemStorageDimensionResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new InventoryItemStorageDimensionResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    InventoryItemStorageDimensionResponse typedBody = JsonConvert.DeserializeObject<InventoryItemStorageDimensionResponse>(responseData);
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

        public virtual InventoryItemStorageDimensionResponse UpdateInventoryItemStorageDimension(InventoryItemStorageDimensionModel body)
        {
            return Task.Run(async () => await UpdateInventoryItemStorageDimensionAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<InventoryItemStorageDimensionResponse> UpdateInventoryItemStorageDimensionAsync(InventoryItemStorageDimensionModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = inventoryItemStorageDimensionEndpoint.UpdateInventoryItemStorageDimensionAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<InventoryItemStorageDimensionResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<InventoryItemStorageDimensionResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    InventoryItemStorageDimensionResponse typedBody = JsonConvert.DeserializeObject<InventoryItemStorageDimensionResponse>(responseData);
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

        public virtual TrueFalseResponse DeleteInventoryItemStorageDimension(ParameterModel body)
        {
            return Task.Run(async () => await DeleteInventoryItemStorageDimensionAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteInventoryItemStorageDimensionAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = inventoryItemStorageDimensionEndpoint.DeleteInventoryItemStorageDimensionAsync();
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

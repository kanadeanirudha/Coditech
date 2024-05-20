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
    public class InventoryItemTrackingDimensionGroupClient : BaseClient, IInventoryItemTrackingDimensionGroupClient
    {
        InventoryItemTrackingDimensionGroupEndpoint inventoryItemTrackingDimensionGroupEndpoint = null;
        public InventoryItemTrackingDimensionGroupClient()
        {
            inventoryItemTrackingDimensionGroupEndpoint = new InventoryItemTrackingDimensionGroupEndpoint();
        }
        public virtual InventoryItemTrackingDimensionGroupListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<InventoryItemTrackingDimensionGroupListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = inventoryItemTrackingDimensionGroupEndpoint.ListAsync(expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<InventoryItemTrackingDimensionGroupListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new InventoryItemTrackingDimensionGroupListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    InventoryItemTrackingDimensionGroupListResponse typedBody = JsonConvert.DeserializeObject<InventoryItemTrackingDimensionGroupListResponse>(responseData);
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

        public virtual InventoryItemTrackingDimensionGroupResponse CreateInventoryItemTrackingDimensionGroup(InventoryItemTrackingDimensionGroupModel body)
        {
            return Task.Run(async () => await CreateInventoryItemTrackingDimensionGroupAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<InventoryItemTrackingDimensionGroupResponse> CreateInventoryItemTrackingDimensionGroupAsync(InventoryItemTrackingDimensionGroupModel body, CancellationToken cancellationToken)
        {
            string endpoint = inventoryItemTrackingDimensionGroupEndpoint.CreateInventoryItemTrackingDimensionGroupAsync();
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
                            ObjectResponseResult<InventoryItemTrackingDimensionGroupResponse> objectResponseResult2 = await ReadObjectResponseAsync<InventoryItemTrackingDimensionGroupResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<InventoryItemTrackingDimensionGroupResponse> objectResponseResult = await ReadObjectResponseAsync<InventoryItemTrackingDimensionGroupResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            InventoryItemTrackingDimensionGroupResponse result = JsonConvert.DeserializeObject<InventoryItemTrackingDimensionGroupResponse>(value);
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

        public virtual InventoryItemTrackingDimensionGroupResponse GetInventoryItemTrackingDimensionGroup(int inventoryItemTrackingDimensionGroupId)
        {
            return Task.Run(async () => await GetInventoryItemTrackingDimensionGroupAsync(inventoryItemTrackingDimensionGroupId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<InventoryItemTrackingDimensionGroupResponse> GetInventoryItemTrackingDimensionGroupAsync(int inventoryItemTrackingDimensionGroupId, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = inventoryItemTrackingDimensionGroupEndpoint.GetInventoryItemTrackingDimensionGroupAsync(inventoryItemTrackingDimensionGroupId);
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
                    var objectResponse = await ReadObjectResponseAsync<InventoryItemTrackingDimensionGroupResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new InventoryItemTrackingDimensionGroupResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    InventoryItemTrackingDimensionGroupResponse typedBody = JsonConvert.DeserializeObject<InventoryItemTrackingDimensionGroupResponse>(responseData);
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

        public virtual InventoryItemTrackingDimensionGroupResponse UpdateInventoryItemTrackingDimensionGroup(InventoryItemTrackingDimensionGroupModel body)
        {
            return Task.Run(async () => await UpdateInventoryItemTrackingDimensionGroupAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<InventoryItemTrackingDimensionGroupResponse> UpdateInventoryItemTrackingDimensionGroupAsync(InventoryItemTrackingDimensionGroupModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = inventoryItemTrackingDimensionGroupEndpoint.UpdateInventoryItemTrackingDimensionGroupAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<InventoryItemTrackingDimensionGroupResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<InventoryItemTrackingDimensionGroupResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    InventoryItemTrackingDimensionGroupResponse typedBody = JsonConvert.DeserializeObject<InventoryItemTrackingDimensionGroupResponse>(responseData);
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

        public virtual TrueFalseResponse DeleteInventoryItemTrackingDimensionGroup(ParameterModel body)
        {
            return Task.Run(async () => await DeleteInventoryItemTrackingDimensionGroupAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteInventoryItemTrackingDimensionGroupAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = inventoryItemTrackingDimensionGroupEndpoint.DeleteInventoryItemTrackingDimensionGroupAsync();
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

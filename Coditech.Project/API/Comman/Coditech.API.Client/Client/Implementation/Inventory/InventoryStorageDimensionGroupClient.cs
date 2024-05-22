﻿using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Newtonsoft.Json;
using System.Net;

namespace Coditech.API.Client
{
    public class InventoryStorageDimensionGroupClient : BaseClient, IInventoryStorageDimensionGroupClient
    {
        InventoryStorageDimensionGroupEndpoint inventoryStorageDimensionGroupEndpoint = null;
        public InventoryStorageDimensionGroupClient()
        {
            inventoryStorageDimensionGroupEndpoint = new InventoryStorageDimensionGroupEndpoint();
        }
        public virtual InventoryStorageDimensionGroupListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<InventoryStorageDimensionGroupListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = inventoryStorageDimensionGroupEndpoint.ListAsync(expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<InventoryStorageDimensionGroupListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new InventoryStorageDimensionGroupListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    InventoryStorageDimensionGroupListResponse typedBody = JsonConvert.DeserializeObject<InventoryStorageDimensionGroupListResponse>(responseData);
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

        public virtual InventoryStorageDimensionGroupResponse CreateInventoryStorageDimensionGroup(InventoryStorageDimensionGroupModel body)
        {
            return Task.Run(async () => await CreateInventoryStorageDimensionGroupAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<InventoryStorageDimensionGroupResponse> CreateInventoryStorageDimensionGroupAsync(InventoryStorageDimensionGroupModel body, CancellationToken cancellationToken)
        {
            string endpoint = inventoryStorageDimensionGroupEndpoint.CreateInventoryStorageDimensionGroupAsync();
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
                            ObjectResponseResult<InventoryStorageDimensionGroupResponse> objectResponseResult2 = await ReadObjectResponseAsync<InventoryStorageDimensionGroupResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<InventoryStorageDimensionGroupResponse> objectResponseResult = await ReadObjectResponseAsync<InventoryStorageDimensionGroupResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            InventoryStorageDimensionGroupResponse result = JsonConvert.DeserializeObject<InventoryStorageDimensionGroupResponse>(value);
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

        public virtual InventoryStorageDimensionGroupResponse GetInventoryStorageDimensionGroup(int inventoryStorageDimensionGroupId)
        {
            return Task.Run(async () => await GetInventoryStorageDimensionGroupAsync(inventoryStorageDimensionGroupId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<InventoryStorageDimensionGroupResponse> GetInventoryStorageDimensionGroupAsync(int inventoryStorageDimensionGroupId, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = inventoryStorageDimensionGroupEndpoint.GetInventoryStorageDimensionGroupAsync(inventoryStorageDimensionGroupId);
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
                    var objectResponse = await ReadObjectResponseAsync<InventoryStorageDimensionGroupResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new InventoryStorageDimensionGroupResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    InventoryStorageDimensionGroupResponse typedBody = JsonConvert.DeserializeObject<InventoryStorageDimensionGroupResponse>(responseData);
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

        public virtual InventoryStorageDimensionGroupResponse UpdateInventoryStorageDimensionGroup(InventoryStorageDimensionGroupModel body)
        {
            return Task.Run(async () => await UpdateInventoryStorageDimensionGroupAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<InventoryStorageDimensionGroupResponse> UpdateInventoryStorageDimensionGroupAsync(InventoryStorageDimensionGroupModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = inventoryStorageDimensionGroupEndpoint.UpdateInventoryStorageDimensionGroupAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<InventoryStorageDimensionGroupResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<InventoryStorageDimensionGroupResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    InventoryStorageDimensionGroupResponse typedBody = JsonConvert.DeserializeObject<InventoryStorageDimensionGroupResponse>(responseData);
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

        public virtual TrueFalseResponse DeleteInventoryStorageDimensionGroup(ParameterModel body)
        {
            return Task.Run(async () => await DeleteInventoryStorageDimensionGroupAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteInventoryStorageDimensionGroupAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = inventoryStorageDimensionGroupEndpoint.DeleteInventoryStorageDimensionGroupAsync();
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

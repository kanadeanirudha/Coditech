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
    public class InventoryGeneralItemMasterClient : BaseClient, IInventoryGeneralItemMasterClient
    {
        InventoryGeneralItemMasterEndpoint inventoryGeneralItemMasterEndpoint = null;
        public InventoryGeneralItemMasterClient()
        {
            inventoryGeneralItemMasterEndpoint = new InventoryGeneralItemMasterEndpoint();
        }
        public virtual InventoryGeneralItemMasterListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<InventoryGeneralItemMasterListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = inventoryGeneralItemMasterEndpoint.ListAsync(expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<InventoryGeneralItemMasterListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new InventoryGeneralItemMasterListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    InventoryGeneralItemMasterListResponse typedBody = JsonConvert.DeserializeObject<InventoryGeneralItemMasterListResponse>(responseData);
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

        public virtual InventoryGeneralItemMasterResponse CreateInventoryGeneralItemMaster(InventoryGeneralItemMasterModel body)
        {
            return Task.Run(async () => await CreateInventoryGeneralItemMasterAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<InventoryGeneralItemMasterResponse> CreateInventoryGeneralItemMasterAsync(InventoryGeneralItemMasterModel body, CancellationToken cancellationToken)
        {
            string endpoint = inventoryGeneralItemMasterEndpoint.CreateInventoryGeneralItemMasterAsync();
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
                            ObjectResponseResult<InventoryGeneralItemMasterResponse> objectResponseResult2 = await ReadObjectResponseAsync<InventoryGeneralItemMasterResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<InventoryGeneralItemMasterResponse> objectResponseResult = await ReadObjectResponseAsync<InventoryGeneralItemMasterResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            InventoryGeneralItemMasterResponse result = JsonConvert.DeserializeObject<InventoryGeneralItemMasterResponse>(value);
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

        public virtual InventoryGeneralItemMasterResponse GetInventoryGeneralItemMaster(int inventoryGeneralItemMasterId)
        {
            return Task.Run(async () => await GetInventoryGeneralItemMasterAsync(inventoryGeneralItemMasterId, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<InventoryGeneralItemMasterResponse> GetInventoryGeneralItemMasterAsync(int inventoryGeneralItemMasterId, CancellationToken cancellationToken)
        {
            if (inventoryGeneralItemMasterId <= 0)
                throw new System.ArgumentNullException("inventoryGeneralItemMasterId");

            string endpoint = inventoryGeneralItemMasterEndpoint.GetInventoryGeneralItemMasterAsync(inventoryGeneralItemMasterId);
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
                    var objectResponse = await ReadObjectResponseAsync<InventoryGeneralItemMasterResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new InventoryGeneralItemMasterResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    InventoryGeneralItemMasterResponse typedBody = JsonConvert.DeserializeObject<InventoryGeneralItemMasterResponse>(responseData);
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

        public virtual InventoryGeneralItemMasterResponse UpdateInventoryGeneralItemMaster(InventoryGeneralItemMasterModel body)
        {
            return Task.Run(async () => await UpdateInventoryGeneralItemMasterAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<InventoryGeneralItemMasterResponse> UpdateInventoryGeneralItemMasterAsync(InventoryGeneralItemMasterModel body, CancellationToken cancellationToken)
        {
            string endpoint = inventoryGeneralItemMasterEndpoint.UpdateInventoryGeneralItemMasterAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<InventoryGeneralItemMasterResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<InventoryGeneralItemMasterResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    InventoryGeneralItemMasterResponse typedBody = JsonConvert.DeserializeObject<InventoryGeneralItemMasterResponse>(responseData);
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

        public virtual TrueFalseResponse DeleteInventoryGeneralItemMaster(ParameterModel body)
        {
            return Task.Run(async () => await DeleteInventoryGeneralItemMasterAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteInventoryGeneralItemMasterAsync(ParameterModel body, CancellationToken cancellationToken)
        {
            string endpoint = inventoryGeneralItemMasterEndpoint.DeleteInventoryGeneralItemMasterAsync();
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
        public virtual InventoryGeneralItemMasterListResponse GetGeneralServicesList(string searchText)
        {
            return Task.Run(async () => await GetGeneralServicesList(searchText, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<InventoryGeneralItemMasterListResponse> GetGeneralServicesList(string searchText, CancellationToken cancellationToken)
        {
            string endpoint = inventoryGeneralItemMasterEndpoint.GetGeneralServicesList(searchText);
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
                    var objectResponse = await ReadObjectResponseAsync<InventoryGeneralItemMasterListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new InventoryGeneralItemMasterListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    InventoryGeneralItemMasterListResponse typedBody = JsonConvert.DeserializeObject<InventoryGeneralItemMasterListResponse>(responseData);
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

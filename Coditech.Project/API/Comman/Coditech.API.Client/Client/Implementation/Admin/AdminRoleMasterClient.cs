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
    public class AdminRoleMasterClient : BaseClient, IAdminRoleMasterClient
    {
        AdminRoleMasterEndpoint adminRoleMasterEndpoint = null;
        public AdminRoleMasterClient()
        {
            adminRoleMasterEndpoint = new AdminRoleMasterEndpoint();
        }
        public virtual AdminRoleListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AdminRoleListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = adminRoleMasterEndpoint.ListAsync(expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<AdminRoleListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new AdminRoleListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AdminRoleListResponse typedBody = JsonConvert.DeserializeObject<AdminRoleListResponse>(responseData);
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

        //public virtual AdminRoleMasterResponse CreateAdminRoleMaster(AdminRoleMasterModel body)
        //{
        //    return Task.Run(async () => await CreateAdminRoleMasterAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        //}

        //public virtual async Task<AdminRoleMasterResponse> CreateAdminRoleMasterAsync(AdminRoleMasterModel body, CancellationToken cancellationToken)
        //{
        //    string endpoint = adminRoleMasterEndpoint.CreateAdminRoleMasterAsync();
        //    HttpResponseMessage response = null;
        //    bool disposeResponse = true;
        //    try
        //    {
        //        ApiStatus status = new ApiStatus();
        //        response = await PostResourceToEndpointAsync(endpoint, JsonConvert.SerializeObject(body), status, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
        //        Dictionary<string, IEnumerable<string>> dictionary = BindHeaders(response);

        //        switch (response.StatusCode)
        //        {
        //            case HttpStatusCode.OK:
        //                {
        //                    ObjectResponseResult<AdminRoleMasterResponse> objectResponseResult2 = await ReadObjectResponseAsync<AdminRoleMasterResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
        //                    if (objectResponseResult2.Object == null)
        //                    {
        //                        throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
        //                    }

        //                    return objectResponseResult2.Object;
        //                }
        //            case HttpStatusCode.Created:
        //                {
        //                    ObjectResponseResult<AdminRoleMasterResponse> objectResponseResult = await ReadObjectResponseAsync<AdminRoleMasterResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
        //                    if (objectResponseResult.Object == null)
        //                    {
        //                        throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
        //                    }

        //                    return objectResponseResult.Object;
        //                }
        //            default:
        //                {
        //                    string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
        //                    AdminRoleMasterResponse result = JsonConvert.DeserializeObject<AdminRoleMasterResponse>(value);
        //                    UpdateApiStatus(result, status, response);
        //                    throw new CoditechException(status.ErrorCode, status.ErrorMessage, status.StatusCode);
        //                }
        //        }
        //    }
        //    finally
        //    {
        //        if (disposeResponse)
        //        {
        //            response.Dispose();
        //        }
        //    }
        //}

        //public virtual AdminRoleMasterResponse GetAdminRoleMaster(int adminRoleMasterId)
        //{
        //    return Task.Run(async () => await GetAdminRoleMasterAsync(adminRoleMasterId, CancellationToken.None)).GetAwaiter().GetResult();
        //}

        //public virtual async Task<AdminRoleMasterResponse> GetAdminRoleMasterAsync(int adminRoleMasterId, CancellationToken cancellationToken)
        //{
        //    if (adminRoleMasterId <= 0)
        //        throw new System.ArgumentNullException("adminRoleMasterId");

        //    string endpoint = adminRoleMasterEndpoint.GetAdminRoleMasterAsync(adminRoleMasterId);
        //    HttpResponseMessage response = null;
        //    var disposeResponse = true;

        //    try
        //    {
        //        ApiStatus status = new ApiStatus();

        //        response = await GetResourceFromEndpointAsync(endpoint, status, cancellationToken).ConfigureAwait(false);
        //        Dictionary<string, IEnumerable<string>> headers_ = BindHeaders(response);
        //        var status_ = (int)response.StatusCode;
        //        if (status_ == 200)
        //        {
        //            var objectResponse = await ReadObjectResponseAsync<AdminRoleMasterResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
        //            if (objectResponse.Object == null)
        //            {
        //                throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
        //            }
        //            return objectResponse.Object;
        //        }
        //        else
        //        if (status_ == 204)
        //        {
        //            return new AdminRoleMasterResponse();
        //        }
        //        else
        //        {
        //            string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        //            AdminRoleMasterResponse typedBody = JsonConvert.DeserializeObject<AdminRoleMasterResponse>(responseData);
        //            UpdateApiStatus(typedBody, status, response);
        //            throw new CoditechException(status.ErrorCode, status.ErrorMessage, status.StatusCode);
        //        }
        //    }
        //    finally
        //    {
        //        if (disposeResponse)
        //            response.Dispose();
        //    }
        //}

        //public virtual AdminRoleMasterResponse UpdateAdminRoleMaster(AdminRoleMasterModel body)
        //{
        //    return Task.Run(async () => await UpdateAdminRoleMasterAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        //}

        //public virtual async Task<AdminRoleMasterResponse> UpdateAdminRoleMasterAsync(AdminRoleMasterModel body, CancellationToken cancellationToken)
        //{
        //    string endpoint = adminRoleMasterEndpoint.UpdateAdminRoleMasterAsync();
        //    HttpResponseMessage response = null;
        //    var disposeResponse = true;

        //    try
        //    {
        //        ApiStatus status = new ApiStatus();

        //        response = await PutResourceToEndpointAsync(endpoint, JsonConvert.SerializeObject(body), status, cancellationToken).ConfigureAwait(false);

        //        var headers_ = BindHeaders(response);
        //        var status_ = (int)response.StatusCode;
        //        if (status_ == 200)
        //        {
        //            var objectResponse = await ReadObjectResponseAsync<AdminRoleMasterResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
        //            if (objectResponse.Object == null)
        //            {
        //                throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
        //            }
        //            return objectResponse.Object;
        //        }
        //        else
        //        if (status_ == 201)
        //        {
        //            var objectResponse = await ReadObjectResponseAsync<AdminRoleMasterResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
        //            if (objectResponse.Object == null)
        //            {
        //                throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
        //            }
        //            return objectResponse.Object;
        //        }
        //        else
        //        {
        //            string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        //            AdminRoleMasterResponse typedBody = JsonConvert.DeserializeObject<AdminRoleMasterResponse>(responseData);
        //            UpdateApiStatus(typedBody, status, response);
        //            throw new CoditechException(status.ErrorCode, status.ErrorMessage, status.StatusCode);
        //        }

        //    }
        //    finally
        //    {
        //        if (disposeResponse)
        //            response.Dispose();
        //    }
        //}

        //public virtual TrueFalseResponse DeleteAdminRoleMaster(ParameterModel body)
        //{
        //    return Task.Run(async () => await DeleteAdminRoleMasterAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        //}

        //public virtual async Task<TrueFalseResponse> DeleteAdminRoleMasterAsync(ParameterModel body, CancellationToken cancellationToken)
        //{
        //    string endpoint = adminRoleMasterEndpoint.DeleteAdminRoleMasterAsync();
        //    HttpResponseMessage response = null;
        //    var disposeResponse = true;

        //    try
        //    {
        //        ApiStatus status = new ApiStatus();

        //        response = await PostResourceToEndpointAsync(endpoint, JsonConvert.SerializeObject(body), status, cancellationToken).ConfigureAwait(false);

        //        var headers_ = BindHeaders(response);
        //        var status_ = (int)response.StatusCode;
        //        if (status_ == 200)
        //        {
        //            var objectResponse = await ReadObjectResponseAsync<TrueFalseResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
        //            if (objectResponse.Object == null)
        //            {
        //                throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
        //            }
        //            return objectResponse.Object;
        //        }
        //        else
        //        {
        //            string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        //            TrueFalseResponse typedBody = JsonConvert.DeserializeObject<TrueFalseResponse>(responseData);
        //            UpdateApiStatus(typedBody, status, response);
        //            throw new CoditechException(status.ErrorCode, status.ErrorMessage, status.StatusCode);
        //        }
        //    }
        //    finally
        //    {
        //        if (disposeResponse)
        //            response.Dispose();
        //    }
        //}
    }
}

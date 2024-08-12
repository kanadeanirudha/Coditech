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

        public virtual AdminRoleResponse GetAdminRoleDetailsById(int adminRoleMasterId)
        {
            return Task.Run(async () => await GetAdminRoleDetailsByIdAsync(adminRoleMasterId, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AdminRoleResponse> GetAdminRoleDetailsByIdAsync(int adminRoleMasterId, CancellationToken cancellationToken)
        {
            if (adminRoleMasterId <= 0)
                throw new System.ArgumentNullException("adminRoleMasterId");

            string endpoint = adminRoleMasterEndpoint.GetAdminRoleDetailsByIdAsync(adminRoleMasterId);
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
                    var objectResponse = await ReadObjectResponseAsync<AdminRoleResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new AdminRoleResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AdminRoleResponse typedBody = JsonConvert.DeserializeObject<AdminRoleResponse>(responseData);
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

        public virtual AdminRoleResponse UpdateAdminRole(AdminRoleModel body)
        {
            return Task.Run(async () => await UpdateAdminRoleAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AdminRoleResponse> UpdateAdminRoleAsync(AdminRoleModel body, CancellationToken cancellationToken)
        {
            string endpoint = adminRoleMasterEndpoint.UpdateAdminRoleAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<AdminRoleResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<AdminRoleResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AdminRoleResponse typedBody = JsonConvert.DeserializeObject<AdminRoleResponse>(responseData);
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

        public virtual TrueFalseResponse DeleteAdminRole(ParameterModel body)
        {
            return Task.Run(async () => await DeleteAdminRoleAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteAdminRoleAsync(ParameterModel body, CancellationToken cancellationToken)
        {
            string endpoint = adminRoleMasterEndpoint.DeleteAdminRoleAsync();
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

        public virtual AdminRoleMenuDetailsResponse GetAdminRoleMenuDetailsById(int adminRoleMasterId, string moduleCode)
        {
            return Task.Run(async () => await GetAdminRoleMenuDetailsByIdAsync(adminRoleMasterId, moduleCode, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AdminRoleMenuDetailsResponse> GetAdminRoleMenuDetailsByIdAsync(int adminRoleMasterId, string moduleCode, CancellationToken cancellationToken)
        {
            if (adminRoleMasterId <= 0)
                throw new System.ArgumentNullException("adminRoleMasterId");

            string endpoint = adminRoleMasterEndpoint.GetAdminRoleMenuDetailsByIdAsync(adminRoleMasterId, moduleCode);
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
                    var objectResponse = await ReadObjectResponseAsync<AdminRoleMenuDetailsResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new AdminRoleMenuDetailsResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AdminRoleMenuDetailsResponse typedBody = JsonConvert.DeserializeObject<AdminRoleMenuDetailsResponse>(responseData);
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

        public virtual AdminRoleMenuDetailsResponse InsertUpdateAdminRoleMenuDetails(AdminRoleMenuDetailsModel body)
        {
            return Task.Run(async () => await InsertUpdateAdminRoleMenuDetailsAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AdminRoleMenuDetailsResponse> InsertUpdateAdminRoleMenuDetailsAsync(AdminRoleMenuDetailsModel body, CancellationToken cancellationToken)
        {
            string endpoint = adminRoleMasterEndpoint.InsertUpdateAdminRoleMenuDetailsAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<AdminRoleMenuDetailsResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<AdminRoleMenuDetailsResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AdminRoleMenuDetailsResponse typedBody = JsonConvert.DeserializeObject<AdminRoleMenuDetailsResponse>(responseData);
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

        public virtual AdminRoleApplicableDetailsListResponse RoleAllocatedToUserList(int adminRoleMasterId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await RoleAllocatedToUserListAsync(adminRoleMasterId, expand, filter, sort, pageIndex, pageSize, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AdminRoleApplicableDetailsListResponse> RoleAllocatedToUserListAsync(int adminRoleMasterId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = adminRoleMasterEndpoint.RoleAllocatedToUserListAsync(adminRoleMasterId, expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<AdminRoleApplicableDetailsListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new AdminRoleApplicableDetailsListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AdminRoleApplicableDetailsListResponse typedBody = JsonConvert.DeserializeObject<AdminRoleApplicableDetailsListResponse>(responseData);
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

        public virtual AdminRoleApplicableDetailsResponse GetAssociateUnAssociateAdminRoleToUser(int adminRoleMasterId, int adminRoleApplicableDetailId)
        {
            return Task.Run(async () => await GetAssociateUnAssociateAdminRoleToUserAsync(adminRoleMasterId, adminRoleApplicableDetailId, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AdminRoleApplicableDetailsResponse> GetAssociateUnAssociateAdminRoleToUserAsync(int adminRoleMasterId, int adminRoleApplicableDetailId, CancellationToken cancellationToken)
        {
            if (adminRoleMasterId <= 0 && adminRoleApplicableDetailId == 0)
                throw new System.ArgumentNullException("adminRoleMasterId");

            string endpoint = adminRoleMasterEndpoint.GetAssociateUnAssociateAdminRoleToUserAsync(adminRoleMasterId, adminRoleApplicableDetailId);
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
                    var objectResponse = await ReadObjectResponseAsync<AdminRoleApplicableDetailsResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new AdminRoleApplicableDetailsResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AdminRoleApplicableDetailsResponse typedBody = JsonConvert.DeserializeObject<AdminRoleApplicableDetailsResponse>(responseData);
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

        public virtual AdminRoleApplicableDetailsResponse AssociateUnAssociateAdminRoleToUser(AdminRoleApplicableDetailsModel body)
        {
            return Task.Run(async () => await AssociateUnAssociateAdminRoleToUserAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AdminRoleApplicableDetailsResponse> AssociateUnAssociateAdminRoleToUserAsync(AdminRoleApplicableDetailsModel body, CancellationToken cancellationToken)
        {
            string endpoint = adminRoleMasterEndpoint.AssociateUnAssociateAdminRoleToUserAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<AdminRoleApplicableDetailsResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<AdminRoleApplicableDetailsResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AdminRoleApplicableDetailsResponse typedBody = JsonConvert.DeserializeObject<AdminRoleApplicableDetailsResponse>(responseData);
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

        public virtual AdminRoleMediaFolderActionResponse GetAdminRoleWiseMediaFolderActionById(int adminRoleMasterId)
        {
            return Task.Run(async () => await GetAdminRoleWiseMediaFolderActionByIdAsync(adminRoleMasterId, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AdminRoleMediaFolderActionResponse> GetAdminRoleWiseMediaFolderActionByIdAsync(int adminRoleMasterId, CancellationToken cancellationToken)
        {
            if (adminRoleMasterId <= 0)
                throw new System.ArgumentNullException("adminRoleMasterId");

            string endpoint = adminRoleMasterEndpoint.GetAdminRoleWiseMediaFolderActionByIdAsync(adminRoleMasterId);
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
                    var objectResponse = await ReadObjectResponseAsync<AdminRoleMediaFolderActionResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new AdminRoleMediaFolderActionResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AdminRoleMediaFolderActionResponse typedBody = JsonConvert.DeserializeObject<AdminRoleMediaFolderActionResponse>(responseData);
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

        public virtual AdminRoleMediaFolderActionResponse InsertUpdateAdminRoleWiseMediaFolderAction(AdminRoleMediaFolderActionModel body)
        {
            return Task.Run(async () => await InsertUpdateAdminRoleWiseMediaFolderActionAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AdminRoleMediaFolderActionResponse> InsertUpdateAdminRoleWiseMediaFolderActionAsync(AdminRoleMediaFolderActionModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = adminRoleMasterEndpoint.InsertUpdateAdminRoleWiseMediaFolderActionAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<AdminRoleMediaFolderActionResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<AdminRoleMediaFolderActionResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AdminRoleMediaFolderActionResponse typedBody = JsonConvert.DeserializeObject<AdminRoleMediaFolderActionResponse>(responseData);
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

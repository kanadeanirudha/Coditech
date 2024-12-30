using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Newtonsoft.Json;

using System.Net;
using System.Net.Http.Headers;

namespace Coditech.API.Client
{
    public class MediaManagerClient : BaseClient, IMediaManagerClient
    {
        MediaManagerEndpoint mediaManagerEndpoint = null;
        public MediaManagerClient()
        {
            mediaManagerEndpoint = new MediaManagerEndpoint();
        }

        public virtual MediaManagerResponse UploadMedia(int folderId, string folderName, UploadMediaModel body)
        {
            string endpoint = mediaManagerEndpoint.UploadMediaAsync(folderId, folderName);
            HttpResponseMessage response = null;
            bool disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();
                var formData = new MultipartFormDataContent();
                var fileContent = new StreamContent(body.MediaFile.OpenReadStream())
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue(body.MediaFile.ContentType)
                    }
                };
                formData.Add(fileContent, "files", body.MediaFile.FileName);
                response = PostResourceToEndpoint(endpoint, formData, status, new CancellationToken());
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        {
                            MediaManagerResponse objectResponseResult = JsonConvert.DeserializeObject<MediaManagerResponse>(response.Content.ReadAsStringAsync().Result);
                            return JsonConvert.DeserializeObject<MediaManagerResponse>(response.Content.ReadAsStringAsync().Result);
                        }
                    case HttpStatusCode.Created:
                        {
                            return JsonConvert.DeserializeObject<MediaManagerResponse>(response.Content.ReadAsStringAsync().Result);
                        }
                    default:
                        {
                            return JsonConvert.DeserializeObject<MediaManagerResponse>(response.Content.ReadAsStringAsync().Result);
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

        public virtual MediaManagerFolderResponse GetFolderStructure(int rootFolderId, int adminRoleId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await GetFolderStructureAsync(rootFolderId, adminRoleId, expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<MediaManagerFolderResponse> GetFolderStructureAsync(int rootFolderId, int adminRoleId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = mediaManagerEndpoint.GetFolderStructureAsync(rootFolderId, adminRoleId,expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<MediaManagerFolderResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new MediaManagerFolderResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    MediaManagerFolderResponse typedBody = JsonConvert.DeserializeObject<MediaManagerFolderResponse>(responseData);
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

        public virtual async Task<FolderListResponse> GetAllFolders()
        {
            string endpoint = mediaManagerEndpoint.GetAllFolders();

            HttpResponseMessage response = null;
            bool disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();

                response = await GetResourceFromEndpointAsync(endpoint, status, CancellationToken.None).ConfigureAwait(false);
                Dictionary<string, IEnumerable<string>> headers_ = BindHeaders(response);
                var status_ = (int)response.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse = await ReadObjectResponseAsync<FolderListResponse>(response, headers_, CancellationToken.None).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new FolderListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    MediaManagerFolderResponse typedBody = JsonConvert.DeserializeObject<MediaManagerFolderResponse>(responseData);
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

        public virtual async Task<TrueFalseResponse> CreateFolderAsync(int rootFolderId, string folderName, int adminRoleMasterId)
        {
            string endpoint = mediaManagerEndpoint.CreateFolderAsync(rootFolderId, folderName);

            HttpResponseMessage response = null;
            bool disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();

                response = await GetResourceFromEndpointAsync(endpoint, status, CancellationToken.None).ConfigureAwait(false);
                Dictionary<string, IEnumerable<string>> headers_ = BindHeaders(response);
                var status_ = (int)response.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse = await ReadObjectResponseAsync<TrueFalseResponse>(response, headers_, CancellationToken.None).ConfigureAwait(false);

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

        public virtual async Task<bool> MoveFolderAsync(int folderId, int destinationFolderId)
        {
            string endpoint = mediaManagerEndpoint.MoveFolderAsync(folderId, destinationFolderId);

            HttpResponseMessage response = null;
            bool disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();

                response = await GetResourceFromEndpointAsync(endpoint, status, CancellationToken.None).ConfigureAwait(false);
                Dictionary<string, IEnumerable<string>> headers_ = BindHeaders(response);
                var status_ = (int)response.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse = await ReadObjectResponseAsync<bool>(response, headers_, CancellationToken.None).ConfigureAwait(false);

                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return false;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    bool typedBody = JsonConvert.DeserializeObject<bool>(responseData);
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

        public virtual async Task<bool> DeleteFolderAsync(int folderId)
        {
            string endpoint = mediaManagerEndpoint.DeleteFolderAsync(folderId);

            HttpResponseMessage response = null;
            bool disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();

                response = await GetResourceFromEndpointAsync(endpoint, status, CancellationToken.None).ConfigureAwait(false);
                Dictionary<string, IEnumerable<string>> headers_ = BindHeaders(response);
                var status_ = (int)response.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse = await ReadObjectResponseAsync<bool>(response, headers_, CancellationToken.None).ConfigureAwait(false);

                    return objectResponse.Object; ;
                }
                else if (status_ == 204)
                {
                    return false;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    bool typedBody = JsonConvert.DeserializeObject<bool>(responseData);
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

        public virtual async Task<bool> DeleteFileAsync(int mediaId)
        {
            string endpoint = mediaManagerEndpoint.DeleteFileAsync(mediaId);

            HttpResponseMessage response = null;
            bool disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();

                response = await GetResourceFromEndpointAsync(endpoint, status, CancellationToken.None).ConfigureAwait(false);
                Dictionary<string, IEnumerable<string>> headers_ = BindHeaders(response);
                var status_ = (int)response.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse = await ReadObjectResponseAsync<bool>(response, headers_, CancellationToken.None).ConfigureAwait(false);

                    return objectResponse.Object; ;
                }
                else if (status_ == 204)
                {
                    return false;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    bool typedBody = JsonConvert.DeserializeObject<bool>(responseData);
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

        public virtual async Task<bool> RenameFolderAsync(int folderId, string renameFolderName)
        {
            string endpoint = mediaManagerEndpoint.RenameFolderAsync(folderId, renameFolderName);

            HttpResponseMessage response = null;
            bool disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();

                response = await GetResourceFromEndpointAsync(endpoint, status, CancellationToken.None).ConfigureAwait(false);
                Dictionary<string, IEnumerable<string>> headers_ = BindHeaders(response);
                var status_ = (int)response.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse = await ReadObjectResponseAsync<bool>(response, headers_, CancellationToken.None).ConfigureAwait(false);

                    return objectResponse.Object; ;
                }
                else if (status_ == 204)
                {
                    return false;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    bool typedBody = JsonConvert.DeserializeObject<bool>(responseData);
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

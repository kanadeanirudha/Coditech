using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;

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
        public virtual MediaManagerResponse UploadMedia(UploadMediaModel body)
        {
            return Task.Run(async () => await UploadMediaAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<MediaManagerFolderResponse> GetFolderStructure(int rootFolderId = 0)
        {
            string endpoint = mediaManagerEndpoint.GetFolderStructureAsync(rootFolderId);

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
                    var objectResponse = await ReadObjectResponseAsync<MediaManagerFolderResponse>(response, headers_, CancellationToken.None).ConfigureAwait(false);
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

        public virtual async Task<TrueFalseResponse> CreateFolderAsync(int rootFolderId, string folderName)
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
                                     
                    return objectResponse.Object;;
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

        public virtual async Task<TrueFalseResponse> UploadFileAsync(int folderId,UploadMediaModel body)
        {
            string endpoint = mediaManagerEndpoint.UploadFileAsync(folderId);
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
                response = await PostResourceToEndpointAsync(endpoint, formData, status, CancellationToken.None).ConfigureAwait(continueOnCapturedContext: false);
                Dictionary<string, IEnumerable<string>> dictionary = BindHeaders(response);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        {
                            ObjectResponseResult<TrueFalseResponse> objectResponseResult2 = await ReadObjectResponseAsync<TrueFalseResponse>(response, BindHeaders(response), CancellationToken.None).ConfigureAwait(continueOnCapturedContext: false);
                            return objectResponseResult2.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            MediaManagerResponse result = JsonConvert.DeserializeObject<MediaManagerResponse>(value);
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

        public virtual async Task<MediaManagerResponse> UploadMediaAsync(UploadMediaModel body, CancellationToken cancellationToken)
        {
            string endpoint = mediaManagerEndpoint.UploadMediaAsync();
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
                response = await PostResourceToEndpointAsync(endpoint, formData, status, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                Dictionary<string, IEnumerable<string>> dictionary = BindHeaders(response);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        {
                            ObjectResponseResult<MediaManagerResponse> objectResponseResult2 = await ReadObjectResponseAsync<MediaManagerResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<MediaManagerResponse> objectResponseResult = await ReadObjectResponseAsync<MediaManagerResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            MediaManagerResponse result = JsonConvert.DeserializeObject<MediaManagerResponse>(value);
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
    }
}

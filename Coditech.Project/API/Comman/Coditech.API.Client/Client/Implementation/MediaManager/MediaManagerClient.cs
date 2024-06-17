using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
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

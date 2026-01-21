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
    public class GeneralTemplateClient : BaseClient, IGeneralTemplateClient
    {
        GeneralTemplateEndpoint generalTemplateEndpoint = null;
        public GeneralTemplateClient()
        {
            generalTemplateEndpoint = new GeneralTemplateEndpoint();
        }
     
        public virtual GeneralTemplateResponse GetTemplate(int generalTemplateId)
        {
            return Task.Run(async () => await GetTemplateAsync(generalTemplateId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralTemplateResponse> GetTemplateAsync(int generalTemplateId, System.Threading.CancellationToken cancellationToken)
        {
            if (generalTemplateId <= 0)
                throw new System.ArgumentNullException("generalTemplateId");

            string endpoint = generalTemplateEndpoint.GetTemplateAsync(generalTemplateId);
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralTemplateResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new GeneralTemplateResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralTemplateResponse typedBody = JsonConvert.DeserializeObject<GeneralTemplateResponse>(responseData);
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


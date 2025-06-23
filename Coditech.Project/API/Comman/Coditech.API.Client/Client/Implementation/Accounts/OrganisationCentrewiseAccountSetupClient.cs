using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Model;
using Newtonsoft.Json;
using System.Net;
namespace Coditech.API.Client
{
    public class OrganisationCentrewiseAccountSetupClient : BaseClient, IOrganisationCentrewiseAccountSetupClient
    {
        OrganisationCentrewiseAccountSetupEndpoint organisationCentrewiseAccountSetupEndpoint = null;
        public OrganisationCentrewiseAccountSetupClient()
        {
            organisationCentrewiseAccountSetupEndpoint = new OrganisationCentrewiseAccountSetupEndpoint();
        }
        #region OrganisationCentrewiseAccountSetup
        public virtual OrganisationCentrewiseAccountSetupResponse GetOrganisationCentrewiseAccountSetup(string centreCode)
        {
            return Task.Run(async () => await GetOrganisationCentrewiseAccountSetupAsync(centreCode, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseAccountSetupResponse> GetOrganisationCentrewiseAccountSetupAsync(string centreCode, CancellationToken cancellationToken)
        {
            //if (organisationCentrewiseAccountSetupId <= 0)
            //    throw new System.ArgumentNullException("organisationCentrewiseAccountSetupId");

            string endpoint = organisationCentrewiseAccountSetupEndpoint.GetOrganisationCentrewiseAccountSetupAsync(centreCode);
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseAccountSetupResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new OrganisationCentrewiseAccountSetupResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseAccountSetupResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseAccountSetupResponse>(responseData);
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

        public virtual OrganisationCentrewiseAccountSetupResponse UpdateOrganisationCentrewiseAccountSetup(OrganisationCentrewiseAccountSetupModel body)
        {
            return Task.Run(async () => await UpdateOrganisationCentrewiseAccountSetupAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseAccountSetupResponse> UpdateOrganisationCentrewiseAccountSetupAsync(OrganisationCentrewiseAccountSetupModel body, CancellationToken cancellationToken)
        {
            string endpoint = organisationCentrewiseAccountSetupEndpoint.UpdateOrganisationCentrewiseAccountSetupAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseAccountSetupResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseAccountSetupResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseAccountSetupResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseAccountSetupResponse>(responseData);
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
        #endregion
    }
}

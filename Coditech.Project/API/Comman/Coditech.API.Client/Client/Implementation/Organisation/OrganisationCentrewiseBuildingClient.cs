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
    public class OrganisationCentrewiseBuildingClient : BaseClient, IOrganisationCentrewiseBuildingClient
    {
        OrganisationCentrewiseBuildingEndpoint organisationCentrewiseBuildingEndpoint = null;
        public OrganisationCentrewiseBuildingClient()
        {
            organisationCentrewiseBuildingEndpoint = new OrganisationCentrewiseBuildingEndpoint();
        }
        public virtual OrganisationCentrewiseBuildingListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseBuildingListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = organisationCentrewiseBuildingEndpoint.ListAsync(expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseBuildingListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new OrganisationCentrewiseBuildingListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseBuildingListResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseBuildingListResponse>(responseData);
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

        public virtual OrganisationCentrewiseBuildingResponse CreateOrganisationCentrewiseBuilding(OrganisationCentrewiseBuildingModel body)
        {
            return Task.Run(async () => await CreateOrganisationAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseBuildingResponse> CreateOrganisationAsync(OrganisationCentrewiseBuildingModel body, CancellationToken cancellationToken)
        {
            string endpoint = organisationCentrewiseBuildingEndpoint.CreateOrganisationAsync();
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
                            ObjectResponseResult<OrganisationCentrewiseBuildingResponse> objectResponseResult2 = await ReadObjectResponseAsync<OrganisationCentrewiseBuildingResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<OrganisationCentrewiseBuildingResponse> objectResponseResult = await ReadObjectResponseAsync<OrganisationCentrewiseBuildingResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            OrganisationCentrewiseBuildingResponse result = JsonConvert.DeserializeObject<OrganisationCentrewiseBuildingResponse>(value);
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

        public virtual OrganisationCentrewiseBuildingResponse GetOrganisationCentrewiseBuilding(short organisationCentrewiseBuildingId)
        {
            return Task.Run(async () => await GetOrganisationAsync(organisationCentrewiseBuildingId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseBuildingResponse> GetOrganisationAsync(short organisationCentrewiseBuildingId, System.Threading.CancellationToken cancellationToken)
        {
            if (organisationCentrewiseBuildingId <= 0)
                throw new System.ArgumentNullException("organisationCentrewiseBuildingId");

            string endpoint = organisationCentrewiseBuildingEndpoint.GetOrganisationAsync(organisationCentrewiseBuildingId);
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseBuildingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new OrganisationCentrewiseBuildingResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseBuildingResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseBuildingResponse>(responseData);
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

        public virtual OrganisationCentrewiseBuildingResponse UpdateOrganisationCentrewiseBuilding(OrganisationCentrewiseBuildingModel body)
        {
            return Task.Run(async () => await UpdateOrganisationAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseBuildingResponse> UpdateOrganisationAsync(OrganisationCentrewiseBuildingModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = organisationCentrewiseBuildingEndpoint.UpdateOrganisationAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseBuildingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseBuildingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseBuildingResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseBuildingResponse>(responseData);
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

        public virtual TrueFalseResponse DeleteOrganisationCentrewiseBuilding(ParameterModel body)
        {
            return Task.Run(async () => await DeleteOrganisationAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteOrganisationAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = organisationCentrewiseBuildingEndpoint.DeleteOrganisationAsync();
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

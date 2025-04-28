using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Newtonsoft.Json;

namespace Coditech.API.Client
{
    public class OrganisationCentrewisePolicyClient : BaseClient, IOrganisationCentrewisePolicyClient
    {
        OrganisationCentrewisePolicyEndpoint organisationCentrewisePolicyEndpoint = null;
        public OrganisationCentrewisePolicyClient()
        {
            organisationCentrewisePolicyEndpoint = new OrganisationCentrewisePolicyEndpoint();
        }
        public virtual GeneralPolicyDetailsListResponse List(string centreCode)
        {
            return Task.Run(async () => await ListAsync(centreCode, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<GeneralPolicyDetailsListResponse> ListAsync(string centreCode, CancellationToken cancellationToken)
        {
            string endpoint = organisationCentrewisePolicyEndpoint.ListAsync(centreCode);
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralPolicyDetailsListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new GeneralPolicyDetailsListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralPolicyDetailsListResponse typedBody = JsonConvert.DeserializeObject<GeneralPolicyDetailsListResponse>(responseData);
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

        public virtual GeneralPolicyDetailsResponse GetCentrewisePolicyDetails(string centreCode, short generalPolicyRulesId)
        {
            return Task.Run(async () => await GetCentrewisePolicyDetailsAsync(centreCode, generalPolicyRulesId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<GeneralPolicyDetailsResponse> GetCentrewisePolicyDetailsAsync(string centreCode, short generalPolicyRulesId, System.Threading.CancellationToken cancellationToken)
        {
            if (generalPolicyRulesId <= 0)
                throw new System.ArgumentNullException("generalPolicyDetailsId");

            string endpoint = organisationCentrewisePolicyEndpoint.GetCentrewisePolicyDetailsAsync(centreCode, generalPolicyRulesId);
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralPolicyDetailsResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new GeneralPolicyDetailsResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralPolicyDetailsResponse typedBody = JsonConvert.DeserializeObject<GeneralPolicyDetailsResponse>(responseData);
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

        public virtual GeneralPolicyDetailsResponse CentrewisePolicyDetails(GeneralPolicyDetailsModel body)
        {
            return Task.Run(async () => await CentrewisePolicyDetailsAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<GeneralPolicyDetailsResponse> CentrewisePolicyDetailsAsync(GeneralPolicyDetailsModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = organisationCentrewisePolicyEndpoint.CentrewisePolicyDetailsAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralPolicyDetailsResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralPolicyDetailsResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralPolicyDetailsResponse typedBody = JsonConvert.DeserializeObject<GeneralPolicyDetailsResponse>(responseData);
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

        public virtual TrueFalseResponse DeleteCentrewisePolicy(ParameterModel body)
        {
            return Task.Run(async () => await DeleteCentrewisePolicyAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteCentrewisePolicyAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = organisationCentrewisePolicyEndpoint.DeleteCentrewisePolicyAsync();
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


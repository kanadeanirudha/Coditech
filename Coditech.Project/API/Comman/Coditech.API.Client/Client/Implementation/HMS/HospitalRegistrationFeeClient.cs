using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;

using Newtonsoft.Json;
using System.Net;

namespace Coditech.API.Client
{
    public class HospitalRegistrationFeeClient : BaseClient, IHospitalRegistrationFeeClient
    {
        HospitalRegistrationFeeEndpoint hospitalRegistrationFeeEndpoint = null;
        public HospitalRegistrationFeeClient()
        {
            hospitalRegistrationFeeEndpoint = new HospitalRegistrationFeeEndpoint();
        }
        public virtual HospitalRegistrationFeeListResponse List(string selectedCentreCode, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(selectedCentreCode, expand, filter, sort, pageIndex, pageSize, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalRegistrationFeeListResponse> ListAsync(string selectedCentreCode, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = hospitalRegistrationFeeEndpoint.ListAsync(selectedCentreCode, expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<HospitalRegistrationFeeListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new HospitalRegistrationFeeListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HospitalRegistrationFeeListResponse typedBody = JsonConvert.DeserializeObject<HospitalRegistrationFeeListResponse>(responseData);
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

        public virtual HospitalRegistrationFeeResponse CreateRegistrationFee(HospitalRegistrationFeeModel body)
        {
            return Task.Run(async () => await CreateRegistrationFeeAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalRegistrationFeeResponse> CreateRegistrationFeeAsync(HospitalRegistrationFeeModel body, CancellationToken cancellationToken)
        {
            string endpoint = hospitalRegistrationFeeEndpoint.CreateRegistrationFeeAsync();
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
                            ObjectResponseResult<HospitalRegistrationFeeResponse> objectResponseResult2 = await ReadObjectResponseAsync<HospitalRegistrationFeeResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<HospitalRegistrationFeeResponse> objectResponseResult = await ReadObjectResponseAsync<HospitalRegistrationFeeResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            HospitalRegistrationFeeResponse result = JsonConvert.DeserializeObject<HospitalRegistrationFeeResponse>(value);
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

        public virtual HospitalRegistrationFeeResponse GetRegistrationFee(int hospitalRegistrationFeeId)
        {
            return Task.Run(async () => await GetRegistrationFeeAsync(hospitalRegistrationFeeId, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalRegistrationFeeResponse> GetRegistrationFeeAsync(int hospitalRegistrationFeeId, CancellationToken cancellationToken)
        {
            if (hospitalRegistrationFeeId <= 0)
                throw new System.ArgumentNullException("hospitalRegistrationFeeId");

            string endpoint = hospitalRegistrationFeeEndpoint.GetRegistrationFeeAsync(hospitalRegistrationFeeId);
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
                    var objectResponse = await ReadObjectResponseAsync<HospitalRegistrationFeeResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new HospitalRegistrationFeeResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HospitalRegistrationFeeResponse typedBody = JsonConvert.DeserializeObject<HospitalRegistrationFeeResponse>(responseData);
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

        public virtual HospitalRegistrationFeeResponse UpdateRegistrationFee(HospitalRegistrationFeeModel body)
        {
            return Task.Run(async () => await UpdateRegistrationFeeAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalRegistrationFeeResponse> UpdateRegistrationFeeAsync(HospitalRegistrationFeeModel body, CancellationToken cancellationToken)
        {
            string endpoint = hospitalRegistrationFeeEndpoint.UpdateRegistrationFeeAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<HospitalRegistrationFeeResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<HospitalRegistrationFeeResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HospitalRegistrationFeeResponse typedBody = JsonConvert.DeserializeObject<HospitalRegistrationFeeResponse>(responseData);
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

        public virtual TrueFalseResponse DeleteRegistrationFee(ParameterModel body)
        {
            return Task.Run(async () => await DeleteRegistrationFeeAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteRegistrationFeeAsync(ParameterModel body, CancellationToken cancellationToken)
        {
            string endpoint = hospitalRegistrationFeeEndpoint.DeleteRegistrationFeeAsync();
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

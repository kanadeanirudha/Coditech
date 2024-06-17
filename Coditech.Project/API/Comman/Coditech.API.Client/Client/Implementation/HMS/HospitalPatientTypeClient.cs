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
    public class HospitalPatientTypeClient : BaseClient, IHospitalPatientTypeClient
    {
        HospitalPatientTypeEndpoint hospitalPatientTypeEndpoint = null;
        public HospitalPatientTypeClient()
        {
            hospitalPatientTypeEndpoint = new HospitalPatientTypeEndpoint();
        }
        public virtual HospitalPatientTypeListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
           
        }

        public virtual async Task<HospitalPatientTypeListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = hospitalPatientTypeEndpoint.ListAsync(expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<HospitalPatientTypeListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new HospitalPatientTypeListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HospitalPatientTypeListResponse typedBody = JsonConvert.DeserializeObject<HospitalPatientTypeListResponse>(responseData);
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

        public virtual HospitalPatientTypeResponse CreateHospitalPatientType(HospitalPatientTypeModel body)
        {
            return Task.Run(async () => await CreateHospitalPatientTypeAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalPatientTypeResponse> CreateHospitalPatientTypeAsync(HospitalPatientTypeModel body, CancellationToken cancellationToken)
        {
            string endpoint = hospitalPatientTypeEndpoint.CreateHospitalPatientTypeAsync();
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
                            ObjectResponseResult<HospitalPatientTypeResponse> objectResponseResult2 = await ReadObjectResponseAsync<HospitalPatientTypeResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<HospitalPatientTypeResponse> objectResponseResult = await ReadObjectResponseAsync<HospitalPatientTypeResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            HospitalPatientTypeResponse result = JsonConvert.DeserializeObject<HospitalPatientTypeResponse>(value);
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

        public virtual HospitalPatientTypeResponse GetHospitalPatientType(byte hospitalPatientTypeId)
        {
            return Task.Run(async () => await GetHospitalPatientTypeAsync(hospitalPatientTypeId, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalPatientTypeResponse> GetHospitalPatientTypeAsync(byte hospitalPatientTypeId, CancellationToken cancellationToken)
        {
            if (hospitalPatientTypeId <= 0)
                throw new System.ArgumentNullException("hospitalPatientTypeId");

            string endpoint = hospitalPatientTypeEndpoint.GetHospitalPatientTypeAsync(hospitalPatientTypeId);
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
                    var objectResponse = await ReadObjectResponseAsync<HospitalPatientTypeResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new HospitalPatientTypeResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HospitalPatientTypeResponse typedBody = JsonConvert.DeserializeObject<HospitalPatientTypeResponse>(responseData);
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

        public virtual HospitalPatientTypeResponse UpdateHospitalPatientType(HospitalPatientTypeModel body)
        {
            return Task.Run(async () => await UpdateHospitalPatientTypeAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalPatientTypeResponse> UpdateHospitalPatientTypeAsync(HospitalPatientTypeModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = hospitalPatientTypeEndpoint.UpdateHospitalPatientTypeAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<HospitalPatientTypeResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<HospitalPatientTypeResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HospitalPatientTypeResponse typedBody = JsonConvert.DeserializeObject<HospitalPatientTypeResponse>(responseData);
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

        public virtual TrueFalseResponse DeleteHospitalPatientType(ParameterModel body)
        {
            return Task.Run(async () => await DeleteHospitalPatientTypeAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteHospitalPatientTypeAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = hospitalPatientTypeEndpoint.DeleteHospitalPatientTypeAsync();
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

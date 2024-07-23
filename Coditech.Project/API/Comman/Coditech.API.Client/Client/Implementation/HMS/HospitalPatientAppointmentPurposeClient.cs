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
    public class HospitalPatientAppointmentPurposeClient : BaseClient, IHospitalPatientAppointmentPurposeClient
    {
        HospitalPatientAppointmentPurposeEndpoint hospitalPatientAppointmentPurposeEndpoint = null;
        public HospitalPatientAppointmentPurposeClient()
        {
            hospitalPatientAppointmentPurposeEndpoint = new HospitalPatientAppointmentPurposeEndpoint();
        }
        public virtual HospitalPatientAppointmentPurposeListResponse List( IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalPatientAppointmentPurposeListResponse> ListAsync( IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = hospitalPatientAppointmentPurposeEndpoint.ListAsync(expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<HospitalPatientAppointmentPurposeListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new HospitalPatientAppointmentPurposeListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HospitalPatientAppointmentPurposeListResponse typedBody = JsonConvert.DeserializeObject<HospitalPatientAppointmentPurposeListResponse>(responseData);
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

        public virtual HospitalPatientAppointmentPurposeResponse CreateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeModel body)
        {
            return Task.Run(async () => await CreateHospitalPatientAppointmentPurposeAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalPatientAppointmentPurposeResponse> CreateHospitalPatientAppointmentPurposeAsync(HospitalPatientAppointmentPurposeModel body, CancellationToken cancellationToken)
        {
            string endpoint = hospitalPatientAppointmentPurposeEndpoint.CreateHospitalPatientAppointmentPurposeAsync();
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
                            ObjectResponseResult<HospitalPatientAppointmentPurposeResponse> objectResponseResult2 = await ReadObjectResponseAsync<HospitalPatientAppointmentPurposeResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<HospitalPatientAppointmentPurposeResponse> objectResponseResult = await ReadObjectResponseAsync<HospitalPatientAppointmentPurposeResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            HospitalPatientAppointmentPurposeResponse result = JsonConvert.DeserializeObject<HospitalPatientAppointmentPurposeResponse>(value);
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

        public virtual HospitalPatientAppointmentPurposeResponse GetHospitalPatientAppointmentPurpose(short hospitalPatientAppointmentPurposeId)
        {
            return Task.Run(async () => await GetHospitalPatientAppointmentPurposeAsync(hospitalPatientAppointmentPurposeId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalPatientAppointmentPurposeResponse> GetHospitalPatientAppointmentPurposeAsync(short hospitalPatientAppointmentPurposeId, System.Threading.CancellationToken cancellationToken)
        {
            if (hospitalPatientAppointmentPurposeId <= 0)
                throw new System.ArgumentNullException("hospitalPatientAppointmentPurposeId");

            string endpoint = hospitalPatientAppointmentPurposeEndpoint.GetHospitalPatientAppointmentPurposeAsync(hospitalPatientAppointmentPurposeId);
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
                    var objectResponse = await ReadObjectResponseAsync<HospitalPatientAppointmentPurposeResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new HospitalPatientAppointmentPurposeResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HospitalPatientAppointmentPurposeResponse typedBody = JsonConvert.DeserializeObject<HospitalPatientAppointmentPurposeResponse>(responseData);
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
        public virtual HospitalPatientAppointmentPurposeResponse UpdateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeModel body)
        {
            return Task.Run(async () => await UpdateHospitalPatientAppointmentPurposeAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<HospitalPatientAppointmentPurposeResponse> UpdateHospitalPatientAppointmentPurposeAsync(HospitalPatientAppointmentPurposeModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = hospitalPatientAppointmentPurposeEndpoint.UpdateHospitalPatientAppointmentPurposeAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<HospitalPatientAppointmentPurposeResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<HospitalPatientAppointmentPurposeResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HospitalPatientAppointmentPurposeResponse typedBody = JsonConvert.DeserializeObject<HospitalPatientAppointmentPurposeResponse>(responseData);
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
        public virtual TrueFalseResponse DeleteHospitalPatientAppointmentPurpose(ParameterModel body)
        {
            return Task.Run(async () => await DeleteHospitalPatientAppointmentPurposeAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<TrueFalseResponse> DeleteHospitalPatientAppointmentPurposeAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = hospitalPatientAppointmentPurposeEndpoint.DeleteHospitalPatientAppointmentPurposeAsync();
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



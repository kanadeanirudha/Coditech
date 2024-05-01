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
    public class HospitalDoctorAllocatedOPDRoomClient : BaseClient, IHospitalDoctorAllocatedOPDRoomClient
    {
        HospitalDoctorAllocatedOPDRoomEndpoint hospitalDoctorAllocatedOPDRoomEndpoint = null;
        public HospitalDoctorAllocatedOPDRoomClient()
        {
            hospitalDoctorAllocatedOPDRoomEndpoint = new HospitalDoctorAllocatedOPDRoomEndpoint();
        }
        public virtual HospitalDoctorAllocatedOPDRoomListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalDoctorAllocatedOPDRoomListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = hospitalDoctorAllocatedOPDRoomEndpoint.ListAsync(expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<HospitalDoctorAllocatedOPDRoomListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new HospitalDoctorAllocatedOPDRoomListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HospitalDoctorAllocatedOPDRoomListResponse typedBody = JsonConvert.DeserializeObject<HospitalDoctorAllocatedOPDRoomListResponse>(responseData);
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

        public virtual HospitalDoctorAllocatedOPDRoomResponse CreateHospitalDoctorAllocatedOPDRoom(HospitalDoctorAllocatedOPDRoomModel body)
        {
            return Task.Run(async () => await CreateHospitalDoctorAllocatedOPDRoomAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalDoctorAllocatedOPDRoomResponse> CreateHospitalDoctorAllocatedOPDRoomAsync(HospitalDoctorAllocatedOPDRoomModel body, CancellationToken cancellationToken)
        {
            string endpoint = hospitalDoctorAllocatedOPDRoomEndpoint.CreateHospitalDoctorAllocatedOPDRoomAsync();
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
                            ObjectResponseResult<HospitalDoctorAllocatedOPDRoomResponse> objectResponseResult2 = await ReadObjectResponseAsync<HospitalDoctorAllocatedOPDRoomResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<HospitalDoctorAllocatedOPDRoomResponse> objectResponseResult = await ReadObjectResponseAsync<HospitalDoctorAllocatedOPDRoomResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            HospitalDoctorAllocatedOPDRoomResponse result = JsonConvert.DeserializeObject<HospitalDoctorAllocatedOPDRoomResponse>(value);
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

        public virtual HospitalDoctorAllocatedOPDRoomResponse GetHospitalDoctorAllocatedOPDRoom(int hospitalDoctorAllocatedOPDRoomId)
        {
            return Task.Run(async () => await GetHospitalDoctorAllocatedOPDRoomAsync(hospitalDoctorAllocatedOPDRoomId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalDoctorAllocatedOPDRoomResponse> GetHospitalDoctorAllocatedOPDRoomAsync(int hospitalDoctorAllocatedOPDRoomId, System.Threading.CancellationToken cancellationToken)
        {
            if (hospitalDoctorAllocatedOPDRoomId <= 0)
                throw new System.ArgumentNullException("hospitalDoctorAllocatedOPDRoomId");

            string endpoint = hospitalDoctorAllocatedOPDRoomEndpoint.GetHospitalDoctorAllocatedOPDRoomAsync(hospitalDoctorAllocatedOPDRoomId);
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
                    var objectResponse = await ReadObjectResponseAsync<HospitalDoctorAllocatedOPDRoomResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new HospitalDoctorAllocatedOPDRoomResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HospitalDoctorAllocatedOPDRoomResponse typedBody = JsonConvert.DeserializeObject<HospitalDoctorAllocatedOPDRoomResponse>(responseData);
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

        public virtual HospitalDoctorAllocatedOPDRoomResponse UpdateHospitalDoctorAllocatedOPDRoom(HospitalDoctorAllocatedOPDRoomModel body)
        {
            return Task.Run(async () => await UpdateHospitalDoctorAllocatedOPDRoomAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalDoctorAllocatedOPDRoomResponse> UpdateHospitalDoctorAllocatedOPDRoomAsync(HospitalDoctorAllocatedOPDRoomModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = hospitalDoctorAllocatedOPDRoomEndpoint.UpdateHospitalDoctorAllocatedOPDRoomAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<HospitalDoctorAllocatedOPDRoomResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<HospitalDoctorAllocatedOPDRoomResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HospitalDoctorAllocatedOPDRoomResponse typedBody = JsonConvert.DeserializeObject<HospitalDoctorAllocatedOPDRoomResponse>(responseData);
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

        public virtual TrueFalseResponse DeleteHospitalDoctorAllocatedOPDRoom(ParameterModel body)
        {
            return Task.Run(async () => await DeleteHospitalDoctorAllocatedOPDRoomAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteHospitalDoctorAllocatedOPDRoomAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = hospitalDoctorAllocatedOPDRoomEndpoint.DeleteHospitalDoctorAllocatedOPDRoomAsync();
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

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
        public virtual HospitalDoctorAllocatedOPDRoomListResponse List(string selectedCentreCode, short selectedDepartmentId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(selectedCentreCode, selectedDepartmentId, expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalDoctorAllocatedOPDRoomListResponse> ListAsync(string selectedCentreCode, short selectedDepartmentId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = hospitalDoctorAllocatedOPDRoomEndpoint.ListAsync(selectedCentreCode, selectedDepartmentId, expand, filter, sort, pageIndex, pageSize);
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
        public virtual HospitalDoctorAllocatedOPDRoomResponse GetHospitalDoctorAllocatedOPDRoom(int hospitalDoctorId, int hospitalDoctorAllocatedOPDRoomId)
        {
            return Task.Run(async () => await GetHospitalDoctorAllocatedOPDRoomAsync(hospitalDoctorId, hospitalDoctorAllocatedOPDRoomId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalDoctorAllocatedOPDRoomResponse> GetHospitalDoctorAllocatedOPDRoomAsync(int hospitalDoctorId, int hospitalDoctorAllocatedOPDRoomId, System.Threading.CancellationToken cancellationToken)
        {
            if (hospitalDoctorId <= 0)
                throw new System.ArgumentNullException("hospitalDoctorId");

            string endpoint = hospitalDoctorAllocatedOPDRoomEndpoint.GetHospitalDoctorAllocatedOPDRoomAsync(hospitalDoctorId, hospitalDoctorAllocatedOPDRoomId);
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
    }
}

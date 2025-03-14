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
    public class GeneralPersonAttendanceDetailsClient : BaseClient, IGeneralPersonAttendanceDetailsClient
    {
        GeneralPersonAttendanceDetailsEndpoint generalPersonAttendanceDetailsEndpoint = null;
        public GeneralPersonAttendanceDetailsClient()
        {
            generalPersonAttendanceDetailsEndpoint = new GeneralPersonAttendanceDetailsEndpoint();
        }


        #region General Person Attendance
        public virtual GeneralPersonAttendanceDetailsListResponse GeneralPersonAttendanceDetailsList(long entityId, string userType, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await GeneralPersonAttendanceDetailsListAsync(entityId, userType, expand, filter, sort, pageIndex, pageSize, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralPersonAttendanceDetailsListResponse> GeneralPersonAttendanceDetailsListAsync(long entityId, string userType, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = generalPersonAttendanceDetailsEndpoint.GeneralPersonAttendanceDetailsListAsync(entityId, userType, expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralPersonAttendanceDetailsListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new GeneralPersonAttendanceDetailsListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralPersonAttendanceDetailsListResponse typedBody = JsonConvert.DeserializeObject<GeneralPersonAttendanceDetailsListResponse>(responseData);
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

        public virtual GeneralPersonAttendanceDetailsResponse GetGeneralPersonAttendanceDetails(long generalPersonAttendanceDetailId)
        {
            return Task.Run(async () => await GetGeneralPersonAttendanceDetailsAsync(generalPersonAttendanceDetailId, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralPersonAttendanceDetailsResponse> GetGeneralPersonAttendanceDetailsAsync(long generalPersonAttendanceDetailId, CancellationToken cancellationToken)
        {
            if (generalPersonAttendanceDetailId <= 0)
                throw new System.ArgumentNullException("generalPersonAttendanceDetailId");

            string endpoint = generalPersonAttendanceDetailsEndpoint.GetGeneralPersonAttendanceDetailsAsync(generalPersonAttendanceDetailId);
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralPersonAttendanceDetailsResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new GeneralPersonAttendanceDetailsResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralPersonAttendanceDetailsResponse typedBody = JsonConvert.DeserializeObject<GeneralPersonAttendanceDetailsResponse>(responseData);
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

        public virtual GeneralPersonAttendanceDetailsResponse InserUpdateGeneralPersonAttendanceDetails(GeneralPersonAttendanceDetailsModel body)
        {
            return Task.Run(async () => await InserUpdateGeneralPersonAttendanceDetailsAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralPersonAttendanceDetailsResponse> InserUpdateGeneralPersonAttendanceDetailsAsync(GeneralPersonAttendanceDetailsModel body, CancellationToken cancellationToken)
        {
            string endpoint = generalPersonAttendanceDetailsEndpoint.InserUpdateGeneralPersonAttendanceDetailsAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralPersonAttendanceDetailsResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralPersonAttendanceDetailsResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralPersonAttendanceDetailsResponse typedBody = JsonConvert.DeserializeObject<GeneralPersonAttendanceDetailsResponse>(responseData);
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

        public virtual TrueFalseResponse DeleteGeneralPersonAttendanceDetails(ParameterModel body)
        {
            return Task.Run(async () => await DeleteGeneralPersonAttendanceDetailsAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteGeneralPersonAttendanceDetailsAsync(ParameterModel body, CancellationToken cancellationToken)
        {
            string endpoint = generalPersonAttendanceDetailsEndpoint.DeleteGeneralPersonAttendanceDetailsAsync();
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
        #endregion
    }
}

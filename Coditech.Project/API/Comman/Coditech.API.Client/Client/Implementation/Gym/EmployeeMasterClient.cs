using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;

using Newtonsoft.Json;

namespace Coditech.API.Client
{
    public class EmployeeMasterClient : BaseClient, IEmployeeMasterClient
    {
        EmployeeMasterEndpoint employeeMasterEndpoint = null;
        public EmployeeMasterClient()
        {
            employeeMasterEndpoint = new EmployeeMasterEndpoint();
        }
        public virtual EmployeeMasterListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<EmployeeMasterListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = employeeMasterEndpoint.ListAsync(expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<EmployeeMasterListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new EmployeeMasterListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    EmployeeMasterListResponse typedBody = JsonConvert.DeserializeObject<EmployeeMasterListResponse>(responseData);
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

        public virtual EmployeeMasterResponse GetEmployeeOtherDetails(int emplyeeId)
        {
            return Task.Run(async () => await GetEmployeeMasterAsync(emplyeeId, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<EmployeeMasterResponse> GetEmployeeMasterAsync(int emplyeeId, CancellationToken cancellationToken)
        {
            if (emplyeeId <= 0)
                throw new System.ArgumentNullException("emplyeeId");

            string endpoint = employeeMasterEndpoint.GetEmployeeMasterAsync(emplyeeId);
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
                    var objectResponse = await ReadObjectResponseAsync<EmployeeMasterResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new EmployeeMasterResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    EmployeeMasterResponse typedBody = JsonConvert.DeserializeObject<EmployeeMasterResponse>(responseData);
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

        public virtual EmployeeMasterResponse UpdateEmployeeOtherDetails(EmployeeMasterModel body)
        {
            return Task.Run(async () => await UpdateEmployeeMasterAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<EmployeeMasterResponse> UpdateEmployeeMasterAsync(EmployeeMasterModel body, CancellationToken cancellationToken)
        {
            string endpoint = employeeMasterEndpoint.UpdateEmployeeMasterAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<EmployeeMasterResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<EmployeeMasterResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    EmployeeMasterResponse typedBody = JsonConvert.DeserializeObject<EmployeeMasterResponse>(responseData);
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

        public virtual TrueFalseResponse DeleteEmployee(ParameterModel body)
        {
            return Task.Run(async () => await DeleteEmployeeAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteEmployeeAsync(ParameterModel body, CancellationToken cancellationToken)
        {
            string endpoint = employeeMasterEndpoint.DeleteEmployeeAsync();
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

        //#region Member Follow Up
        //public virtual GymMemberFollowUpListResponse GymMemberFollowUpList(int emplyeeId, long personId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        //{
        //    return Task.Run(async () => await GymMemberFollowUpListAsync(emplyeeId, personId, expand, filter, sort, pageIndex, pageSize, CancellationToken.None)).GetAwaiter().GetResult();
        //}

        //public virtual async Task<GymMemberFollowUpListResponse> GymMemberFollowUpListAsync(int emplyeeId, long personId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        //{
        //    string endpoint = employeeMasterEndpoint.GymMemberFollowUpListAsync(emplyeeId, personId, expand, filter, sort, pageIndex, pageSize);
        //    HttpResponseMessage response = null;
        //    var disposeResponse = true;
        //    try
        //    {
        //        ApiStatus status = new ApiStatus();

        //        response = await GetResourceFromEndpointAsync(endpoint, status, cancellationToken).ConfigureAwait(false);
        //        Dictionary<string, IEnumerable<string>> headers_ = BindHeaders(response);
        //        var status_ = (int)response.StatusCode;
        //        if (status_ == 200)
        //        {
        //            var objectResponse = await ReadObjectResponseAsync<GymMemberFollowUpListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
        //            if (objectResponse.Object == null)
        //            {
        //                throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
        //            }
        //            return objectResponse.Object;
        //        }
        //        else if (status_ == 204)
        //        {
        //            return new GymMemberFollowUpListResponse();
        //        }
        //        else
        //        {
        //            string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        //            GymMemberFollowUpListResponse typedBody = JsonConvert.DeserializeObject<GymMemberFollowUpListResponse>(responseData);
        //            UpdateApiStatus(typedBody, status, response);
        //            throw new CoditechException(status.ErrorCode, status.ErrorMessage, status.StatusCode);
        //        }
        //    }
        //    finally
        //    {
        //        if (disposeResponse)
        //            response.Dispose();
        //    }
        //}

        //#endregion

    }
}

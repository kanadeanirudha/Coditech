using Coditech.API.Data;
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
    public class TaskApprovalSettingClient : BaseClient, ITaskApprovalSettingClient
    {
        TaskApprovalSettingEndpoint taskApprovalSettingEndpoint = null;
        public TaskApprovalSettingClient()
        {
            taskApprovalSettingEndpoint = new TaskApprovalSettingEndpoint();
        }
        public virtual TaskApprovalSettingListResponse List(string SelectedCentreCode, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(SelectedCentreCode, expand, filter, sort, pageIndex, pageSize, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TaskApprovalSettingListResponse> ListAsync(string SelectedCentreCode, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = taskApprovalSettingEndpoint.ListAsync(SelectedCentreCode, expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<TaskApprovalSettingListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new TaskApprovalSettingListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    TaskApprovalSettingListResponse typedBody = JsonConvert.DeserializeObject<TaskApprovalSettingListResponse>(responseData);
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

        //GetTaskApprovalSetting
        public virtual TaskApprovalSettingResponse GetTaskApprovalSetting(short taskMasterId, string centreCode)
        {
            return Task.Run(async () => await GetTaskApprovalSettingAsync(taskMasterId, centreCode, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TaskApprovalSettingResponse> GetTaskApprovalSettingAsync(short taskMasterId, string centreCode, System.Threading.CancellationToken cancellationToken)
        {
            if (taskMasterId <= 0)
                throw new System.ArgumentNullException("TaskMasterId");

            string endpoint = taskApprovalSettingEndpoint.GetTaskApprovalSettingAsync(taskMasterId, centreCode);
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
                    var objectResponse = await ReadObjectResponseAsync<TaskApprovalSettingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new TaskApprovalSettingResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    TaskApprovalSettingResponse typedBody = JsonConvert.DeserializeObject<TaskApprovalSettingResponse>(responseData);
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

        //AddEmployeeList
        public virtual TaskApprovalSettingResponse AddUpdateTaskApprovalSetting(TaskApprovalSettingModel body)
        {
            return Task.Run(async () => await AddUpdateTaskApprovalSettingAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TaskApprovalSettingResponse> AddUpdateTaskApprovalSettingAsync(TaskApprovalSettingModel body, CancellationToken cancellationToken)
        {
            string endpoint = taskApprovalSettingEndpoint.AddUpdateTaskApprovalSettingAsync();
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
                            ObjectResponseResult<TaskApprovalSettingResponse> objectResponseResult2 = await ReadObjectResponseAsync<TaskApprovalSettingResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<TaskApprovalSettingResponse> objectResponseResult = await ReadObjectResponseAsync<TaskApprovalSettingResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            TaskApprovalSettingResponse result = JsonConvert.DeserializeObject<TaskApprovalSettingResponse>(value);
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

        //GetUpdateTaskApprovalSetting
        public virtual TaskApprovalSettingResponse GetUpdateTaskApprovalSetting(short taskMasterId, string centreCode, int taskApprovalSettingId)
        {
            return Task.Run(async () => await GetUpdateTaskApprovalSettingAsync(taskMasterId, centreCode, taskApprovalSettingId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TaskApprovalSettingResponse> GetUpdateTaskApprovalSettingAsync(short taskMasterId, string centreCode, int taskApprovalSettingId, System.Threading.CancellationToken cancellationToken)
        {
            if (taskMasterId <= 0)
                throw new System.ArgumentNullException("TaskMasterId");

            string endpoint = taskApprovalSettingEndpoint.GetUpdateTaskApprovalSettingAsync(taskMasterId, centreCode, taskApprovalSettingId);
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
                    var objectResponse = await ReadObjectResponseAsync<TaskApprovalSettingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new TaskApprovalSettingResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    TaskApprovalSettingResponse typedBody = JsonConvert.DeserializeObject<TaskApprovalSettingResponse>(responseData);
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

        public virtual TaskApprovalSettingResponse UpdateTaskApprovalSetting(TaskApprovalSettingModel body)
        {
            return Task.Run(async () => await UpdateTaskApprovalSettingAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<TaskApprovalSettingResponse> UpdateTaskApprovalSettingAsync(TaskApprovalSettingModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = taskApprovalSettingEndpoint.UpdateTaskApprovalSettingAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<TaskApprovalSettingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<TaskApprovalSettingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    TaskApprovalSettingResponse typedBody = JsonConvert.DeserializeObject<TaskApprovalSettingResponse>(responseData);
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
        public virtual TrueFalseResponse DeleteTaskApprovalSetting(ParameterModel body)
        {
            return Task.Run(async () => await DeleteTaskApprovalSettingAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<TrueFalseResponse> DeleteTaskApprovalSettingAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = taskApprovalSettingEndpoint.DeleteTaskApprovalSettingAsync();
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

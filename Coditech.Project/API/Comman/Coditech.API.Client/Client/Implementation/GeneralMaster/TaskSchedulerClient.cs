using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Coditech.API.Client
{
    public class TaskSchedulerClient : BaseClient, ITaskSchedulerClient
    {
        TaskSchedulerEndpoint taskSchedulerEndpoint = null;
        public TaskSchedulerClient()
        {
            taskSchedulerEndpoint = new TaskSchedulerEndpoint();
        }

        public virtual TaskSchedulerListResponse List(IEnumerable<string> expand)
        {
            return Task.Run(async () => await ListAsync(expand, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TaskSchedulerListResponse> ListAsync(IEnumerable<string> expand, CancellationToken cancellationToken)
        {
            string endpoint = taskSchedulerEndpoint.ListAsync(expand);
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
                    var objectResponse = await ReadObjectResponseAsync<TaskSchedulerListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new TaskSchedulerListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    TaskSchedulerListResponse typedBody = JsonConvert.DeserializeObject<TaskSchedulerListResponse>(responseData);
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

        public virtual TaskSchedulerResponse CreateTaskScheduler(TaskSchedulerModel body)
        {
            return Task.Run(async () => await CreateTaskSchedulerAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TaskSchedulerResponse> CreateTaskSchedulerAsync(TaskSchedulerModel body, CancellationToken cancellationToken)
        {
            string endpoint = taskSchedulerEndpoint.CreateTaskSchedulerAsync();
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
                            ObjectResponseResult<TaskSchedulerResponse> objectResponseResult2 = await ReadObjectResponseAsync<TaskSchedulerResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<TaskSchedulerResponse> objectResponseResult = await ReadObjectResponseAsync<TaskSchedulerResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            TaskSchedulerResponse result = JsonConvert.DeserializeObject<TaskSchedulerResponse>(value);
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

        public virtual TaskSchedulerResponse GetTaskSchedulerDetails(int configuratorId, string schedulerCallFor)
        {
            return Task.Run(async () => await GetTaskSchedulerDetailsAsync(configuratorId, schedulerCallFor, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TaskSchedulerResponse> GetTaskSchedulerDetailsAsync(int configuratorId, string schedulerCallFor, System.Threading.CancellationToken cancellationToken)
        {
            if (configuratorId <= 0)
                throw new System.ArgumentNullException("configuratorId");

            string endpoint = taskSchedulerEndpoint.GetTaskSchedulerDetailsAsync(configuratorId, schedulerCallFor);
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
                    var objectResponse = await ReadObjectResponseAsync<TaskSchedulerResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new TaskSchedulerResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    TaskSchedulerResponse typedBody = JsonConvert.DeserializeObject<TaskSchedulerResponse>(responseData);
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

        public virtual TaskSchedulerResponse UpdateTaskSchedulerDetails(TaskSchedulerModel body)
        {
            return Task.Run(async () => await UpdateTaskSchedulerDetailsAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TaskSchedulerResponse> UpdateTaskSchedulerDetailsAsync(TaskSchedulerModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = taskSchedulerEndpoint.UpdateTaskSchedulerDetailsAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<TaskSchedulerResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<TaskSchedulerResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    TaskSchedulerResponse typedBody = JsonConvert.DeserializeObject<TaskSchedulerResponse>(responseData);
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

        //Delete Task Scheduler
        public virtual TrueFalseResponse DeleteTaskScheduler(ParameterModel body)
        {
            return Task.Run(async () => await DeleteTaskSchedulerAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteTaskSchedulerAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = taskSchedulerEndpoint.DeleteTaskSchedulerAsync();
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

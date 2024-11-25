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


    }
}

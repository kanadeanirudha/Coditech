using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface ITaskSchedulerClient : IBaseClient
    {

        /// <summary>
        /// Create TaskScheduler.
        /// </summary>
        /// <param name="TaskSchedulerModel">TaskSchedulerModel.</param>
        /// <returns>Returns TaskSchedulerResponse.</returns>
        TaskSchedulerResponse CreateBatchTaskScheduler(TaskSchedulerModel body);

        /// <summary>
        /// Get TaskSchedulerResponse by taskSchedulerMasterId.
        /// </summary>
        /// <param name="configuratorId">configuratorId</param>
        /// <param name="schedulerCallFor">schedulerCallFor</param>
        /// <returns>Returns TaskSchedulerResponse.</returns>
        TaskSchedulerResponse GetBatchTaskSchedulerDetails(int configuratorId, string schedulerCallFor);

        /// <summary>
        /// Update TaskScheduler.
        /// </summary>
        /// <param name="TaskSchedulerModel">TaskSchedulerModel.</param>
        /// <returns>Returns updated TaskSchedulerResponse</returns>
        TaskSchedulerResponse UpdateBatchTaskSchedulerDetails(TaskSchedulerModel body);
     
    }
}

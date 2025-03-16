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
        /// Get list of General Task Scheduler.
        /// </summary>
        /// <returns>GeneralTaskSchedulerListResponse</returns>
        TaskSchedulerListResponse List(IEnumerable<string> expand);

        /// <summary>
        /// Create TaskScheduler.
        /// </summary>
        /// <param name="TaskSchedulerModel">TaskSchedulerModel.</param>
        /// <returns>Returns TaskSchedulerResponse.</returns>
        TaskSchedulerResponse CreateTaskScheduler(TaskSchedulerModel body);

        /// <summary>
        /// Get TaskSchedulerResponse by taskSchedulerMasterId.
        /// </summary>
        /// <param name="configuratorId">configuratorId</param>
        /// <param name="schedulerCallFor">schedulerCallFor</param>
        /// <returns>Returns TaskSchedulerResponse.</returns>
        TaskSchedulerResponse GetTaskSchedulerDetails(int configuratorId, string schedulerCallFor);

        /// <summary>
        /// Update TaskScheduler.
        /// </summary>
        /// <param name="TaskSchedulerModel">TaskSchedulerModel.</param>
        /// <returns>Returns updated TaskSchedulerResponse</returns>
        TaskSchedulerResponse UpdateTaskSchedulerDetails(TaskSchedulerModel body);

        /// <summary>
        /// Delete TaskScheduler.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteTaskScheduler(ParameterModel parameterModel);

        /// <summary>
        /// Get TaskSchedulerResponse by taskSchedulerMasterId.
        /// </summary>
        /// <param name="starTime">configuratorId</param>
        /// <returns>Returns TaskSchedulerResponse.</returns>
        TaskSchedulerResponse GetExecuteTaskScheduler(DateTime starTime);
    }
}

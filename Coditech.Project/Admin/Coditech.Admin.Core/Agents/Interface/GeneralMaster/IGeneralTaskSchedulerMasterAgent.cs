using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGeneralTaskSchedulerMasterAgent
    {

        /// <summary>
        /// Get list of General Task Scheduler.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>TaskSchedulerListViewModel</returns>
        TaskSchedulerListViewModel GetTaskSchedulerList(DataTableViewModel dataTableModel);

        TaskSchedulerViewModel CreateTaskScheduler(TaskSchedulerViewModel taskSchedulerViewModel);

        /// <summary>
        /// Get taskScheduler by TaskSchedulerMasterId.
        /// </summary>
        /// <param name="configuratorId">configuratorId</param>
        /// <param name="schedulerCallFor">schedulerCallFor</param>
        /// <returns>Returns TaskSchedulerViewModel.</returns>
        TaskSchedulerViewModel GetTaskSchedulerDetails(int configuratorId, string schedulerCallFor);

        /// <summary>
        /// Update General Scheduler.
        /// </summary>
        /// <param name="taskSchedulerViewModel">taskSchedulerViewModel.</param>
        /// <returns>Returns updated taskSchedulerViewModel</returns>
        TaskSchedulerViewModel UpdateTaskSchedulerDetails(TaskSchedulerViewModel taskSchedulerViewModel);
        /// <summary>
        /// Delete  General Scheduler.
        /// </summary>
        /// <param name="taskSchedulerMasterIds">generalBatchMasterId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteTaskScheduler(string taskSchedulerMasterIds, out string message);

        /// <summary>
        /// Get taskScheduler by TaskSchedulerMasterId.
        /// </summary>
        /// <param name="startTime">schedulerCallFor</param>
        /// <returns>Returns TaskSchedulerViewModel.</returns>
        TaskSchedulerViewModel GetExecuteTaskScheduler(DateTime startTime);
    }
}

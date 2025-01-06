using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface ITaskMasterAgent
    {
        /// <summary>
        /// Get list of Task Master.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>TaskMasterListViewModel</returns>
        TaskMasterListViewModel GetTaskMasterList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Task Master.
        /// </summary>
        /// <param name="TaskMasterViewModel">TaskMaster View Model.</param>
        /// <returns>Returns created model.</returns>
        TaskMasterViewModel CreateTaskMaster(TaskMasterViewModel taskMasterViewModel);

        /// <summary>
        /// Get Task Master by taskMasterId.
        /// </summary>
        /// <param name="taskMasterId">taskMasterId</param>
        /// <returns>Returns TaskMasterViewModel.</returns>
        TaskMasterViewModel GetTaskMaster(short taskMasterId);

        /// <summary>
        /// Update TaskMaster.
        /// </summary>
        /// <param name="taskMasterViewModel">taskMasterViewModel.</param>
        /// <returns>Returns updated taskMasterViewModel</returns>
        TaskMasterViewModel UpdateTaskMaster(TaskMasterViewModel taskMasterViewModel);

        /// <summary>
        /// Delete TaskMaster.
        /// </summary>
        /// <param name="taskMasterId">taskMasterId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteTaskMaster(string taskMasterId, out string errorMessage);
        TaskMasterListResponse GetTaskMasterList();
    }
}

using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface ITaskMasterClient : IBaseClient
    {
        /// <summary>
        /// Get list of TaskMaster.
        /// </summary>
        /// <returns>TaskMasterListResponse</returns>
        TaskMasterListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create TaskMaster.
        /// </summary>
        /// <param name="TaskMasterModel">TaskMasterModel.</param>
        /// <returns>Returns TaskMasterResponse.</returns>
        TaskMasterResponse CreateTaskMaster(TaskMasterModel body);

        /// <summary>
        /// Get TaskMaster by taskMasterId.
        /// </summary>
        /// <param name="taskMasterId">taskMasterId</param>
        /// <returns>Returns TaskMasterResponse.</returns>
        TaskMasterResponse GetTaskMaster(short taskMasterId);

        /// <summary>
        /// Update TaskMaster.
        /// </summary>
        /// <param name="TaskMasterModel">TaskMasterModel.</param>
        /// <returns>Returns updated TaskMasterResponse</returns>
        TaskMasterResponse UpdateTaskMaster(TaskMasterModel body);

        /// <summary>
        /// Delete TaskMaster.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteTaskMaster(ParameterModel body);
    }
}

using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;

namespace Coditech.Admin.Agents
{
    public interface ITaskApprovalSettingAgent
    {
        /// <summary>
        /// Get list of Task Approval Setting.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>TaskApprovalSettingListViewModel</returns>
        TaskApprovalSettingListViewModel GetTaskApprovalSettingList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Get TaskApprovalSetting by TaskApprovalSettingId.
        /// </summary>
        /// <param name="centreCode">centreCode</param>
        /// <param name="taskMasterId">taskApprovalSettingId</param>
        /// <returns>Returns TaskApprovalSettingViewModel.</returns>
        TaskApprovalSettingViewModel GetTaskApprovalSetting(short taskMasterId, string centreCode);

        // <summary>
        /// Get TaskApprovalSetting by TaskApprovalSettingId.
        /// </summary>
        /// <param name="centreCode">centreCode</param>
        /// <returns>Returns EmployeeMasterModel.</returns>
        List<EmployeeMasterModel> GetEmployeeListByCentreCode(string centreCode);


        /// <summary>
        /// Create AddUpdateTaskApprovalSetting.
        /// </summary>
        /// <param name="taskApprovalSettingViewModel">TaskApproval Setting  View Model.</param>
        /// <returns>Returns created model.</returns>
        TaskApprovalSettingViewModel AddUpdateTaskApprovalSetting(TaskApprovalSettingViewModel taskApprovalSettingViewModel);
    }
}

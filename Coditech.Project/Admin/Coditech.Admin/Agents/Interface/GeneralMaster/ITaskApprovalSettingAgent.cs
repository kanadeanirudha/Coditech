using Coditech.Admin.ViewModel;

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
    }
}

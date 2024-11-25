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
       
    }
}

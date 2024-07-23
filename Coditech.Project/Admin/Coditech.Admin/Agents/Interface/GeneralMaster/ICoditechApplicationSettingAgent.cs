using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface ICoditechApplicationSettingAgent
    {
        /// <summary>
        /// Get list of Coditech Application Setting.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>CoditechApplicationSettingListViewModel</returns>
        CoditechApplicationSettingListViewModel GetCoditechApplicationSettingList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Coditech Application Setting.
        /// </summary>
        /// <param name="coditechApplicationSettingViewModel">Coditech Application Setting View Model.</param>
        /// <returns>Returns created model.</returns>
        CoditechApplicationSettingViewModel CreateCoditechApplicationSetting(CoditechApplicationSettingViewModel coditechApplicationSettingViewModel);

        /// <summary>
        /// Get Coditech Application Setting by coditechApplicationSettingId.
        /// </summary>
        /// <param name="coditechApplicationSettingId">coditechApplicationSettingId</param>
        /// <returns>Returns CoditechApplicationSettingViewModel.</returns>
        CoditechApplicationSettingViewModel GetCoditechApplicationSetting(short coditechApplicationSettingId);

        /// <summary>
        /// Update Coditech Application Setting.
        /// </summary>
        /// <param name="coditechApplicationSettingViewModel">coditechApplicationSettingViewModel.</param>
        /// <returns>Returns updated CoditechApplicationSettingViewModel</returns>
        CoditechApplicationSettingViewModel UpdateCoditechApplicationSetting(CoditechApplicationSettingViewModel coditechApplicationSettingViewModel);

        /// <summary>
        /// Delete Coditech Application Setting.
        /// </summary>
        /// <param name="coditechApplicationSettingIds">coditechApplicationSettingId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteCoditechApplicationSetting(string coditechApplicationSettingIds, out string errorMessage);
        CoditechApplicationSettingListResponse GetCoditechApplicationSettingList();
    }
}

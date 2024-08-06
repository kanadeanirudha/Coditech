using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IMediaSettingMasterAgent
    {
        /// <summary>
        /// Get list of Media Setting Master.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>MediaSettingMasterListViewModel</returns>
        MediaSettingMasterListViewModel GetMediaSettingMasterList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Get MediaSettingMaster by mediaTypeMasterId.
        /// </summary>
        /// <param name="mediaTypeMasterId">mediaTypeMasterId</param>
        /// <returns>Returns MediaSettingMasterViewModel.</returns>
        MediaSettingMasterViewModel GetMediaSettingMaster(byte mediaTypeMasterId);

        /// <summary>
        /// Update Media Setting Master.
        /// </summary>
        /// <param name="mediaSettingMasterViewModel">mediaSettingMasterViewModel.</param>
        /// <returns>Returns updated MediaSettingMasterViewModel</returns>
        MediaSettingMasterViewModel UpdateMediaSettingMaster(MediaSettingMasterViewModel mediaSettingMasterViewModel);
    }
}

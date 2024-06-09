using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

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
        /// Create Media Setting Master.
        /// </summary>
        /// <param name="mediaSettingMasterViewModel">General MediaSettingMaster View Model.</param>
        /// <returns>Returns created model.</returns>
        MediaSettingMasterViewModel CreateMediaSettingMaster(MediaSettingMasterViewModel mediaSettingMasterViewModel);

        /// <summary>
        /// Get MediaSettingMaster by mediaSettingMasterId.
        /// </summary>
        /// <param name="mediaSettingMasterId">mediaSettingMasterId</param>
        /// <returns>Returns MediaSettingMasterViewModel.</returns>
        MediaSettingMasterViewModel GetMediaSettingMaster(short mediaSettingMasterId);

        /// <summary>
        /// Update Media Setting Master.
        /// </summary>
        /// <param name="mediaSettingMasterViewModel">mediaSettingMasterViewModel.</param>
        /// <returns>Returns updated MediaSettingMasterViewModel</returns>
        MediaSettingMasterViewModel UpdateMediaSettingMaster(MediaSettingMasterViewModel mediaSettingMasterViewModel);

        /// <summary>
        /// Delete Media Setting Master.
        /// </summary>
        /// <param name="mediaSettingMasterId">mediaSettingMasterId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteMediaSettingMaster(string mediaSettingMasterId, out string errorMessage);
        MediaSettingMasterListResponse GetMediaSettingMasterList();
    }
}

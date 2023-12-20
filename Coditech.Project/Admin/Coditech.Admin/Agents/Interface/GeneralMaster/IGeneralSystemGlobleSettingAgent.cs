using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralSystemGlobleSettingAgent
    {
        /// <summary>
        /// Get list of General System Globle Setting.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralSystemGlobleSettingListViewModel</returns>
        GeneralSystemGlobleSettingListViewModel GetSystemGlobleSettingList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create System Globle Setting.
        /// </summary>
        /// <param name="generalSystemGlobleSettingViewModel">General System Globle Setting View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralSystemGlobleSettingViewModel CreateSystemGlobleSetting(GeneralSystemGlobleSettingViewModel generalSystemGlobleSettingViewModel);

        /// <summary>
        /// Get System Globle Setting by generalSystemGlobleSettingId.
        /// </summary>
        /// <param name="generalSystemGlobleSettingId">generalSystemGlobleSettingId</param>
        /// <returns>Returns GeneralSystemGlobleSettingViewModel.</returns>
        GeneralSystemGlobleSettingViewModel GetSystemGlobleSetting(short generalSystemGlobleSettingId);

        /// <summary>
        /// Update System Globle Setting.
        /// </summary>
        /// <param name="generalSystemGlobleSettingViewModel">generalSystemGlobleSettingViewModel.</param>
        /// <returns>Returns updated GeneralSystemGlobleSettingViewModel</returns>
        GeneralSystemGlobleSettingViewModel UpdateSystemGlobleSetting(GeneralSystemGlobleSettingViewModel generalCountryViewModel);

        /// <summary>
        /// Delete System Globle Setting.
        /// </summary>
        /// <param name="generalSystemGlobleSettingId">generalSystemGlobleSettingId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteSystemGlobleSetting(string generalSystemGlobleSettingId, out string errorMessage);
        GeneralSystemGlobleSettingListResponse GetSystemGlobleSettingList();
    }
}

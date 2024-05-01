using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralUserMainMenuAgent
    {
        /// <summary>
        /// Get list of UserMainMenu.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralUserMainMenuListViewModel</returns>
        GeneralUserMainMenuListViewModel GetUserMainMenuList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create UserMainMenu.
        /// </summary>
        /// <param name="generalUserMainnMenuViewModel">General UserMainMenu View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralUserMainnMenuViewModel CreateUserMainMenu(GeneralUserMainnMenuViewModel generalUserMainnMenuViewModel);

        /// <summary>
        /// Get UserMainMenu by generalUserMainMenuId.
        /// </summary>
        /// <param name="generalUserMainMenuId">generalUserMainMenuId</param>
        /// <returns>Returns GeneralUserMainnMenuViewModel.</returns>
        GeneralUserMainnMenuViewModel GetUserMainMenu(short generalUserMainMenuId);

        /// <summary>
        /// Update UserMainMenu.
        /// </summary>
        /// <param name="generalUserMainnMenuViewModel">generalUserMainnMenuViewModel.</param>
        /// <returns>Returns updated GeneralUserMainnMenuViewModel</returns>
        GeneralUserMainnMenuViewModel UpdateUserMainMenu(GeneralUserMainnMenuViewModel generalUserMainnMenuViewModel);

        /// <summary>
        /// Delete UserMainMenu.
        /// </summary>
        /// <param name="generalUserMainMenuId">generalUserMainMenuId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteUserMainMenu(string generalUserMainMenuId, out string errorMessage);
        GeneralUserMainMenuListResponse GetUserMainMenuList();
    }
}

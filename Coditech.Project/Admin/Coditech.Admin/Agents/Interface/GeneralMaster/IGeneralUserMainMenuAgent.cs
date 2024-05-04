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
        UserMainMenuListViewModel GetUserMainMenuList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create UserMainMenu.
        /// </summary>
        /// <param name="UserMainMenuViewModel">General UserMainMenu View Model.</param>
        /// <returns>Returns created model.</returns>
        UserMainMenuViewModel CreateUserMainMenu(UserMainMenuViewModel UserMainMenuViewModel);

        /// <summary>
        /// Get UserMainMenu by generalUserMainMenuId.
        /// </summary>
        /// <param name="generalUserMainMenuId">generalUserMainMenuId</param>
        /// <returns>Returns UserMainMenuViewModel.</returns>
        UserMainMenuViewModel GetUserMainMenu(short generalUserMainMenuId);

        /// <summary>
        /// Update UserMainMenu.
        /// </summary>
        /// <param name="UserMainMenuViewModel">UserMainMenuViewModel.</param>
        /// <returns>Returns updated UserMainMenuViewModel</returns>
        UserMainMenuViewModel UpdateUserMainMenu(UserMainMenuViewModel UserMainMenuViewModel);

        /// <summary>
        /// Delete UserMainMenu.
        /// </summary>
        /// <param name="generalUserMainMenuId">generalUserMainMenuId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteUserMainMenu(string generalUserMainMenuId, out string errorMessage);
        GeneralUserMainMenuListResponse GetUserMainMenuList();
    }
}

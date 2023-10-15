using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IAdminRoleMasterAgent
    {
        /// <summary>
        /// Get list of AdminRole.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>AdminRoleListViewModel</returns>
        AdminRoleListViewModel GetAdminRoleList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Get AdminRole by adminRoleId.
        /// </summary>
        /// <param name="adminRoleId">adminRoleId</param>
        /// <returns>Returns AdminRoleViewModel.</returns>
        AdminRoleViewModel GetAdminRoleDetailsById(int adminRoleId);

        /// <summary>
        /// Update AdminRole.
        /// </summary>
        /// <param name="generalAdminRoleViewModel">generalAdminRoleViewModel.</param>
        /// <returns>Returns updated AdminRoleViewModel</returns>
        AdminRoleViewModel UpdateAdminRole(AdminRoleViewModel generalAdminRoleViewModel);

        /// <summary>
        /// Delete AdminRole.
        /// </summary>
        /// <param name="adminRoleId">adminRoleId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteAdminRole(string adminRoleIds, out string errorMessage);
    }
}

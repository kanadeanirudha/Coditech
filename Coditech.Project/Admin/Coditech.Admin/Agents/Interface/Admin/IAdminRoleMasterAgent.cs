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
        /// <param name="adminRoleViewModel">adminRoleViewModel.</param>
        /// <returns>Returns updated AdminRoleViewModel</returns>
        AdminRoleViewModel UpdateAdminRole(AdminRoleViewModel adminRoleViewModel);

        /// <summary>
        /// Delete AdminRole.
        /// </summary>
        /// <param name="adminRoleId">adminRoleId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteAdminRole(string adminRoleIds, out string errorMessage);

        /// <summary>
        /// Get Admin Role Menu Details by adminRoleId.
        /// </summary>
        /// <param name="adminRoleId">adminRoleId</param>
        /// <returns>Returns AdminRoleMenuDetailsViewModel.</returns>
        AdminRoleMenuDetailsViewModel GetAdminRoleMenuDetailsById(int adminRoleMasterId, string moduleCode);

        /// <summary>
        /// Insert Update Admin Role Menu Details
        /// </summary>
        /// <param name="adminRoleViewModel">adminRoleMenuDetailsViewModel.</param>
        /// <returns>Returns updated adminRoleMenuDetailsViewModel</returns>
        AdminRoleMenuDetailsViewModel InsertUpdateAdminRoleMenuDetails(AdminRoleMenuDetailsViewModel adminRoleMenuDetailsViewModel);

        /// <summary>
        /// Get Role Allocated To User List
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>AdminRoleApplicableDetailsListViewModel</returns>
        AdminRoleApplicableDetailsListViewModel RoleAllocatedToUserList(int adminRoleMasterId, DataTableViewModel dataTableModel);
    }
}

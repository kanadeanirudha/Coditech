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

        /// <summary>
        /// Get Admin Role Menu Details by AdminRoleMasterId.
        /// </summary>
        /// <param name="adminRoleMasterId">adminRoleMasterId</param>
        /// <param name="adminRoleApplicableDetailId">adminRoleApplicableDetailId</param>
        /// <returns>Returns AdminRoleApplicableDetailsViewModel.</returns>
        AdminRoleApplicableDetailsViewModel GetAssociateUnAssociateAdminRoleToUser(int adminRoleMasterId, int adminRoleApplicableDetailId);

        /// <summary>
        /// Associate UnAssociate Admin Role To User
        /// </summary>
        /// <param name="adminRoleViewModel">AdminRoleApplicableDetailsViewModel.</param>
        /// <returns>Returns AdminRoleApplicableDetailsViewModel</returns>
        AdminRoleApplicableDetailsViewModel AssociateUnAssociateAdminRoleToUser(AdminRoleApplicableDetailsViewModel adminRoleApplicableDetailsViewModel);

        /// <summary>
        /// Get Admin Role Wise Media Folder Action By Id
        /// </summary>
        /// <param name="adminRoleId">adminRoleId</param>
        /// <returns>Returns AdminRoleMediaFolderActionViewModel.</returns>
        AdminRoleMediaFolderActionViewModel GetAdminRoleWiseMediaFolderActionById(int adminRoleMasterId);

        /// <summary>
        /// Update Admin RoleWise Media Folder Action.
        /// </summary>
        /// <param name="adminRoleMediaFolderActionViewModel">adminRoleMediaFolderActionViewModel.</param>
        /// <returns>Returns updated adminRoleMediaFolderActionViewModel</returns>
        AdminRoleMediaFolderActionViewModel InsertUpdateAdminRoleWiseMediaFolderAction(AdminRoleMediaFolderActionViewModel adminRoleMediaFolderActionViewModel);
    }
}

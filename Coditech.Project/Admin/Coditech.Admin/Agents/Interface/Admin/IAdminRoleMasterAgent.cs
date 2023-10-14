using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IAdminRoleMasterAgent
    {
        /// <summary>
        /// Get list of AdminRoleMaster.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>AdminRoleMasterListViewModel</returns>
        AdminRoleListViewModel GetAdminRoleMasterList(DataTableViewModel dataTableModel);
        
        ///// <summary>
        ///// Create AdminRoleMaster.
        ///// </summary>
        ///// <param name="generalAdminRoleMasterViewModel">AdminRoleMaster View Model.</param>
        ///// <returns>Returns created model.</returns>
        //AdminRoleMasterViewModel CreateAdminRoleMaster(AdminRoleMasterViewModel generalAdminRoleMasterViewModel);

        ///// <summary>
        ///// Get AdminRoleMaster by adminRoleMasterId.
        ///// </summary>
        ///// <param name="adminRoleMasterId">adminRoleMasterId</param>
        ///// <returns>Returns AdminRoleMasterViewModel.</returns>
        //AdminRoleMasterViewModel GetAdminRoleMaster(int adminRoleMasterId);

        ///// <summary>
        ///// Update AdminRoleMaster.
        ///// </summary>
        ///// <param name="generalAdminRoleMasterViewModel">generalAdminRoleMasterViewModel.</param>
        ///// <returns>Returns updated AdminRoleMasterViewModel</returns>
        //AdminRoleMasterViewModel UpdateAdminRoleMaster(AdminRoleMasterViewModel generalAdminRoleMasterViewModel);

        ///// <summary>
        ///// Delete AdminRoleMaster.
        ///// </summary>
        ///// <param name="adminRoleMasterId">adminRoleMasterId.</param>
        ///// <returns>Returns true if deleted successfully else return false.</returns>
        //bool DeleteAdminRoleMaster(string adminRoleMasterIds, out string errorMessage);
    }
}

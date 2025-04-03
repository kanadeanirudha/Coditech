using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralUserModuleAgent
    {
        /// <summary>
        /// Get list of UserModule.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralUserModuleListViewModel</returns>
        UserModuleListViewModel GetUserModuleList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create UserModule.
        /// </summary>
        /// <param name="UserModuleViewModel">General User Module View Model.</param>
        /// <returns>Returns created model.</returns>
        UserModuleViewModel CreateUserModule(UserModuleViewModel UserModuleViewModel);

        /// <summary>
        /// Get UserModule by userModuleMasterId.
        /// </summary>
        /// <param name="userModuleMasterId">userModuleMasterId</param>
        /// <returns>Returns UserModuleViewModel.</returns>
        UserModuleViewModel GetUserModule(short userModuleMasterId);

        /// <summary>
        /// Update UserModule.
        /// </summary>
        /// <param name="generalUserModuleViewModel">UserModuleViewModel.</param>
        /// <returns>Returns updated UserModuleViewModel</returns>
        UserModuleViewModel UpdateUserModule(UserModuleViewModel generalUserModuleViewModel);

        /// <summary>
        /// Delete UserModule.
        /// </summary>
        /// <param name="userModuleMasterId">userModuleMasterId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteUserModule(string userModuleMasterId, out string errorMessage);
    }
}

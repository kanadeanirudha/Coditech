using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IUserTypeAgent
    {
        /// <summary>
        /// Get list of User Type.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>UserTypeListViewModel</returns>
        UserTypeListViewModel GetUserTypeList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create User Type.
        /// </summary>
        /// <param name="userTypeViewModel">General User Type View Model.</param>
        /// <returns>Returns created model.</returns>
        UserTypeViewModel CreateUserType(UserTypeViewModel userTypeViewModel);

        /// <summary>
        /// Get UserType by userTypeId.
        /// </summary>
        /// <param name="userTypeId">userTypeId</param>
        /// <returns>Returns UserTypeViewModel.</returns>
        UserTypeViewModel GetUserType(short userTypeId);

        /// <summary>
        /// Update UserType.
        /// </summary>
        /// <param name="userTypeViewModel">userTypeViewModel.</param>
        /// <returns>Returns updated UserTypeViewModel</returns>
        UserTypeViewModel UpdateUserType(UserTypeViewModel userTypeViewModel);

        /// <summary>
        /// Delete UserType.
        /// </summary>
        /// <param name="userTypeId">userTypeId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
       // bool DeleteUserType(string userTypeId, out string errorMessage);
        //UserTypeListResponse GetUserTypeList();

    }
}

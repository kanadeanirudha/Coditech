using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IAdminRoleMasterClient : IBaseClient
    {
        /// <summary>
        /// Get list of AdminRoleMaster.
        /// </summary>
        /// <returns>AdminRoleListResponse</returns>
        AdminRoleListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Get AdminRoleMaster by AdminRoleMasterId.
        /// </summary>
        /// <param name="adminRoleMasterId">adminRoleMasterId</param>
        /// <returns>Returns AdminRoleResponse.</returns>
        AdminRoleResponse GetAdminRoleDetailsById(int adminRoleMasterId);

        /// <summary>
        /// Update AdminRoleMaster.
        /// </summary>
        /// <param name="AdminRoleModel">AdminRoleModel.</param>
        /// <returns>Returns updated AdminRoleResponse</returns>
        AdminRoleResponse UpdateAdminRole(AdminRoleModel body);

        /// <summary>
        /// Delete AdminRoleMaster.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteAdminRole(ParameterModel body);
    }
}

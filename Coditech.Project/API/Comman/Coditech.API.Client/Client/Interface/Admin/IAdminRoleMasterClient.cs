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

        /// <summary>
        /// Get Admin Role Menu Details by AdminRoleMasterId.
        /// </summary>
        /// <param name="adminRoleMasterId">adminRoleMasterId</param>
        /// <returns>Returns AdminRoleResponse.</returns>
        AdminRoleMenuDetailsResponse GetAdminRoleMenuDetailsById(int adminRoleMasterId, string moduleCode);

        /// <summary>
        /// Insert Update Admin Role Menu Details
        /// </summary>
        /// <param name="AdminRoleModel">AdminRoleMenuDetailsModel.</param>
        /// <returns>Returns updated AdminRoleMenuDetailsResponse</returns>
        AdminRoleMenuDetailsResponse InsertUpdateAdminRoleMenuDetails(AdminRoleMenuDetailsModel body);

        /// <summary>
        /// Role Allocated To User List.
        /// </summary>
        /// <returns>AdminRoleApplicableDetailsListResponse</returns>
        AdminRoleApplicableDetailsListResponse RoleAllocatedToUserList(int adminRoleMasterId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);
    }
}

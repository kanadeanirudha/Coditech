using Coditech.Common.API.Model.Response;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IAdminRoleMasterClient : IBaseClient
    {
        /// <summary>
        /// Get list of AdminRoleMaster.
        /// </summary>
        /// <returns>AdminRoleMasterListResponse</returns>
        AdminRoleListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        ///// <summary>
        ///// Create AdminRoleMaster.
        ///// </summary>
        ///// <param name="AdminRoleMasterModel">AdminRoleMasterModel.</param>
        ///// <returns>Returns AdminRoleMasterResponse.</returns>
        //AdminRoleMasterResponse CreateAdminRoleMaster(AdminRoleMasterModel body);

        ///// <summary>
        ///// Get AdminRoleMaster by AdminRoleMasterId.
        ///// </summary>
        ///// <param name="adminRoleMasterId">adminRoleMasterId</param>
        ///// <returns>Returns AdminRoleMasterResponse.</returns>
        //AdminRoleMasterResponse GetAdminRoleMaster(int adminRoleMasterId);

        ///// <summary>
        ///// Update AdminRoleMaster.
        ///// </summary>
        ///// <param name="AdminRoleMasterModel">AdminRoleMasterModel.</param>
        ///// <returns>Returns updated AdminRoleMasterResponse</returns>
        //AdminRoleMasterResponse UpdateAdminRoleMaster(AdminRoleMasterModel body);

        ///// <summary>
        ///// Delete AdminRoleMaster.
        ///// </summary>
        ///// <param name="ParameterModel">ParameterModel.</param>
        ///// <returns>Returns true if deleted successfully else return false.</returns>
        //TrueFalseResponse DeleteAdminRoleMaster(ParameterModel body);
    }
}

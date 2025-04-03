using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IUserTypeClient : IBaseClient
    {
        /// <summary>
        /// Get list of  User Type.
        /// </summary>
        /// <returns>UserTypeListResponse</returns>
        UserTypeListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create User Type.
        /// </summary>
        /// <param name="UserTypeModel">UserTypeModel.</param>
        /// <returns>Returns UserTypeResponse.</returns>
        UserTypeResponse CreateUserType(UserTypeModel body);

        /// <summary>
        /// Get UserType by UserTypeId.
        /// </summary>
        /// <param name="UserTypeId">UserTypeId</param>
        /// <returns>Returns UserTypeResponse.</returns>
        UserTypeResponse GetUserType(short userTypeId);

        /// <summary>
        /// Update UserType.
        /// </summary>
        /// <param name="UserTypeModel">UserTypeModel.</param>
        /// <returns>Returns updated UserTypeResponse</returns>
        UserTypeResponse UpdateUserType(UserTypeModel body);

        /// <summary>
        /// Delete UserType.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
       // UserTypeResponse DeleteUserType( ParameterModel body);

    }
}

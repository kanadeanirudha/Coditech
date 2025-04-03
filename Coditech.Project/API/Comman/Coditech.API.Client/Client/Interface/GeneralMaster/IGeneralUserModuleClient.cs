using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralUserModuleClient : IBaseClient
    {
        /// <summary>
        /// Get list of General UserModule.
        /// </summary>
        /// <returns>GeneralUserModuleListResponse</returns>
        UserModuleListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create UserModule.
        /// </summary>
        /// <param name="GeneralUserModuleModel">GeneralUserModuleModel.</param>
        /// <returns>Returns GeneralUserModuleResponse.</returns>
        GeneralUserModuleResponse CreateUserModule(UserModuleModel body);

        /// <summary>
        /// Get UserModule by userModuleMasterId.
        /// </summary>
        /// <param name="userModuleMasterId">userModuleMasterId</param>
        /// <returns>Returns GeneralUserModuleResponse.</returns>
        GeneralUserModuleResponse GetUserModule(short userModuleMasterId);

        /// <summary>
        /// Update UserModule.
        /// </summary>
        /// <param name="GeneralUserModuleModel">GeneralUserModuleModel.</param>
        /// <returns>Returns updated GeneralUserModuleResponse</returns>
        GeneralUserModuleResponse UpdateUserModule(UserModuleModel body);

        /// <summary>
        /// Delete UserModule.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteUserModule(ParameterModel body);
    }
}

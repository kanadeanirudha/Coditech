using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralUserMainMenuClient : IBaseClient
    {
        /// <summary>
        /// Get list of General UserMainMenu.
        /// </summary>
        /// <returns>GeneralUserMainMenuListResponse</returns>
        GeneralUserMainMenuListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create UserMainMenu.
        /// </summary>
        /// <param name="GeneralUserMainMenuModel">GeneralUserMainMenuModel.</param>
        /// <returns>Returns GeneralUserMainMenuResponse.</returns>
        GeneralUserMainMenuResponse CreateUserMainMenu(UserMainMenuModel body);

        /// <summary>
        /// Get UserMainMenu by generalUserMainMenuId.
        /// </summary>
        /// <param name="generalUserMainMenuId">generalUserMainMenuId</param>
        /// <returns>Returns GeneralUserMainMenuResponse.</returns>
        GeneralUserMainMenuResponse GetUserMainMenu(short generalUserMainMenuId);

        /// <summary>
        /// Update UserMainMenu.
        /// </summary>
        /// <param name="GeneralUserMainMenuModel">GeneralUserMainMenuModel.</param>
        /// <returns>Returns updated GeneralUserMainMenuResponse</returns>
        GeneralUserMainMenuResponse UpdateUserMainMenu(UserMainMenuModel body);

        /// <summary>
        /// Delete UserMainMenu.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteUserMainMenu(ParameterModel body);
    }
}

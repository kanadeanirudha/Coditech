using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;

namespace Coditech.API.Client
{
    public interface IUserClient : IBaseClient
    {
        /// <summary>
        /// Login to application.
        /// </summary>
        /// <param name="body">User Model.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        UserModel Login(IEnumerable<string> expand, UserLoginModel body);

        /// <summary>
        /// Get Active Module List.
        /// </summary>
        /// <returns>Returns UserModuleListResponse.</returns>
        UserModuleListResponse GetActiveModuleList();

        /// <summary>
        /// Get Active Menu List List by moduleCode.
        /// </summary>
        /// <param name="moduleCode">moduleCode</param>
        /// <returns>Returns UserMainMenuResponse.</returns>
        UserMenuListResponse GetActiveMenuList(string moduleCode);

        /// <summary>
        /// Create Person.
        /// </summary>
        /// <param name="GeneralPersonModel">GeneralPersonModel.</param>
        /// <returns>Returns GeneralPersonResponse.</returns>
        GeneralPersonResponse InsertPersonInformation(GeneralPersonModel body);

        /// <summary>
        /// Get GeneralPerson by personId.
        /// </summary>
        /// <param name="personId">personId</param>
        /// <returns>Returns GeneralPersonResponse.</returns>
        GeneralPersonResponse GetPersonInformation(long personId);

        /// <summary>
        /// Update GeneralPerson.
        /// </summary>
        /// <param name="GeneralPersonModel">GeneralPersonModel.</param>
        /// <returns>Returns updated GeneralPersonResponse</returns>
        GeneralPersonResponse UpdatePersonInformation(GeneralPersonModel body);
    }
}

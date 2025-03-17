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
        /// Change Password.
        /// </summary>
        /// <param name="ChangePasswordModel">ChangePasswordModel.</param>
        /// <returns>Returns ChangePasswordResponse.</returns>
        ChangePasswordResponse ChangePassword(ChangePasswordModel body);

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
        UserMainMenuListResponse GetActiveMenuList(string moduleCode);

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

        /// <summary>
        /// Get GeneralPerson Address Details by personId.
        /// </summary>
        /// <param name="personId">personId</param>
        /// <returns>Returns GeneralPersonAddressListResponse</returns>
        GeneralPersonAddressListResponse GetGeneralPersonAddresses(long personId);

        /// <summary>
        /// Update GeneralPerson.
        /// </summary>
        /// <param name="GeneralPersonModel">GeneralPersonAddressDetailModel.</param>
        /// <returns>Returns updated GeneralPersonResponse</returns>
        GeneralPersonAddressResponse InsertUpdateGeneralPersonAddress(GeneralPersonAddressModel model);

        /// <summary>
        /// Reset Password.
        /// </summary>
        /// <param name="resetPasswordModel">GeneralPersonModel.</param>
        /// <returns>Returns ResetPasswordResponse.</returns>
        ResetPasswordResponse ResetPassword(ResetPasswordModel resetPasswordModel);

        /// <summary>
        /// Reset send link Password.
        /// </summary>
        /// <param name="userName">UserModel.</param>
        /// <returns>Returns ResetPasswordResponse.</returns>
        ResetPasswordSendLinkResponse ResetPasswordSendLink(string userName);
        UserTypeListResponse GetUserTypeList();
    }
}

using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IUserAgent
    {
        /// <summary>
        /// This method is used to login the user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return the login details.</returns>
        UserLoginViewModel Login(UserLoginViewModel model);

        /// <summary>
        /// This method is used to logout the user.
        /// </summary>
        Task Logout();

        /// Get Active Module List
        UserModuleListViewModel GetActiveModuleList();

        /// <summary>
        /// Get Active Menu List by moduleCode.
        /// </summary>
        /// <param name="moduleCode">moduleCode</param>
        /// <returns>Returns UserMenuListViewModel.</returns>
        UserMenuListViewModel GetActiveMenuList(string moduleCode);

        /// <summary>
        /// Get Person Address Details by personId.
        /// </summary>
        /// <param name="personId">personId</param>
        /// <returns>Returns GeneralPersonAddressListViewModel.</returns>
        GeneralPersonAddressListViewModel GetGeneralPersonAddresses(long personId);
        /// <summary>
        /// Update Person Address.
        /// </summary>
        /// <param name="generalPersonAddressViewModel">generalPersonAddressViewModel.</param>
        /// <returns>Returns updated generalPersonAddressViewModel</returns>
        GeneralPersonAddressViewModel InsertUpdateGeneralPersonAddress(GeneralPersonAddressViewModel generalPersonAddressViewModel);
    }
}

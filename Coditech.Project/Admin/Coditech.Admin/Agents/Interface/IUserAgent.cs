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
    }
}

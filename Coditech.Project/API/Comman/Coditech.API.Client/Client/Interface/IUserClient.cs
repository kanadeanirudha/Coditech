using Coditech.Common.API.Model;

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
    }
}

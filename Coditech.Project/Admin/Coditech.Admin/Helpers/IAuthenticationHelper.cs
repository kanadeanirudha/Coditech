using Microsoft.AspNetCore.Authorization;

namespace Coditech.Admin.Helpers
{
    public interface IAuthenticationHelper
    {
        //Set authentication cookied for the logged in user
        Task SetAuthCookie(string userName, bool createPersistantCookie);

        //Redirect to login view in case user is not authenticate.
        void RedirectFromLoginPage(string userName, bool createPersistantCookie);

        //Overloaded method for Authorize attribute, user to authenticate & authorize the user for each action.
        void OnAuthorization(AuthorizationHandlerContext filterContext);

        //Method Used to Authenticate the user.
        void AuthenticateUser(AuthorizationHandlerContext filterContext);
    }
}

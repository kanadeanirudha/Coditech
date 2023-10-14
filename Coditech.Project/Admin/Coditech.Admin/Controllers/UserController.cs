using Coditech.Admin.Agents;
using Coditech.Admin.Helpers;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Coditech.Common.Helper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Web;

namespace Coditech.Admin.Controllers
{
    public class UserController : BaseController
    {
        private UserLoginViewModel model = null;
        private readonly IUserAgent _userAgent;
        protected readonly IAuthenticationHelper _authenticationHelper;
        public UserController(IUserAgent userAgent, IAuthenticationHelper authenticationHelper)
        {
            _userAgent = userAgent;
            _authenticationHelper = authenticationHelper;
        }


        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                _userAgent.Logout();

            GetLoginRememberMeCookie();
            return View(new UserLoginViewModel());
        }

        // Posts the UserLoginViewModel to authenticate the user.
        // Logs in if the user is authenticated or it shows error messages accordingly.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public virtual ActionResult Login(UserLoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserLoginViewModel loginviewModel = _userAgent.Login(model);
                if (HelperUtility.IsNotNull(loginviewModel))
                {
                    if (!loginviewModel.HasError)
                    {
                        _authenticationHelper.SetAuthCookie(model.UserName, model.RememberMe);

                        if (model.RememberMe)
                            SaveLoginRememberMeCookie(model.UserName);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("ErrorMessage", "Invalid Email Address or Password");
                    }
                    ModelState.AddModelError("ErrorMessage", loginviewModel.ErrorMessage);
                }
                else
                {
                    ModelState.AddModelError("ErrorMessage", "Invalid Email Address or Password");
                }
            }
            return View(model);
        }

        //Logs off the user from the site.
        [AllowAnonymous]
        [HttpGet]
        public virtual ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
                _userAgent.Logout();

            return RedirectToAction<UserController>(x => x.Login(string.Empty));
        }

        
        public ActionResult GetNotificationCount(int userId)
        {
            int notificationCount = 2; /*userId > 0 ? _userMasterBA.GetNotificationCount(userId) : 0*/;
            return View("~/Views/Shared/_NotificationCount.cshtml", notificationCount);
        }
        #region Private

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(HttpUtility.UrlDecode(returnUrl));

            return RedirectToAction<DashboardController>(x => x.Index());
        }

        private void SaveLoginRememberMeCookie(string userId)
        {
            //Check if the browser support cookies 
            if ((HttpContextHelper.Request.Cookies?.Count > 0))
            {
                CookieHelper.SetCookie(AdminConstants.LoginCookieNameValue, userId, (Convert.ToDouble(CoditechAdminSettings.CookieExpiresValue) * AdminConstants.MinutesInADay), true);

            }
        }
        private void GetLoginRememberMeCookie()
        {
            if (HttpContext.Request.Cookies?.Count > 0)
            {
                if (CookieHelper.IsCookieExists(AdminConstants.LoginCookieNameValue))
                {
                    string loginName = HttpUtility.HtmlEncode(CookieHelper.GetCookieValue<string>(AdminConstants.LoginCookieNameValue));
                    model = new UserLoginViewModel();
                    model.UserName = loginName;
                    model.RememberMe = true;
                }
            }
        }
        #endregion
    }
}
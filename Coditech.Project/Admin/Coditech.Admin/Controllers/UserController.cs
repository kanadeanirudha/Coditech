using Coditech.Admin.Agents;
using Coditech.Admin.Helpers;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper;
using Coditech.Resources;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Web;

namespace Coditech.Admin.Controllers
{
    public class UserController : BaseController
    {
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

        public virtual ActionResult GetNotificationCount(int userId)
        {
            int notificationCount = 2; /*userId > 0 ? _userMasterBA.GetNotificationCount(userId) : 0*/;
            return View("~/Views/Shared/_NotificationCount.cshtml", notificationCount);
        }

        [HttpGet]
        public virtual ActionResult GetGeneralPersonAddressess(long personId)
        {
            GeneralPersonAddressListViewModel model = _userAgent.GetGeneralPersonAddresses(personId);
            return PartialView("~/Views/Shared/GeneralPerson/_GeneralPersonAddress.cshtml", model);
        }

        [HttpPost]
        public virtual ActionResult CreateEditGeneralPersonalAddress(GeneralPersonAddressViewModel generalPersonAddressViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_userAgent.InsertUpdateGeneralPersonAddress(generalPersonAddressViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("GetGeneralPersonAddressess", new { personId = generalPersonAddressViewModel.PersonId });
            }
            return PartialView("~/Views/Shared/GeneralPerson/_GeneralPersonAddress.cshtml", generalPersonAddressViewModel);
        }

        #region Protected
        protected virtual ActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(HttpUtility.UrlDecode(returnUrl));

            return RedirectToAction<DashboardController>(x => x.Index());
        }

        protected virtual void SaveLoginRememberMeCookie(string userId)
        {
            //Check if the browser support cookies 
            if ((HttpContextHelper.Request.Cookies?.Count > 0))
            {
                CookieHelper.SetCookie(AdminConstants.LoginCookieNameValue, userId, (Convert.ToDouble(CoditechAdminSettings.CookieExpiresValue) * AdminConstants.MinutesInADay), true);

            }
        }
        protected virtual void GetLoginRememberMeCookie()
        {
            if (HttpContext.Request.Cookies?.Count > 0)
            {
                if (CookieHelper.IsCookieExists(AdminConstants.LoginCookieNameValue))
                {
                    string loginName = HttpUtility.HtmlEncode(CookieHelper.GetCookieValue<string>(AdminConstants.LoginCookieNameValue));
                    UserLoginViewModel userLoginViewModel = new UserLoginViewModel();
                    userLoginViewModel.UserName = loginName;
                    userLoginViewModel.RememberMe = true;
                }
            }
        }
        #endregion
    }
}
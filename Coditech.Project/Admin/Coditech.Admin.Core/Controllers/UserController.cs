using Coditech.Admin.Agents;
using Coditech.Admin.Helpers;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
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
                        if (!loginviewModel.IsPasswordChange)
                        {
                            return RedirectToAction<UserController>(x => x.ChangePassword());
                        }
                        return RedirectToLocal(returnUrl);
                    }
                    ModelState.AddModelError("ErrorMessage", loginviewModel.ErrorMessage);
                }
                else
                {
                    ModelState.AddModelError("ErrorMessage", "Invalid Username or Password");
                }
            }
            return View(model);
        }

        [HttpGet]
        public virtual ActionResult ChangePassword()
        {
            return View("~/Views/User/ChangePassword.cshtml", new ChangePasswordViewModel());
        }

        [HttpPost]
        public virtual ActionResult ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                changePasswordViewModel = _userAgent.ChangePassword(changePasswordViewModel);
                if (!changePasswordViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.ChangePasswordSuccessMessage));
                    return RedirectToAction<UserController>(x => x.Logout());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(changePasswordViewModel.ErrorMessage));
            return View("~/Views/User/ChangePassword.cshtml", changePasswordViewModel);
        }


        [HttpGet]
        public virtual ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
                _userAgent.Logout();

            SessionHelper.RemoveDataFromSession("returnUrl");
            return RedirectToAction<UserController>(x => x.Login(string.Empty));
        }

        public virtual ActionResult GetNotificationCount(int userId)
        {
            int notificationCount = 2; /*userId > 0 ? _userMasterBA.GetNotificationCount(userId) : 0*/;
            return View("~/Views/Shared/_NotificationCount.cshtml", notificationCount);
        }

        [HttpGet]
        public virtual ActionResult GetGeneralPersonAddressess(long personId, long entityId, string entityType, string controllerName)
        {
            GeneralPersonAddressListViewModel model = _userAgent.GetGeneralPersonAddresses(personId);
            model.EntityId = entityId;
            model.EntityType = entityType;
            model.ControllerName = controllerName;
            return PartialView("~/Views/Shared/GeneralPerson/_GeneralPersonAddress.cshtml", model);
        }


        #region ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult ResetPassword(string token)
        {
            ResetPasswordViewModel resetPasswordViewModel = new ResetPasswordViewModel()
            {
                ResetPasswordToken = token
            };
            return View(resetPasswordViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            ModelState.Remove("UserName");
            if (ModelState.IsValid)
            {
                ResetPasswordViewModel resetPasswordModel = _userAgent.ResetPassword(model);
                if (resetPasswordModel != null && !resetPasswordModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage("Your password reset successfully. Please login with new password."));
                    return RedirectToAction("Login");
                }
                else
                {
                    SetNotificationMessage(GetErrorNotificationMessage(resetPasswordModel.ErrorMessage));
                }
            }
            return View("~/views/user/ResetPassword.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public virtual ActionResult ResetPasswordSendLink(ResetPasswordViewModel resetPasswordViewModel)
        {
            ModelState.Remove("NewPassword");
            ModelState.Remove("ConfirmPassword");
            ModelState.Remove("OTP");
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(resetPasswordViewModel.UserName))
                {
                    ResetPasswordSendLinkViewModel resetPasswordLinkModel = _userAgent.ResetPasswordSendLink(resetPasswordViewModel.UserName);
                    if (resetPasswordLinkModel != null && !resetPasswordLinkModel.HasError)
                    {
                        SetNotificationMessage(GetSuccessNotificationMessage("Your password reset link has been sent to your email address/mobile number."));
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("ErrorMessage", resetPasswordLinkModel.ErrorMessage);
                    }
                }
            }
            return View("~/views/user/ResetPassword.cshtml", resetPasswordViewModel);
        }
        #endregion

        #region Protected
        protected virtual ActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(HttpUtility.UrlDecode(returnUrl));

            return Redirect($"{CoditechAdminSettings.DashboardUrl}?numberOfDaysRecord={CoditechAdminSettings.DefaultDashboardDataDays}");
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
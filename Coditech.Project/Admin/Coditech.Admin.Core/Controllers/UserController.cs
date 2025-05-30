using Coditech.Admin.Agents;
using Coditech.Admin.Helpers;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
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

            var userLoginViewModel = GetLoginRememberMeCookie();
            return View(userLoginViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
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
                            SaveLoginRememberMeCookie(model.UserName, model.Password);
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

        [HttpGet]
        public virtual ActionResult ChangeAccountBalanceSheet(int selectedBalanceId, string returnUrl)
        {
            // Retrieve user data from session
            UserModel userModel = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession);

            if (userModel?.BalanceSheetList?.Count > 0)
            {
                var selectedBalance = userModel.BalanceSheetList.FirstOrDefault(b => b.AccSetupBalanceSheetId == selectedBalanceId);
                if (selectedBalance != null)
                {
                    userModel.SelectedBalanceSheetId = selectedBalance.AccSetupBalanceSheetId;
                    userModel.SelectedBalanceSheet = selectedBalance.AccBalancesheetHeadDesc;
                    // SessionHelper.RemoveDataFromSession(AdminConstants.AccountPrerequisiteSession);
                }

                // Save selected balance ID in session
                SessionHelper.SaveDataInSession(AdminConstants.UserDataSession, userModel);
            }

            return RedirectToLocal(returnUrl);
        }

        #endregion

        #region Protected
        protected virtual ActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(HttpUtility.UrlDecode(returnUrl));

            return Redirect($"{CoditechAdminSettings.DashboardUrl}?numberOfDaysRecord={CoditechAdminSettings.DefaultDashboardDataDays}");
        }

        protected virtual void SaveLoginRememberMeCookie(string userId, string password)
        {
            //Check if the browser support cookies 
            if ((HttpContextHelper.Request.Cookies?.Count > 0))
            {
                CookieHelper.SetCookie(AdminConstants.LoginCookieNameValue, HelperUtility.EncodeBase64($"{userId}|{password}"), (Convert.ToDouble(CoditechAdminSettings.CookieExpiresValue) * AdminConstants.MinutesInADay), true);
            }
        }

        protected virtual UserLoginViewModel GetLoginRememberMeCookie()
        {
            UserLoginViewModel userLoginViewModel = new UserLoginViewModel();

            if (HttpContext.Request.Cookies?.Count > 0)
            {
                if (CookieHelper.IsCookieExists(AdminConstants.LoginCookieNameValue))
                {
                    string loginNamePassword = HelperUtility.DecodeBase64(HttpUtility.HtmlEncode(CookieHelper.GetCookieValue<string>(AdminConstants.LoginCookieNameValue)));
                    if (!string.IsNullOrEmpty(loginNamePassword))
                    {
                        userLoginViewModel.UserName = loginNamePassword.Split("|")[0];
                        userLoginViewModel.Password = loginNamePassword.Split("|")[1];
                        userLoginViewModel.RememberMe = true;
                    }
                }
            }
            return userLoginViewModel;
        }
        #endregion
    }
}
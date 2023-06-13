using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
namespace Coditech.Admin.Agents
{
    public class UserAgent : BaseAgent, IUserAgent
    {
        #region Private Variable
        //private readonly ICoditechLogging _coditechLogging;
        private readonly IUserClient _userClient;
        #endregion
        #region Public Constructor
        public UserAgent(IUserClient userClient)
        {
            //_coditechLogging = GetService<ICoditechLogging>();
            _userClient = GetClient<IUserClient>(userClient);
        }
        #endregion
        #region Public Methods

        // This method is used to logout the user.
        public virtual async Task Logout()
        {
            //await HttpContextHelper.Current.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            SessionHelper.Clear();
        }
        // This method is used to login the user.
        public virtual UserLoginViewModel Login(UserLoginViewModel model)
        {
            //_coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.UserLogin.ToString(), TraceLevel.Info);
            try
            {
                UserModel response = _userClient.Login(null, new UserLoginModel()
                {
                    UserName = model.UserName,
                    Password = model.Password
                });
                //Reset password
                //if (!string.IsNullOrEmpty(accountModel?.User?.PasswordToken))
                //{
                //    UserLoginViewModel loginViewModel = UserViewModelMap.ToLoginViewModel(accountModel);
                //    loginViewModel.IsResetPassword = true;
                //    loginViewModel.HasError = true;
                //    loginViewModel.ErrorMessage = Admin_Resources.InvalidUserNamePassword;
                //    return loginViewModel;
                //}

                //if (!accountModel.IsAdminUser)
                //    return new UserLoginViewModel() { HasError = true, ErrorMessage = Admin_Resources.ErrorAccessDenied };

                //SaveInSession(AdminConstants.UserAccountSessionKey, accountModel.ToViewModel<UserViewModel>());
                //_coditechLogging.LogMessage("Agent method executed.", CoditechLoggingEnum.Components.Admin.ToString(), TraceLevel.Info);

                return new UserLoginViewModel();
            }
            catch (CoditechUnauthorizedException ex)
            {
                //_coditechLogging.LogMessage(ex, string.Empty, TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    //case 1://Error Code For Reset Super Admin Details for the  first time login.
                    //    return ReturnErrorViewModel(model, ex, true);
                    //case 2://Error Code to Reset the Password for the first time login.
                    //    return ReturnErrorViewModel(model, ex, false);
                    //case ErrorCodes.AccountLocked:
                    //    return new UserLoginViewModel() { HasError = true, ErrorMessage = Admin_Resources.ErrorAccountLocked };
                    //case ErrorCodes.TwoAttemptsToAccountLocked:
                    //    return new UserLoginViewModel() { HasError = true, ErrorMessage = Admin_Resources.ErrorTwoAttemptsRemain };
                    //case ErrorCodes.OneAttemptToAccountLocked:
                    //    return new UserLoginViewModel() { HasError = true, ErrorMessage = Admin_Resources.ErrorOneAttemptRemain };
                    //default:
                    //    return new UserLoginViewModel() { HasError = true, ErrorMessage = Admin_Resources.InvalidUserNamePassword };
                }

                return new UserLoginViewModel();
            }
            catch (Exception ex)
            {
                //_coditechLogging.LogMessage(ex, string.Empty, TraceLevel.Error);
                return new UserLoginViewModel();
                //return new UserLoginViewModel() { HasError = true, ErrorMessage = Admin_Resources.InvalidUserNamePassword };
            }
        }
        #endregion
    }
}

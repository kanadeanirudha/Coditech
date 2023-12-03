using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class UserAgent : BaseAgent, IUserAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IUserClient _userClient;
        #endregion

        #region Public Constructor
        public UserAgent(ICoditechLogging coditechLogging, IUserClient userClient)
        {
            _coditechLogging = coditechLogging;
            _userClient = GetClient<IUserClient>(userClient);
        }
        #endregion

        #region Public Methods
        // This method is used to login the user.
        public virtual UserLoginViewModel Login(UserLoginViewModel userLoginViewModel)
        {
            _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.UserLogin.ToString(), TraceLevel.Info);
            try
            {

                UserModel userModel = _userClient.Login(null, new UserLoginModel()
                {
                    UserName = userLoginViewModel.UserName,
                    Password = userLoginViewModel.Password
                });
                if (IsNotNull(userModel))
                {
                    SaveInSession<UserModel>(AdminConstants.UserDataSession, userModel);
                }
                return userLoginViewModel;
            }
            catch (CoditechException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.NotFound:
                        return (UserLoginViewModel)GetViewModelWithErrorMessage(userLoginViewModel, AdminResources.ErrorMessage_ThisaccountdoesnotexistEnteravalidemailaddressorpassword);
                    default:
                        return (UserLoginViewModel)GetViewModelWithErrorMessage(userLoginViewModel, GeneralResources.ErrorMessage_PleaseContactYourAdministrator);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex.Message, CoditechLoggingEnum.Components.UserLogin.ToString());
                return (UserLoginViewModel)GetViewModelWithErrorMessage(userLoginViewModel, GeneralResources.ErrorMessage_PleaseContactYourAdministrator);
            }
        }


        // This method is used to logout the user.
        public virtual async Task Logout()
        {
            await HttpContextHelper.Current.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            SessionHelper.Clear();
        }

        //Get Active Module List .
        public virtual UserModuleViewModel GetActiveModuleList(short userId)
        {
            UserModuleResponse response = _userClient.GetActiveModuleList(userId);
            return response?.UserModuleModel.ToViewModel<UserModuleViewModel>();
        }
        #endregion
    }
}

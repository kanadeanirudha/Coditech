using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
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
                    userLoginViewModel.IsPasswordChange = userModel.IsPasswordChange;
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
                    case ErrorCodes.ContactAdministrator:
                        return (UserLoginViewModel)GetViewModelWithErrorMessage(userLoginViewModel, GeneralResources.ErrorMessage_PleaseContactYourAdministrator);
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

        // This method is used to Change Password the user.
        public virtual ChangePasswordViewModel ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {

            try
            {
                changePasswordViewModel.UserMasterId = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.UserMasterId ?? 0;
                ChangePasswordResponse response = _userClient.ChangePassword(changePasswordViewModel.ToModel<ChangePasswordModel>());
                ChangePasswordModel changePasswordModel = response?.ChangePasswordModel;
                return IsNotNull(changePasswordModel) ? changePasswordModel.ToViewModel<ChangePasswordViewModel>() : new ChangePasswordViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.ChangePassword.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (ChangePasswordViewModel)GetViewModelWithErrorMessage(changePasswordViewModel, ex.ErrorMessage);
                    default:
                        return (ChangePasswordViewModel)GetViewModelWithErrorMessage(changePasswordViewModel, GeneralResources.UpdateErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.ChangePassword.ToString(), TraceLevel.Error);
                return (ChangePasswordViewModel)GetViewModelWithErrorMessage(changePasswordViewModel, GeneralResources.UpdateErrorMessage);
            }
        }


        // This method is used to logout the user.
        public virtual async Task Logout()
        {
            await HttpContextHelper.Current.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            SessionHelper.Clear();
        }

        //Get Member Address Details by personID
        public virtual GeneralPersonAddressListViewModel GetGeneralPersonAddresses(long personId)
        {
            GeneralPersonAddressListResponse response = _userClient.GetGeneralPersonAddresses(personId);
            GeneralPersonAddressListModel generalPersonAddressListModel = new GeneralPersonAddressListModel() { PersonAddressList = response?.GeneralPersonAddressList };
            GeneralPersonAddressListViewModel generalPersonAddressListViewModel = new GeneralPersonAddressListViewModel();
            generalPersonAddressListViewModel.GeneralPersonAddressList = generalPersonAddressListModel.PersonAddressList.ToViewModel<GeneralPersonAddressViewModel>()?.ToList();
            generalPersonAddressListViewModel.FirstName = response.FirstName;
            generalPersonAddressListViewModel.LastName = response.LastName;
            return generalPersonAddressListViewModel;
        }

        //Update Member Address Details
        public virtual GeneralPersonAddressViewModel InsertUpdateGeneralPersonAddress(GeneralPersonAddressViewModel generalPersonAddressViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.UserAddress.ToString(), TraceLevel.Info);
                GeneralPersonAddressResponse response = _userClient.InsertUpdateGeneralPersonAddress(generalPersonAddressViewModel.ToModel<GeneralPersonAddressModel>());
                GeneralPersonAddressModel generalPersonAddressDetailModel = response?.GeneralPersonAddressModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.UserAddress.ToString(), TraceLevel.Info);
                return IsNotNull(generalPersonAddressDetailModel) ? generalPersonAddressDetailModel.ToViewModel<GeneralPersonAddressViewModel>() : (GeneralPersonAddressViewModel)GetViewModelWithErrorMessage(new GeneralPersonAddressViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserAddress.ToString(), TraceLevel.Error);
                return (GeneralPersonAddressViewModel)GetViewModelWithErrorMessage(generalPersonAddressViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        #region ResetPassword
        // This method is used to reset password the user
        public virtual ResetPasswordSendLinkViewModel ResetPasswordSendLink(string userName)
        {
            ResetPasswordSendLinkViewModel resetPasswordSendLinkViewModel = new ResetPasswordSendLinkViewModel();
            try
            {
                ResetPasswordSendLinkResponse resetPasswordSendLinkResponse = _userClient.ResetPasswordSendLink(userName);
                if (resetPasswordSendLinkResponse != null && !resetPasswordSendLinkResponse.HasError)
                {
                    return resetPasswordSendLinkViewModel;
                }
                else
                {
                    return (ResetPasswordSendLinkViewModel)GetViewModelWithErrorMessage(resetPasswordSendLinkViewModel, GeneralResources.ErrorMessage_PleaseContactYourAdministrator);
                }
            }
            catch (CoditechException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.NotFound:
                        return (ResetPasswordSendLinkViewModel)GetViewModelWithErrorMessage(resetPasswordSendLinkViewModel, "Please make sure that the UserName you entered is correct.");
                    case ErrorCodes.ContactAdministrator:
                        return (ResetPasswordSendLinkViewModel)GetViewModelWithErrorMessage(resetPasswordSendLinkViewModel, $"Access Denied. {GeneralResources.ErrorMessage_PleaseContactYourAdministrator}");
                    default:
                        return (ResetPasswordSendLinkViewModel)GetViewModelWithErrorMessage(resetPasswordSendLinkViewModel, $"{GeneralResources.ErrorMessage_PleaseContactYourAdministrator}");
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.ResetPassword.ToString(), TraceLevel.Error);
                return (ResetPasswordSendLinkViewModel)GetViewModelWithErrorMessage(resetPasswordSendLinkViewModel, GeneralResources.ErrorMessage_PleaseContactYourAdministrator);
            }
        }

        public virtual ResetPasswordViewModel ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                ResetPasswordResponse resetPasswordResponse = _userClient.ResetPassword(model.ToModel<ResetPasswordModel>());
                return model;
            }
            catch (CoditechException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.NotFound:
                        return (ResetPasswordViewModel)GetViewModelWithErrorMessage(model, "Please make sure that the UserName you entered is correct.");
                    case ErrorCodes.InValidOTP:
                        return (ResetPasswordViewModel)GetViewModelWithErrorMessage(model, $"Invalid OTP. Please try again.");
                    case ErrorCodes.ExpiredOTP:
                        return (ResetPasswordViewModel)GetViewModelWithErrorMessage(model, $"OTP expired. Please reset password again.");
                    default:
                        return (ResetPasswordViewModel)GetViewModelWithErrorMessage(model, $"{GeneralResources.ErrorMessage_PleaseContactYourAdministrator}");
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.ResetPassword.ToString(), TraceLevel.Error);
                return (ResetPasswordViewModel)GetViewModelWithErrorMessage(model, GeneralResources.ErrorMessage_PleaseContactYourAdministrator);
            }
        }
        #endregion

        #endregion
    }
}

﻿using Coditech.Admin.Utilities;
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
        public virtual UserModuleListViewModel GetActiveModuleList()
        {
            UserModuleListResponse response = _userClient.GetActiveModuleList();
            UserModuleListModel moduleList = new UserModuleListModel { ModuleList = response?.ModuleList };
            UserModuleListViewModel listViewModel = new UserModuleListViewModel();
            listViewModel.ModuleList = moduleList?.ModuleList?.ToViewModel<UserModuleViewModel>().ToList();
            return listViewModel;
        }

        //Get Active Menu List .
        public virtual UserMenuListViewModel GetActiveMenuList(string moduleCode)
        {
            UserMenuListResponse response = _userClient.GetActiveMenuList(moduleCode);
            UserMenuListModel menuList = new UserMenuListModel { MenuList = response?.MenuList };
            UserMenuListViewModel listViewModel = new UserMenuListViewModel();
            listViewModel.MenuList = menuList?.MenuList?.ToViewModel<UserMenuViewModel>().ToList();
            return listViewModel;
        }

        //Get Member Address Details by personID
        public virtual GeneralPersonAddressListViewModel GetGeneralPersonAddresses(long personId)
        {
            GeneralPersonAddressListResponse response = _userClient.GetGeneralPersonAddresses(personId);
            GeneralPersonAddressListModel generalPersonAddressListModel = new GeneralPersonAddressListModel() { PersonAddressList = response?.GeneralPersonAddressList };
            GeneralPersonAddressListViewModel generalPersonAddressListViewModel = new GeneralPersonAddressListViewModel();
            generalPersonAddressListViewModel.GeneralPersonAddressList = generalPersonAddressListModel.PersonAddressList.ToViewModel<GeneralPersonAddressViewModel>()?.ToList();
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
        #endregion
    }
}

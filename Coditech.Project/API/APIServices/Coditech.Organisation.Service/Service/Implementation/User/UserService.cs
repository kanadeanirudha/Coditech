﻿using System.Data;
using System.Diagnostics;
using Coditech.API.Data;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Service
{
    public class UserService : BaseService, IUserService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        protected readonly ICoditechEmail _coditechEmail;
        protected readonly ICoditechSMS _coditechSMS;
        protected readonly ICoditechWhatsApp _coditechWhatsApp;
        private readonly ICoditechRepository<AdminRoleApplicableDetails> _adminRoleApplicableDetailsRepository;
        private readonly ICoditechRepository<AdminRoleMenuDetails> _adminRoleMenuDetailsRepository;
        private readonly ICoditechRepository<AdminRoleCentreRights> _adminRoleCentreRightsRepository;
        private readonly ICoditechRepository<UserMaster> _userMasterRepository;
        private readonly ICoditechRepository<GeneralPerson> _generalPersonRepository;
        private readonly ICoditechRepository<GeneralPersonAddress> _generalPersonAddressRepository;
        private readonly ICoditechRepository<HospitalPatientRegistration> _hospitalPatientRegistrationRepository;
        private readonly ICoditechRepository<OrganisationCentrewiseUserNameRegistration> _organisationCentrewiseUserNameRegistrationRepository;
        private readonly ICoditechRepository<MediaDetail> _mediaDetailRepository;
        private readonly ICoditechRepository<AccSetupBalanceSheet> _accSetupBalanceSheetRepository;
        private readonly ICoditechRepository<UserType> _userTypeRepository;
        private readonly ICoditechRepository<OrganisationCentreMaster> _organisationCentreMasterRepository;
        private readonly ICoditechRepository<OrganisationCentrewiseAccountSetup> _organisationCentrewiseAccountSetupRepository;
        private readonly ICoditechRepository<GeneralCurrencyMaster> _generalCurrencyMasterRepository;
        private readonly ICoditechRepository<GeneralFinancialYear> _generalFinancialYearMasterRepository;
        private readonly ICoditechRepository<EmployeeMaster> _employeeMasterRepository;
        public UserService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider, ICoditechEmail coditechEmail, ICoditechSMS coditechSMS, ICoditechWhatsApp coditechWhatsApp) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _coditechEmail = coditechEmail;
            _coditechSMS = coditechSMS;
            _coditechWhatsApp = coditechWhatsApp;
            _adminRoleApplicableDetailsRepository = new CoditechRepository<AdminRoleApplicableDetails>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleCentreRightsRepository = new CoditechRepository<AdminRoleCentreRights>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleMenuDetailsRepository = new CoditechRepository<AdminRoleMenuDetails>(_serviceProvider.GetService<Coditech_Entities>());
            _userMasterRepository = new CoditechRepository<UserMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _generalPersonRepository = new CoditechRepository<GeneralPerson>(_serviceProvider.GetService<Coditech_Entities>());
            _generalPersonAddressRepository = new CoditechRepository<GeneralPersonAddress>(_serviceProvider.GetService<Coditech_Entities>());
            _hospitalPatientRegistrationRepository = new CoditechRepository<HospitalPatientRegistration>(_serviceProvider.GetService<Coditech_Entities>());
            _organisationCentrewiseUserNameRegistrationRepository = new CoditechRepository<OrganisationCentrewiseUserNameRegistration>(_serviceProvider.GetService<Coditech_Entities>());
            _mediaDetailRepository = new CoditechRepository<MediaDetail>(_serviceProvider.GetService<Coditech_Entities>());
            _accSetupBalanceSheetRepository = new CoditechRepository<AccSetupBalanceSheet>(_serviceProvider.GetService<Coditech_Entities>());
            _userTypeRepository = new CoditechRepository<UserType>(_serviceProvider.GetService<Coditech_Entities>());
            _organisationCentreMasterRepository = new CoditechRepository<OrganisationCentreMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _organisationCentrewiseAccountSetupRepository = new CoditechRepository<OrganisationCentrewiseAccountSetup>(_serviceProvider.GetService<Coditech_Entities>());
            _generalCurrencyMasterRepository = new CoditechRepository<GeneralCurrencyMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _generalFinancialYearMasterRepository = new CoditechRepository<GeneralFinancialYear>(_serviceProvider.GetService<Coditech_Entities>());
            _employeeMasterRepository = new CoditechRepository<EmployeeMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        #region Public
        public virtual UserModel Login(UserLoginModel userLoginModel)
        {
            if (IsNull(userLoginModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            userLoginModel.Password = MD5Hash(userLoginModel.Password);
            UserMaster userMasterData = _userMasterRepository.Table.FirstOrDefault(x => x.UserName.ToLower() == userLoginModel.UserName.ToLower() && x.Password == userLoginModel.Password
                                                                             && (x.UserType == UserTypeEnum.Admin.ToString() || x.UserType == UserTypeEnum.Employee.ToString()));
            if (IsNull(userMasterData))
                throw new CoditechException(ErrorCodes.NotFound, null);
            else if (!userMasterData.IsActive)
                throw new CoditechException(ErrorCodes.ContactAdministrator, null);

            UserModel userModel = BindUserDetail(userMasterData);
            return userModel;
        }

        public virtual UserModel GetUserDetailByUserName(string userName)
        {
            if (IsNull(userName))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            UserMaster userMasterData = _userMasterRepository.Table.FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower() && (x.UserType == UserTypeEnum.Admin.ToString() || x.UserType == UserTypeEnum.Employee.ToString()));

            if (IsNull(userMasterData))
                throw new CoditechException(ErrorCodes.NotFound, null);
            else if (!userMasterData.IsActive)
                throw new CoditechException(ErrorCodes.ContactAdministrator, null);

            UserModel userModel = BindUserDetail(userMasterData);
            return userModel;
        }

        public virtual bool AcceptTermsAndConditions(string userType, long entityId)
        {
            if (string.IsNullOrEmpty(userType))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "userType"));

            if (entityId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EntityId"));

            UserMaster userMaster = _userMasterRepository.Table.Where(x => x.UserType == userType && x.EntityId == entityId)?.FirstOrDefault();
            if (userMaster == null)
                return false;

            userMaster.IsAcceptedTermsAndConditions = true;

            return _userMasterRepository.Update(userMaster);
        }
        #region ResetPassowrd
        public virtual ResetPasswordModel ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (IsNull(resetPasswordModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            string userName = DecodeBase64(resetPasswordModel.ResetPasswordToken);

            UserMaster userMasterData = _userMasterRepository.Table.Where(x => x.UserName == userName)?.FirstOrDefault();

            if (IsNull(userMasterData))
                throw new CoditechException(ErrorCodes.NotFound, "Please make sure that the Username you entered is correct.");


            if (userMasterData.ResetPasswordToken != resetPasswordModel.OTP)
                throw new CoditechException(ErrorCodes.InValidOTP, "Invalid OTP");

            if (userMasterData.ResetPasswordTokenExpiredDate <= DateTime.Now)
                throw new CoditechException(ErrorCodes.ExpiredOTP, "OTP Expried");

            userMasterData.Password = MD5Hash(resetPasswordModel.NewPassword);
            userMasterData.ResetPasswordToken = null;
            userMasterData.ResetPasswordTokenExpiredDate = null;
            _userMasterRepository.Update(userMasterData);
            return new ResetPasswordModel();
        }

        //ResetPasswordSendLink
        public virtual ResetPasswordSendLinkModel ResetPasswordSendLink(string userName, bool isMobileRequest)
        {
            if (IsNull(userName))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            UserMaster userMasterData = _userMasterRepository.Table.Where(x => x.UserName == userName)?.FirstOrDefault();

            if (IsNull(userMasterData))
                throw new CoditechException(ErrorCodes.NotFound, "Please make sure that the Username you entered is correct.");

            if (!userMasterData.IsActive)
                throw new CoditechException(ErrorCodes.ContactAdministrator, "Access Denied. Please contact a Site Administrator.");

            if (!isMobileRequest)
            {
                if (!(userMasterData.UserType == UserTypeEnum.Employee.ToString() || userMasterData.UserType == UserTypeEnum.Admin.ToString()))
                    throw new CoditechException(ErrorCodes.ContactAdministrator, "Access Denied. Please contact a Site Administrator.");
            }
            string resetPassToken = HelperUtility.GenerateNumericCode();
            userMasterData.ResetPasswordToken = resetPassToken;
            userMasterData.ResetPasswordTokenExpiredDate = DateTime.Now.AddMinutes(Convert.ToDouble(ApiSettings.ResetPasswordExpriedTimeInMinute));
            _userMasterRepository.Update(userMasterData);

            List<CoditechApplicationSetting> coditechApplicationSettingList = CoditechApplicationSetting();
            string url = $"{coditechApplicationSettingList.First(x => x.ApplicationCode == "CoditechAdminRootUri")?.ApplicationValue1}/User/ResetPassword?token={EncodeBase64(userMasterData.UserName)}";
            try
            {
                string emailTemplateCodeEnum = $"{(isMobileRequest ? APIConstant.Mobile : "")}{EmailTemplateCodeEnum.ResetPasswordLink.ToString()}";
                GeneralPersonModel generalPersonModel = GetGeneralPersonDetailsByEntityType(userMasterData.EntityId, userMasterData.UserType);
                GeneralEmailTemplateModel emailTemplateModel = GetEmailTemplateByCode(generalPersonModel?.SelectedCentreCode, emailTemplateCodeEnum);
                if (IsNotNull(emailTemplateModel) && !string.IsNullOrEmpty(emailTemplateModel?.EmailTemplateCode) && !string.IsNullOrEmpty(generalPersonModel?.EmailId))
                {
                    string messageText = ReplaceResetPassworkLink(generalPersonModel, emailTemplateModel.EmailTemplate, url, resetPassToken);
                    _coditechEmail.SendEmail(generalPersonModel.SelectedCentreCode, generalPersonModel.EmailId, "", emailTemplateModel.Subject, messageText, true);
                }
                GeneralEmailTemplateModel smsTemplateModel = GetSMSTemplateByCode(generalPersonModel?.SelectedCentreCode, emailTemplateCodeEnum);
                if (IsNotNull(smsTemplateModel) && !string.IsNullOrEmpty(smsTemplateModel?.EmailTemplateCode) && !string.IsNullOrEmpty(generalPersonModel?.MobileNumber))
                {
                    string messageText = ReplaceResetPassworkLink(generalPersonModel, smsTemplateModel.EmailTemplate, url, resetPassToken);
                    _coditechSMS.SendSMS(generalPersonModel.SelectedCentreCode, messageText, $"{generalPersonModel.CallingCode}{generalPersonModel?.MobileNumber}");
                }
                GeneralEmailTemplateModel whatsAppTemplateModel = GetWhatsAppTemplateByCode(generalPersonModel?.SelectedCentreCode, emailTemplateCodeEnum);
                if (IsNotNull(whatsAppTemplateModel) && !string.IsNullOrEmpty(whatsAppTemplateModel?.EmailTemplateCode) && !string.IsNullOrEmpty(generalPersonModel?.MobileNumber))
                {
                    string messageText = ReplaceResetPassworkLink(generalPersonModel, whatsAppTemplateModel.EmailTemplate, url, resetPassToken);
                    _coditechWhatsApp.SendWhatsAppMessage(generalPersonModel.SelectedCentreCode, messageText, $"{generalPersonModel.CallingCode}{generalPersonModel?.MobileNumber}");
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
            }
            return new ResetPasswordSendLinkModel();
        }
        #endregion

        //Change Password.
        public virtual ChangePasswordModel ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (IsNull(changePasswordModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (changePasswordModel.EntityId <= 0 || string.IsNullOrEmpty(changePasswordModel.UserType))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EntityId"));

            UserMaster userMasterData = _userMasterRepository.Table.FirstOrDefault(x => x.EntityId == changePasswordModel.EntityId
                                                                                        && x.UserType == changePasswordModel.UserType);
            if (IsNotNull(userMasterData) && userMasterData.Password == MD5Hash(changePasswordModel.CurrentPassword))
            {
                userMasterData.Password = MD5Hash(changePasswordModel.NewPassword);
                userMasterData.IsPasswordChange = true;
                userMasterData.ResetPasswordToken = null;
                _userMasterRepository.Update(userMasterData);
            }
            else
            {
                changePasswordModel.HasError = true;
                changePasswordModel.ErrorCode = ErrorCodes.AlreadyExist;
                changePasswordModel.ErrorMessage = "Current Password DoesNot Match.";
            }
            return changePasswordModel;
        }

        public virtual List<UserModuleModel> GetActiveModuleList()
        {
            List<UserModuleModel> moduleList = new List<UserModuleModel>();
            foreach (UserModuleMaster item in base.GetAllActiveModuleList())
            {
                moduleList.Add(new UserModuleModel()
                {
                    UserModuleMasterId = item.UserModuleMasterId,
                    ModuleCode = item.ModuleCode,
                    ModuleName = item.ModuleName,
                });
            }
            return moduleList;
        }

        public virtual List<UserMainMenuModel> GetActiveMenuList(string moduleCodel)
        {
            List<UserMainMenuModel> menuList = new List<UserMainMenuModel>();
            foreach (UserMainMenuMaster item in base.GetAllActiveMenuList(moduleCodel))
            {
                menuList.Add(new UserMainMenuModel()
                {
                    UserMainMenuMasterId = item.UserMainMenuMasterId,
                    ModuleCode = item.ModuleCode,
                    MenuCode = item.MenuCode,
                    MenuName = item.MenuName,
                    ControllerName = item.ControllerName,
                    ParentMenuCode = item.ParentMenuCode,
                    MenuDisplaySeqNo = item.MenuDisplaySeqNo,
                });
            }
            return menuList;
        }

        #region General Person
        public virtual GeneralPersonModel InsertPersonInformation(GeneralPersonModel generalPersonModel, string customData = null)
        {
            string errorMessage = string.Empty;
            if (!ValidatedGeneralPersonData(generalPersonModel, out errorMessage))
            {
                generalPersonModel.HasError = true;
                generalPersonModel.ErrorMessage = string.IsNullOrEmpty(errorMessage) ? GeneralResources.ErrorFailedToCreate : errorMessage;
                _coditechLogging.LogMessage(errorMessage, CoditechLoggingEnum.Components.UserRegistration.ToString(), TraceLevel.Error);
                return generalPersonModel;
            }
            if (IsNull(generalPersonModel.DateOfBirth) && generalPersonModel.Age > 0)
            {
                generalPersonModel.DateOfBirth = new DateTime(CalculateBirthYear(generalPersonModel.Age), 1, 1);
            }
            GeneralPerson personData = InsertGeneralPersonData(generalPersonModel);
            if (personData?.PersonId > 0)
            {
                generalPersonModel.PersonId = personData.PersonId;
                List<GeneralSystemGlobleSettingModel> settingMasterList = GetSystemGlobleSettingList();
                if (string.IsNullOrEmpty(generalPersonModel.Password))
                {
                    string password = settingMasterList?.FirstOrDefault(x => x.FeatureName.Equals(GeneralSystemGlobleSettingEnum.DefaultPassword.ToString(), StringComparison.InvariantCultureIgnoreCase)).FeatureValue;
                    generalPersonModel.Password = password;
                }
                generalPersonModel.CentreName = GetOrganisationCentreNameByCentreCode(generalPersonModel.SelectedCentreCode);

                InsertPersonDetails(generalPersonModel, settingMasterList, customData);
            }
            else
            {
                generalPersonModel.HasError = true;
                generalPersonModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }

            if (generalPersonModel.PhotoMediaId > 0)
            {
                var mediaDetail = _mediaDetailRepository.Table.Where(x => x.MediaId == generalPersonModel.PhotoMediaId).FirstOrDefault();
                if (mediaDetail != null)
                {
                    generalPersonModel.PhotoMediaPath = $"{GetMediaUrl}{mediaDetail.Path}";
                    generalPersonModel.PhotoMediaFileName = mediaDetail.FileName;
                }
            }
            return generalPersonModel;
        }

        public virtual GeneralPersonModel GetPersonInformation(long personId)
        {
            if (personId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PersonId"));

            //Get the General Person Details based on id.
            GeneralPerson personData = _generalPersonRepository.Table.FirstOrDefault(x => x.PersonId == personId);
            GeneralPersonModel generalPersonModel = personData.FromEntityToModel<GeneralPersonModel>();

            if (IsNotNull(generalPersonModel?.DateOfBirth))
            {
                generalPersonModel.Age = CalculateAge(Convert.ToDateTime(generalPersonModel.DateOfBirth));
            }
            if (generalPersonModel.PhotoMediaId > 0)
            {
                var mediaDetail = _mediaDetailRepository.Table.Where(x => x.MediaId == generalPersonModel.PhotoMediaId)?.FirstOrDefault();
                if (mediaDetail != null)
                {
                    generalPersonModel.PhotoMediaPath = $"{GetMediaUrl()}{mediaDetail.Path}";
                    generalPersonModel.PhotoMediaFileName = mediaDetail.FileName;
                }
            }
            return generalPersonModel;
        }

        public virtual bool UpdatePersonInformation(GeneralPersonModel generalPersonModel)
        {

            if (IsNull(generalPersonModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalPersonModel.PersonId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PersonId"));

            if (generalPersonModel.EntityId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EntityId"));

            GeneralPerson generalPerson = generalPersonModel.FromModelToEntity<GeneralPerson>();

            //Update General Person
            bool isPersonUpdated = _generalPersonRepository.Update(generalPerson);
            if (isPersonUpdated)
            {
                UserMaster userMaster = new UserMaster()
                {
                    EntityId = generalPersonModel.EntityId,
                    UserType = generalPersonModel.UserType,
                    FirstName = generalPersonModel.FirstName,
                    MiddleName = generalPersonModel.MiddleName,
                    LastName = generalPersonModel.LastName,
                    EmailId = generalPersonModel.EmailId,
                    IsActive = generalPersonModel.IsActive,
                };
                UpdateUserMasterDetails(userMaster);
                UpdateIsActiveFlagForUserType(generalPersonModel);
            }
            else
            {
                generalPersonModel.HasError = true;
                generalPersonModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isPersonUpdated;
        }
        #endregion

        #region General Person Addresses
        public virtual GeneralPersonAddressListModel GetGeneralPersonAddresses(long personId)
        {
            if (personId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PersonId"));

            GeneralPersonAddressListModel generalPersonAddressListModel = new GeneralPersonAddressListModel();
            //Get the General Person Address Details based on id.
            List<GeneralPersonAddress> personAddresses = _generalPersonAddressRepository.Table.Where(x => x.PersonId == personId)?.ToList();
            List<GeneralPersonAddressModel> personAddressDetailList = new List<GeneralPersonAddressModel>();
            if (personAddresses?.Count > 0)
            {
                if (personAddresses.Any(x => x.AddressTypeEnum == AddressTypeEnum.PermanentAddress.ToString()))
                {
                    GeneralPersonAddressModel generalPersonAddressModel = personAddresses.FirstOrDefault(x => x.AddressTypeEnum == AddressTypeEnum.PermanentAddress.ToString()).FromEntityToModel<GeneralPersonAddressModel>();
                    personAddressDetailList.Add(generalPersonAddressModel);
                }
                else
                {
                    personAddressDetailList.Add(new GeneralPersonAddressModel() { AddressTypeEnum = AddressTypeEnum.PermanentAddress.ToString() });
                }

                if (personAddresses.Any(x => x.AddressTypeEnum == AddressTypeEnum.CorrespondanceAddress.ToString()))
                {
                    GeneralPersonAddressModel generalPersonAddressModel = personAddresses.FirstOrDefault(x => x.AddressTypeEnum == AddressTypeEnum.CorrespondanceAddress.ToString()).FromEntityToModel<GeneralPersonAddressModel>();
                    personAddressDetailList.Add(generalPersonAddressModel);
                }
                else
                {
                    personAddressDetailList.Add(new GeneralPersonAddressModel() { AddressTypeEnum = AddressTypeEnum.CorrespondanceAddress.ToString() });
                }

                if (personAddresses.Any(x => x.AddressTypeEnum == AddressTypeEnum.BusinessAddress.ToString()))
                {
                    GeneralPersonAddressModel generalPersonAddressModel = personAddresses.FirstOrDefault(x => x.AddressTypeEnum == AddressTypeEnum.BusinessAddress.ToString()).FromEntityToModel<GeneralPersonAddressModel>();
                    personAddressDetailList.Add(generalPersonAddressModel);
                }
                else
                {
                    personAddressDetailList.Add(new GeneralPersonAddressModel() { AddressTypeEnum = AddressTypeEnum.BusinessAddress.ToString() });
                }
            }
            else
            {
                personAddressDetailList.Add(new GeneralPersonAddressModel() { AddressTypeEnum = AddressTypeEnum.PermanentAddress.ToString() });
                personAddressDetailList.Add(new GeneralPersonAddressModel() { AddressTypeEnum = AddressTypeEnum.CorrespondanceAddress.ToString() });
                personAddressDetailList.Add(new GeneralPersonAddressModel() { AddressTypeEnum = AddressTypeEnum.BusinessAddress.ToString() });
            }
            generalPersonAddressListModel.PersonAddressList = personAddressDetailList;

            GeneralPersonModel generalPersonModel = GetGeneralPersonDetails(personId);
            if (IsNotNull(generalPersonAddressListModel))
            {
                generalPersonAddressListModel.FirstName = generalPersonModel.FirstName;
                generalPersonAddressListModel.LastName = generalPersonModel.LastName;
            }
            return generalPersonAddressListModel;
        }

        public virtual GeneralPersonAddressModel InsertUpdateGeneralPersonAddress(GeneralPersonAddressModel generalPersonAddressModel)
        {

            if (IsNull(generalPersonAddressModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalPersonAddressModel.PersonId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PersonId"));

            GeneralPersonAddress generalPersonAddress = generalPersonAddressModel.FromModelToEntity<GeneralPersonAddress>();
            if (_generalPersonAddressRepository.Table.Any(x => x.PersonId == generalPersonAddressModel.PersonId && x.AddressTypeEnum == generalPersonAddressModel.AddressTypeEnum))
            {
                if (!_generalPersonAddressRepository.Update(generalPersonAddress))
                {
                    generalPersonAddressModel.HasError = true;
                    generalPersonAddressModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
                }
                else if (generalPersonAddressModel.IsCorrespondanceAddressSameAsPermanentAddress && generalPersonAddressModel.AddressTypeEnum == AddressTypeEnum.PermanentAddress.ToString())
                {
                    long? generalPersonAddressId = _generalPersonAddressRepository.Table.Where(x => x.PersonId == generalPersonAddressModel.PersonId && x.AddressTypeEnum == AddressTypeEnum.CorrespondanceAddress.ToString())?.FirstOrDefault()?.GeneralPersonAddressId;
                    generalPersonAddress.IsCorrespondanceAddressSameAsPermanentAddress = false;
                    generalPersonAddress.AddressTypeEnum = AddressTypeEnum.CorrespondanceAddress.ToString();
                    if (generalPersonAddressId > 0)
                    {
                        generalPersonAddress.GeneralPersonAddressId = Convert.ToInt64(generalPersonAddressId);
                        _generalPersonAddressRepository.Update(generalPersonAddress);
                    }
                    else
                    {
                        generalPersonAddress.GeneralPersonAddressId = 0;
                        generalPersonAddress = _generalPersonAddressRepository.Insert(generalPersonAddress);
                    }
                }
            }
            else
            {
                generalPersonAddress = _generalPersonAddressRepository.Insert(generalPersonAddress);
                if (generalPersonAddress?.GeneralPersonAddressId == 0)
                {
                    generalPersonAddressModel.HasError = true;
                    generalPersonAddressModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
                }
                else if (generalPersonAddressModel.IsCorrespondanceAddressSameAsPermanentAddress && generalPersonAddressModel.AddressTypeEnum == AddressTypeEnum.PermanentAddress.ToString())
                {
                    generalPersonAddress.GeneralPersonAddressId = 0;
                    generalPersonAddress.IsCorrespondanceAddressSameAsPermanentAddress = false;
                    generalPersonAddress.AddressTypeEnum = AddressTypeEnum.CorrespondanceAddress.ToString();
                    generalPersonAddress = _generalPersonAddressRepository.Insert(generalPersonAddress);
                }
            }
            return generalPersonAddressModel;
        }

        //GetUserTypeList
        public virtual List<UserTypeModel> GetUserTypeList()
        {
            List<UserTypeModel> typeList = new List<UserTypeModel>();
            var userTypeList = _userTypeRepository.Table.Where(x => x.IsActive).ToList();
            foreach (UserType item in userTypeList)
            {
                //typeList.Add(item.FromEntityToModel<UserTypeModel>());
                typeList.Add(new UserTypeModel()
                {
                    UserTypeId = item.UserTypeId,
                    UserTypeCode = item.UserTypeCode,
                    UserDescription = item.UserDescription,
                    IsCommon = item.IsCommon,
                    IsLoginRequired = item.IsLoginRequired,
                    IsActive = item.IsActive
                });
            }
            return typeList;
        }
        #endregion

        #endregion

        #region Protected Method


        protected virtual UserModel BindUserDetail(UserMaster userMasterData)
        {
            UserModel userModel = userMasterData?.FromEntityToModel<UserModel>();


            userModel.IsAdminUser = IsAdminUser(userModel.UserType);
            //Bind Role
            BindRoleTypes(userModel);

            List<UserModuleMaster> userAllModuleList = GetAllActiveModuleList();
            List<UserMainMenuMaster> userAllMenuList = GetAllActiveMenuList();
            List<AdminRoleMenuDetails> userRoleMenuList = new List<AdminRoleMenuDetails>();
            List<AdminRoleMediaFolderAction> userRoleMediaFolderActionList = new CoditechRepository<AdminRoleMediaFolderAction>(_serviceProvider.GetService<Coditech_Entities>()).Table?.ToList();
            if (!userModel.IsAdminUser)
            {
                userRoleMenuList = _adminRoleMenuDetailsRepository.Table.Where(x => x.IsActive && x.AdminRoleMasterId == userModel.SelectedAdminRoleMasterId)?.ToList();
                if (userRoleMenuList?.Count == 0)
                {
                    throw new CoditechException(ErrorCodes.ContactAdministrator, null);
                }
                else
                {
                    userAllModuleList = userAllModuleList.Where(x => x.ModuleCode != "CODITECHTOOLKIT")?.ToList();
                    //Bind Menu And Modules For Admin User
                    BindMenuAndModulesForNonAdminUser(userModel, userAllModuleList, userAllMenuList, userRoleMenuList);
                    BindRoleMediaFolderAction(userModel, userRoleMediaFolderActionList);
                    //Bind accessible Centre
                    List<string> centreCodeList = _adminRoleCentreRightsRepository.Table.Where(x => x.AdminRoleMasterId == userModel.SelectedAdminRoleMasterId && x.IsActive)?.Select(y => y.CentreCode)?.ToList();
                    List<UserAccessibleCentreModel> allCentreList = OrganisationCentreList();
                    foreach (string centreCode in centreCodeList)
                    {
                        userModel.AccessibleCentreList.Add(allCentreList.First(x => x.CentreCode == centreCode));
                    }
                    //Bind Balance Sheet
                    BindAccountBalanceSheetIdByCentreCode(userModel);
                }
            }
            else
            {
                //Bind Menu And Modules For Non Admin User
                BindMenuAndModulesForAdminUser(userModel, userAllModuleList, userAllMenuList);
                BindRoleMediaFolderAction(userModel, userRoleMediaFolderActionList);
                userModel.AccessibleCentreList = OrganisationCentreList();
            }
            userModel.SelectedCentreCode = userModel?.AccessibleCentreList?.FirstOrDefault()?.CentreCode;
            if (!string.IsNullOrEmpty(userModel.SelectedCentreCode))
            {
                OrganisationCentreMaster organisationCentreMasterData = _organisationCentreMasterRepository.Table.FirstOrDefault(x => x.CentreCode == userModel.SelectedCentreCode);

                if (IsNotNull(organisationCentreMasterData))
                {
                    OrganisationCentreModel organisationCentreModel = organisationCentreMasterData.FromEntityToModel<OrganisationCentreModel>();

                    if (organisationCentreModel.LogoMediaId > 0)
                    {
                        MediaDetail mediaDetail = _mediaDetailRepository.Table.Where(x => x.MediaId == organisationCentreModel.LogoMediaId).FirstOrDefault();
                        if (mediaDetail != null)
                        {
                            organisationCentreModel.LogoMediaPath = $"{GetMediaUrl()}{mediaDetail.Path}";
                        }
                    }

                    if (organisationCentreModel.LogoSmallMediaId > 0)
                    {
                        MediaDetail mediaDetail = _mediaDetailRepository.Table.Where(x => x.MediaId == organisationCentreModel.LogoSmallMediaId).FirstOrDefault();
                        if (mediaDetail != null)
                        {
                            organisationCentreModel.LogoSmallMediaPath = $"{GetMediaUrl()}{mediaDetail.Path}";
                        }
                    }
                    userModel.LogoMediaPath = organisationCentreModel.LogoMediaPath;
                    userModel.LogoSmallMediaPath = organisationCentreModel.LogoSmallMediaPath;
                }


            }
            userModel.GeneralEnumaratorList = BindEnumarator();
            userModel.GeneralSystemGlobleSettingList = GetSystemGlobleSettingList();
            return userModel;
        }

        //Bind Role Types
        protected virtual void BindRoleTypes(UserModel userModel)
        {
            if (!userModel.IsAdminUser)
            {
                List<AdminRoleApplicableDetails> roleList = _adminRoleApplicableDetailsRepository.Table.Where(x => x.EmployeeId == userModel.EntityId && x.IsActive)?.ToList();
                if (roleList?.Count() == 0)
                {
                    throw new CoditechException(ErrorCodes.ContactAdministrator, null);
                }
                else
                {
                    userModel.SelectedAdminRoleMasterId = roleList.FirstOrDefault(x => x.RoleType == APIConstant.Regular).AdminRoleMasterId;
                    //userModel.SelectedRoleCode = roleList.FirstOrDefault(x => x.RoleType == APIConstant.Regular). AdminRoleCode;
                    foreach (AdminRoleApplicableDetails item in roleList)
                    {
                        userModel.RoleList.Add(new AdminRoleDetailsModel()
                        {
                            AdminRoleMasterId = item.AdminRoleMasterId,
                            RoleType = item.RoleType,
                        });
                    }
                }
            }
        }
        //Bind Menu And Modules For Admin User
        protected virtual void BindMenuAndModulesForAdminUser(UserModel userModel, List<UserModuleMaster> userAllModuleList, List<UserMainMenuMaster> userAllMenuList)
        {
            foreach (UserModuleMaster item in userAllModuleList)
            {
                UserMainMenuMaster userMainMenuMaster = userAllMenuList?.FirstOrDefault(x => x.ControllerName != null && x.ModuleCode == item.ModuleCode);
                userModel.ModuleList.Add(new UserModuleModel()
                {
                    UserModuleMasterId = item.UserModuleMasterId,
                    ModuleCode = item.ModuleCode,
                    ModuleName = item.ModuleName.ToUpper(),
                    ModuleSeqNumber = item.ModuleSeqNumber,
                    ModuleTooltip = item.ModuleTooltip,
                    ModuleIconName = item.ModuleIconName,
                    ModuleColorClass = item.ModuleColorClass,
                    DefaultMenuLink = $"{userMainMenuMaster?.ControllerName?.ToLower()}/{userMainMenuMaster?.ActionName?.ToLower()}",
                });
            }

            foreach (UserMainMenuMaster item in userAllMenuList)
            {
                userModel.MenuList.Add(new UserMainMenuModel()
                {
                    UserMainMenuMasterId = item.UserMainMenuMasterId,
                    ModuleCode = item.ModuleCode,
                    MenuCode = item.MenuCode,
                    MenuName = item.MenuName,
                    ParentMenuCode = item.ParentMenuCode,
                    MenuDisplaySeqNo = item.MenuDisplaySeqNo,
                    ControllerName = item.ControllerName?.ToLower(),
                    ActionName = item.ActionName?.ToLower(),
                    MenuLink = $"{item.ControllerName?.ToLower()}/{item.ActionName?.ToLower()}",
                    MenuToolTip = item.MenuToolTip,
                    MenuIconName = item.MenuIconName
                });
            }
        }

        protected virtual void BindRoleMediaFolderAction(UserModel userModel, List<AdminRoleMediaFolderAction> userRoleMediaFolderActionList)
        {
            userModel.AdminRoleMediaFolderActionList = new List<AdminRoleMediaFolderActionModel>();

            if (userModel.IsAdminUser)
            {
                userModel.AdminRoleMediaFolderActionList.Add(new AdminRoleMediaFolderActionModel()
                {
                    SelectedMediaActions = Enum.GetValues(typeof(MediaFolderActionEnum))
                        .Cast<MediaFolderActionEnum>()
                        .Select(e => e.ToString())
                        .ToList()
                });
            }
            else
            {
                foreach (AdminRoleMediaFolderAction userRoleMediaFolderAction in userRoleMediaFolderActionList)
                {
                    userModel.AdminRoleMediaFolderActionList.Add(new AdminRoleMediaFolderActionModel()
                    {
                        AdminRoleMediaFolderActionId = userRoleMediaFolderAction.AdminRoleMediaFolderActionId,
                        AdminRoleMasterId = userRoleMediaFolderAction.AdminRoleMasterId,
                        SelectedMediaActions = userRoleMediaFolderAction.MediaAction?.Split(",").ToList()
                    });
                }
            }

        }

        //Bind Menu And Modules For Non Admin User
        protected virtual void BindMenuAndModulesForNonAdminUser(UserModel userModel, List<UserModuleMaster> userAllModuleList, List<UserMainMenuMaster> userAllMenuList, List<AdminRoleMenuDetails> userRoleMenuList)
        {
            //Bind Menu & Module for non admin user
            foreach (AdminRoleMenuDetails item in userRoleMenuList)
            {
                UserMainMenuMaster userMenuModel = userAllMenuList.FirstOrDefault(x => x.MenuCode == item.MenuCode);
                if (IsNotNull(userMenuModel))
                {
                    userModel.MenuList.Add(new UserMainMenuModel()
                    {
                        UserMainMenuMasterId = userMenuModel.UserMainMenuMasterId,
                        ModuleCode = userMenuModel.ModuleCode,
                        MenuCode = userMenuModel.MenuCode,
                        MenuName = userMenuModel.MenuName,
                        ParentMenuCode = userMenuModel.ParentMenuCode,
                        MenuDisplaySeqNo = userMenuModel.MenuDisplaySeqNo,
                        ControllerName = userMenuModel.ControllerName?.ToLower(),
                        ActionName = userMenuModel.ActionName?.ToLower(),
                        MenuLink = $"{userMenuModel.ControllerName?.ToLower()}/{userMenuModel.ActionName?.ToLower()}",
                        MenuToolTip = userMenuModel.MenuToolTip,
                        MenuIconName = userMenuModel.MenuIconName
                    });

                    if (!userModel.ModuleList.Any(x => x.ModuleCode == userMenuModel.ModuleCode) && !string.IsNullOrEmpty(userMenuModel.ControllerName))
                    {
                        UserModuleModel userModuleModel = userAllModuleList.FirstOrDefault(x => x.ModuleCode == userMenuModel.ModuleCode).FromEntityToModel<UserModuleModel>();
                        if (IsNotNull(userModuleModel))
                        {
                            userModuleModel.DefaultMenuLink = $"{userMenuModel?.ControllerName?.ToLower()}/{userMenuModel?.ActionName?.ToLower()}";
                            userModel.ModuleList.Add(userModuleModel);
                        }
                    }
                }
            }
        }
        protected virtual void BindAccountBalanceSheetIdByCentreCode(UserModel userModel)
        {
            List<string> centreCodeList = userModel.AccessibleCentreList.Select(x => x.CentreCode).ToList();
            List<AccSetupBalanceSheet> balanceSheets = _accSetupBalanceSheetRepository.Table.Where(x => centreCodeList.Contains(x.CentreCode) && x.IsActive).ToList();
            userModel.BalanceSheetList = balanceSheets?.Select(x => new UserBalanceSheetModel
            {
                AccSetupBalanceSheetId = x.AccSetupBalanceSheetId,
                AccBalancesheetHeadDesc = x.AccBalancesheetHeadDesc,
                CentreCode = x.CentreCode,
                AccBalancesheetCode = x.AccBalancesheetCode,

            })?.ToList();

            if (userModel.BalanceSheetList?.Count > 0)
            {
                var firstBalanceSheet = userModel.BalanceSheetList.FirstOrDefault();
                if (firstBalanceSheet != null)
                {
                    userModel.SelectedBalanceSheetId = firstBalanceSheet.AccSetupBalanceSheetId;
                    userModel.SelectedBalanceSheet = firstBalanceSheet.AccBalancesheetHeadDesc;
                }
            }
        }

        protected virtual void UpdateUserMasterDetails(UserMaster model)
        {
            CoditechRepository<UserMaster> _userMasterRepository = new CoditechRepository<UserMaster>(_serviceProvider.GetService<Coditech_Entities>());

            UserMaster userMaster = _userMasterRepository.Table.Where(x => x.EntityId == model.EntityId && x.UserType == model.UserType)?.FirstOrDefault();
            if (userMaster != null)
            {
                userMaster.FirstName = model.FirstName ?? userMaster.FirstName;
                userMaster.MiddleName = model.MiddleName ?? userMaster.MiddleName;
                userMaster.LastName = model.LastName ?? userMaster.LastName;
                userMaster.EmailId = model.EmailId ?? userMaster.EmailId ?? userMaster.EmailId;
                userMaster.IsActive = model.IsActive;
                _userMasterRepository.Update(userMaster);
            }
        }

        protected virtual bool ValidatedGeneralPersonData(GeneralPersonModel generalPersonModel, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (IsNull(generalPersonModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            int generalEnumaratorId = 0;
            if (ValidateUserwiseGeneralPerson(generalPersonModel, ref errorMessage, ref generalEnumaratorId))
            {
                string userNameBasedOn = _organisationCentrewiseUserNameRegistrationRepository.Table.Where(x => x.CentreCode == generalPersonModel.SelectedCentreCode && x.UserType.ToLower() == generalPersonModel.UserType.ToLower())?.Select(y => y.UserNameBasedOn)?.FirstOrDefault();
                if (string.IsNullOrEmpty(userNameBasedOn))
                {
                    errorMessage = "Organisation Centrewise UserName Registration not set";
                    return false;
                }
                else if (userNameBasedOn == UserNameRegistrationTypeEnum.MobileNumber.ToString() && string.IsNullOrEmpty(generalPersonModel.MobileNumber))
                {
                    errorMessage = "Mobile Number is required";
                    return false;
                }
                else if (userNameBasedOn == UserNameRegistrationTypeEnum.EmailId.ToString() && string.IsNullOrEmpty(generalPersonModel.EmailId))
                {
                    errorMessage = "EmailId is required";
                    return false;
                }
                else if (userNameBasedOn == UserNameRegistrationTypeEnum.MobileNumber.ToString())
                {
                    if (_userMasterRepository.Table.Any(x => x.UserName == generalPersonModel.MobileNumber && x.UserType.ToLower() == generalPersonModel.UserType.ToLower()))
                    {
                        errorMessage = "Mobile Number is already exist.";
                        return false;
                    }
                }
                else if (userNameBasedOn == UserNameRegistrationTypeEnum.EmailId.ToString())
                {
                    if (_userMasterRepository.Table.Any(x => x.UserName == generalPersonModel.EmailId && x.UserType.ToLower() == generalPersonModel.UserType.ToLower()))
                    {
                        errorMessage = "EmailId is already exist.";
                        return false;
                    }
                }

                if (!new CoditechRepository<GeneralRunningNumbers>(_serviceProvider.GetService<Coditech_Entities>()).Table.Any(x => x.KeyFieldEnumId == generalEnumaratorId && x.IsActive && !x.IsRowLock && x.CentreCode == generalPersonModel.SelectedCentreCode))
                {
                    errorMessage = "General Running Numbers is not set for Person Code.";
                    return false;
                }
            }
            return true;
        }

        protected virtual bool ValidateUserwiseGeneralPerson(GeneralPersonModel generalPersonModel, ref string errorMessage, ref int generalEnumaratorId)
        {
            if (generalPersonModel.UserType.Equals(UserTypeEnum.Employee.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                if (string.IsNullOrEmpty(generalPersonModel.SelectedCentreCode) || string.IsNullOrEmpty(generalPersonModel.SelectedDepartmentId))
                {
                    errorMessage = "SelectedCentreCode or SelectedDepartmentId is null";
                    return false;
                }

                generalEnumaratorId = GetEnumIdByEnumCode(GeneralRunningNumberForEnum.EmployeeRegistration.ToString(), GeneralEnumaratorGroupCodeEnum.GeneralRunningNumberFor.ToString());
                if (generalEnumaratorId == 0)
                {
                    errorMessage = "Employee Registration is null.";
                    return false;
                }
            }
            else if (generalPersonModel.UserType.Equals(UserTypeEnum.Patient.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                if (string.IsNullOrEmpty(generalPersonModel.SelectedCentreCode))
                {
                    errorMessage = "SelectedCentreCode is null";
                    return false;
                }

                generalEnumaratorId = GetEnumIdByEnumCode(GeneralRunningNumberForEnum.PatientUAHNumber.ToString(), GeneralEnumaratorGroupCodeEnum.GeneralRunningNumberFor.ToString());
                if (generalEnumaratorId == 0)
                {
                    errorMessage = "PatientRegistration is null";
                    return false;
                }
            }

            return true;
        }

        protected virtual void InsertPatient(GeneralPersonModel generalPersonModel, List<GeneralSystemGlobleSettingModel> settingMasterList)
        {
            generalPersonModel.PersonCode = GenerateRegistrationCode(GeneralRunningNumberForEnum.PatientUAHNumber.ToString(), generalPersonModel.SelectedCentreCode);
            HospitalPatientRegistration hospitalPatientRegistration = new HospitalPatientRegistration()
            {
                PersonId = generalPersonModel.PersonId,
                HospitalPatientTypeId = generalPersonModel.HospitalPatientTypeId,
                UAHNumber = generalPersonModel.PersonCode,
                UserType = generalPersonModel.UserType,
                CentreCode = generalPersonModel.SelectedCentreCode,
                RegistrationDate = DateTime.Now
            };
            hospitalPatientRegistration = _hospitalPatientRegistrationRepository.Insert(hospitalPatientRegistration);
            //Check Is Patient need to Login
            if (hospitalPatientRegistration?.HospitalPatientRegistrationId > 0 && settingMasterList?.FirstOrDefault(x => x.FeatureName.Equals(GeneralSystemGlobleSettingEnum.IsPatientLogin.ToString(), StringComparison.InvariantCultureIgnoreCase)).FeatureValue == "1")
            {
                InsertUserMasterDetails(generalPersonModel, hospitalPatientRegistration.HospitalPatientRegistrationId, false);
            }
        }

        protected virtual string ReplaceResetPassworkLink(GeneralPersonModel generalPersonModel, string emailTemplate, string url, string resetPassToken)
        {
            string messageText = emailTemplate;
            messageText = ReplaceTokenWithMessageText(EmailTemplateTokenConstant.FirstName, generalPersonModel.FirstName, messageText);
            messageText = ReplaceTokenWithMessageText(EmailTemplateTokenConstant.LastName, generalPersonModel.LastName, messageText);
            messageText = ReplaceTokenWithMessageText(EmailTemplateTokenConstant.Url, url, messageText);
            messageText = ReplaceTokenWithMessageText(EmailTemplateTokenConstant.OTP, resetPassToken, messageText);
            return ReplaceEmailTemplateFooter(generalPersonModel.SelectedCentreCode, messageText);
        }

        protected virtual int CalculateAge(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;

            // Go back to the year the person was born in case of a leap year
            if (dateOfBirth > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }

        protected virtual int CalculateBirthYear(int age)
        {
            DateTime today = DateTime.Today;
            int currentYear = today.Year;
            int birthYear = currentYear - age;

            // Adjust for cases where the birthday hasn't occurred yet this year
            if (today < new DateTime(currentYear, today.Month, today.Day).AddYears(-age))
            {
                birthYear--;
            }

            return birthYear;
        }
        protected virtual void InsertPersonDetails(GeneralPersonModel generalPersonModel, List<GeneralSystemGlobleSettingModel> settingMasterList, string customData = null)
        {
            if (generalPersonModel.UserType.Equals(UserTypeEnum.Employee.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                InsertEmployee(generalPersonModel, settingMasterList, generalPersonModel.IsActive);
            }
            else if (generalPersonModel.UserType.Equals(UserTypeEnum.Patient.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                InsertPatient(generalPersonModel, settingMasterList);
            }
        }

        protected virtual void UpdateIsActiveFlagForUserType(GeneralPersonModel generalPersonModel)
        {
            if (generalPersonModel.UserType == UserTypeEnum.Employee.ToString())
            {
                EmployeeMaster employeeMaster = _employeeMasterRepository.Table.Where(x => x.EmployeeId == generalPersonModel.EntityId).FirstOrDefault();
                if (employeeMaster != null)
                {
                    employeeMaster.IsActive = generalPersonModel.IsActive;
                    _employeeMasterRepository.Update(employeeMaster);
                }
            }
        }
        #endregion
    }
}

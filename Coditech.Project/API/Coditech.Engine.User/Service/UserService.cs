using Coditech.API.Data;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;

using System.Data;
using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Service
{
    public class UserService : BaseService, IUserService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        protected readonly ICoditechEmail _coditechEmail;
        private readonly ICoditechRepository<AdminRoleApplicableDetails> _adminRoleApplicableDetailsRepository;
        private readonly ICoditechRepository<AdminRoleMenuDetails> _adminRoleMenuDetailsRepository;
        private readonly ICoditechRepository<AdminRoleCentreRights> _adminRoleCentreRightsRepository;
        private readonly ICoditechRepository<UserMaster> _userMasterRepository;
        private readonly ICoditechRepository<GeneralEnumaratorGroup> _generalEnumaratorGroupRepository;
        private readonly ICoditechRepository<GeneralEnumaratorMaster> _generalEnumaratorRepository;
        private readonly ICoditechRepository<GeneralPerson> _generalPersonRepository;
        private readonly ICoditechRepository<GeneralPersonAddress> _generalPersonAddressRepository;
        private readonly ICoditechRepository<GymMemberDetails> _gymMemberDetailsRepository;
        private readonly ICoditechRepository<EmployeeMaster> _employeeMasterRepository;
        private readonly ICoditechRepository<HospitalPatientRegistration> _hospitalPatientRegistrationRepository;
        private readonly ICoditechRepository<OrganisationCentrewiseUserNameRegistration> _organisationCentrewiseUserNameRegistrationRepository;
        private readonly ICoditechRepository<EmployeeService> _employeeServiceRepository;
        public UserService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider, ICoditechEmail coditechEmail) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _coditechEmail = coditechEmail;
            _adminRoleApplicableDetailsRepository = new CoditechRepository<AdminRoleApplicableDetails>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleCentreRightsRepository = new CoditechRepository<AdminRoleCentreRights>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleMenuDetailsRepository = new CoditechRepository<AdminRoleMenuDetails>(_serviceProvider.GetService<Coditech_Entities>());
            _userMasterRepository = new CoditechRepository<UserMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _generalEnumaratorGroupRepository = new CoditechRepository<GeneralEnumaratorGroup>(_serviceProvider.GetService<Coditech_Entities>());
            _generalEnumaratorRepository = new CoditechRepository<GeneralEnumaratorMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _generalPersonRepository = new CoditechRepository<GeneralPerson>(_serviceProvider.GetService<Coditech_Entities>());
            _generalPersonAddressRepository = new CoditechRepository<GeneralPersonAddress>(_serviceProvider.GetService<Coditech_Entities>());
            _gymMemberDetailsRepository = new CoditechRepository<GymMemberDetails>(_serviceProvider.GetService<Coditech_Entities>());
            _employeeMasterRepository = new CoditechRepository<EmployeeMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _hospitalPatientRegistrationRepository = new CoditechRepository<HospitalPatientRegistration>(_serviceProvider.GetService<Coditech_Entities>());
            _organisationCentrewiseUserNameRegistrationRepository = new CoditechRepository<OrganisationCentrewiseUserNameRegistration>(_serviceProvider.GetService<Coditech_Entities>());
            _employeeServiceRepository = new CoditechRepository<EmployeeService>(_serviceProvider.GetService<Coditech_Entities>());
        }

        #region Public
        public virtual UserModel Login(UserLoginModel userLoginModel)
        {
            if (IsNull(userLoginModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            userLoginModel.Password = MD5Hash(userLoginModel.Password);
            UserMaster userMasterData = _userMasterRepository.Table.FirstOrDefault(x => x.UserName == userLoginModel.UserName && x.Password == userLoginModel.Password);

            if (IsNull(userMasterData))
                throw new CoditechException(ErrorCodes.NotFound, null);
            else if (!userMasterData.IsActive)
                throw new CoditechException(ErrorCodes.ContactAdministrator, null);

            UserModel userModel = userMasterData?.FromEntityToModel<UserModel>();

            userModel.IsAdminUser = IsAdminUser(userModel.UserType);
            //Bind Role
            BindRoleTypes(userModel);

            List<UserModuleMaster> userAllModuleList = GetAllActiveModuleList();
            List<UserMainMenuMaster> userAllMenuList = GetAllActiveMenuList();
            List<AdminRoleMenuDetails> userRoleMenuList = new List<AdminRoleMenuDetails>();
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

                    //Bind Balance Sheet
                    userModel.BalanceSheetList = BindAccountBalanceSheetByRoleId(userModel);

                    //Bind accessible Centre
                    List<string> centreCodeList = _adminRoleCentreRightsRepository.Table.Where(x => x.AdminRoleMasterId == userModel.SelectedAdminRoleMasterId)?.Select(y => y.CentreCode)?.ToList();
                    List<UserAccessibleCentreModel> allCentreList = OrganisationCentreList();
                    foreach (string centreCode in centreCodeList)
                    {
                        userModel.AccessibleCentreList.Add(allCentreList.First(x => x.CentreCode == centreCode));
                    }
                }
            }
            else
            {
                //Bind Menu And Modules For Non Admin User
                BindMenuAndModulesForAdminUser(userModel, userAllModuleList, userAllMenuList);
                userModel.AccessibleCentreList = OrganisationCentreList();
            }
            userModel.SelectedCentreCode = userModel?.AccessibleCentreList?.FirstOrDefault()?.CentreCode;

            userModel.GeneralEnumaratorList = BindEnumarator();
            userModel.GeneralSystemGlobleSettingList = GetSystemGlobleSettingList();
            return userModel;
        }

        //Change Password.
        public virtual ChangePasswordModel ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (IsNull(changePasswordModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            UserMaster userMasterData = _userMasterRepository.Table.FirstOrDefault(x => x.UserMasterId == changePasswordModel.UserMasterId);
            if (IsNotNull(userMasterData) && userMasterData.Password == MD5Hash(changePasswordModel.CurrentPassword))
            {
                userMasterData.Password = MD5Hash(changePasswordModel.NewPassword);
                _userMasterRepository.Update(userMasterData);
            }
            else
            {
                changePasswordModel.HasError = true;
                changePasswordModel.ErrorMessage = "Current Password DoesNot Match ";
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
                    ParentMenuCode = item.ParentMenuCode,
                    MenuDisplaySeqNo = item.MenuDisplaySeqNo,
                });
            }
            return menuList;
        }

        #region General Person
        public virtual GeneralPersonModel InsertPersonInformation(GeneralPersonModel generalPersonModel)
        {

            if (!ValidatedGeneralPersonData(generalPersonModel))
            {
                generalPersonModel.HasError = true;
                generalPersonModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
                return generalPersonModel;
            }
            GeneralPerson generalPerson = generalPersonModel.FromModelToEntity<GeneralPerson>();

            // Create new Person and return it.
            GeneralPerson personData = _generalPersonRepository.Insert(generalPerson);
            if (personData?.PersonId > 0)
            {
                generalPersonModel.PersonId = personData.PersonId;
                List<GeneralSystemGlobleSettingModel> settingMasterList = GetSystemGlobleSettingList();
                string password = settingMasterList?.FirstOrDefault(x => x.FeatureName.Equals(GeneralSystemGlobleSettingEnum.DefaultPassword.ToString(), StringComparison.InvariantCultureIgnoreCase)).FeatureValue;
                generalPersonModel.Password = MD5Hash(password);
                if (generalPersonModel.UserType.Equals(UserTypeEnum.GymMember.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    InsertGymMember(generalPersonModel, settingMasterList);
                }
                else if (generalPersonModel.UserType.Equals(UserTypeEnum.Employee.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    InsertEmployee(generalPersonModel, settingMasterList);
                }
                else if (generalPersonModel.UserType.Equals(UserTypeEnum.Patient.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    InsertPatient(generalPersonModel, settingMasterList);
                }
            }
            else
            {
                generalPersonModel.HasError = true;
                generalPersonModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
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

            //Update Gym Member
            bool isPersonUpdated = _generalPersonRepository.Update(generalPerson);
            if (isPersonUpdated)
            {
                UserMaster userMaster = new UserMaster()
                {
                    EntityId = generalPersonModel.EntityId,
                    FirstName = generalPersonModel.FirstName,
                    MiddleName = generalPersonModel.MiddleName,
                    LastName = generalPersonModel.LastName,
                    EmailId = generalPersonModel.EmailId,
                };
                UpdateUserMasterDetails(userMaster);
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
            }
            else
            {
                generalPersonAddress = _generalPersonAddressRepository.Insert(generalPersonAddress);
                if (generalPersonAddress?.GeneralPersonAddressId == 0)
                {
                    generalPersonAddressModel.HasError = true;
                    generalPersonAddressModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
                }
            }
            return generalPersonAddressModel;
        }
        #endregion General Person Addresses

        #endregion

        #region Protected Method

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
                    ModuleName = item.ModuleName,
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
                        userModuleModel.DefaultMenuLink = $"{userMenuModel?.ControllerName?.ToLower()}/{userMenuModel?.ActionName?.ToLower()}";
                        userModel.ModuleList.Add(userModuleModel);
                    }
                }
            }
        }

        protected virtual List<UserBalanceSheetModel> BindAccountBalanceSheetByRoleId(UserModel userModel)
        {
            return new List<UserBalanceSheetModel>();
            int errorCode = 0;
            CoditechViewRepository<UserBalanceSheetModel> objStoredProc = new CoditechViewRepository<UserBalanceSheetModel>();
            objStoredProc.SetParameter("@iAdminRoleId", userModel.SelectedAdminRoleMasterId, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@iErrorCode", userModel.ErrorCode, ParameterDirection.Output, DbType.Int32);
            List<UserBalanceSheetModel> accountBalanceSheetList = objStoredProc.ExecuteStoredProcedureList("USP_GetBalancesheetList @iAdminRoleId,@iErrorCode OUT", 1, out errorCode)?.ToList();
            if (errorCode == 0 && accountBalanceSheetList?.Count > 0)
            {
                userModel.SelectedBalanceId = accountBalanceSheetList.FirstOrDefault().BalsheetID;
                userModel.SelectedBalanceSheet = accountBalanceSheetList.FirstOrDefault().ActBalsheetHeadDesc;
            }
            return accountBalanceSheetList;
        }

        protected virtual void UpdateUserMasterDetails(UserMaster model)
        {
            CoditechRepository<UserMaster> _userMasterRepository = new CoditechRepository<UserMaster>(_serviceProvider.GetService<Coditech_Entities>());

            UserMaster userMaster = _userMasterRepository.Table.FirstOrDefault(x => x.EntityId == model.EntityId);
            if (userMaster != null)
            {
                userMaster.FirstName = model.FirstName ?? userMaster.FirstName;
                userMaster.MiddleName = model.MiddleName ?? userMaster.MiddleName;
                userMaster.LastName = model.LastName ?? userMaster.LastName;
                userMaster.EmailId = model.EmailId ?? userMaster.EmailId ?? userMaster.EmailId;
                _userMasterRepository.Update(userMaster);
            }
        }

        protected virtual List<GeneralEnumaratorModel> BindEnumarator()
        {
            List<GeneralEnumaratorModel> generalEnumaratorList = new List<GeneralEnumaratorModel>();
            generalEnumaratorList = (from generalEnumarator in _generalEnumaratorRepository.Table
                                     join generalEnumaratorGroup in _generalEnumaratorGroupRepository.Table on generalEnumarator.GeneralEnumaratorGroupId equals generalEnumaratorGroup.GeneralEnumaratorGroupId
                                     where generalEnumarator.IsActive
                                     select new GeneralEnumaratorModel
                                     {
                                         GeneralEnumaratorGroupId = generalEnumaratorGroup.GeneralEnumaratorGroupId,
                                         EnumGroupCode = generalEnumaratorGroup.EnumGroupCode,
                                         GeneralEnumaratorId = generalEnumarator.GeneralEnumaratorId,
                                         EnumName = generalEnumarator.EnumName,
                                         EnumDisplayText = generalEnumarator.EnumDisplayText,
                                         EnumValue = generalEnumarator.EnumValue,
                                         SequenceNumber = generalEnumarator.SequenceNumber,
                                     })?.ToList();
            return generalEnumaratorList;
        }

        protected virtual void InsertUserMasterDetails(GeneralPersonModel generalPersonModel, long entityId)
        {
            string userNameBasedOn = _organisationCentrewiseUserNameRegistrationRepository.Table.Where(x => x.CentreCode == generalPersonModel.SelectedCentreCode && x.UserType.ToLower() == generalPersonModel.UserType.ToLower())?.Select(y => y.UserNameBasedOn)?.FirstOrDefault();
            UserMaster userMaster = generalPersonModel.FromModelToEntity<UserMaster>();
            userMaster.EntityId = entityId;
            if (string.IsNullOrEmpty(userNameBasedOn))
            {
                userMaster.UserName = generalPersonModel.PersonCode;
            }
            else if (userNameBasedOn.Equals(UserNameRegistrationTypeEnum.EmailId.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                userMaster.UserName = generalPersonModel.EmailId;
            }
            else if (userNameBasedOn.Equals(UserNameRegistrationTypeEnum.MobileNumber.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                userMaster.UserName = generalPersonModel.MobileNumber;
            }
            else if (userNameBasedOn.Equals(UserNameRegistrationTypeEnum.PersonCode.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                userMaster.UserName = generalPersonModel.PersonCode;
            }
            userMaster = _userMasterRepository.Insert(userMaster);
        }

        protected virtual bool ValidatedGeneralPersonData(GeneralPersonModel generalPersonModel)
        {
            bool status = true;
            if (IsNull(generalPersonModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            int generalEnumaratorId = 0;
            if (generalPersonModel.UserType.Equals(UserTypeEnum.GymMember.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                if (string.IsNullOrEmpty(generalPersonModel.SelectedCentreCode))
                {
                    status = false;
                    _coditechLogging.LogMessage("SelectedCentreCode or SelectedDepartmentId is null", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                }

                generalEnumaratorId = GetEnumIdByEnumCode(GeneralRunningNumberForEnum.GymMemberRegistration.ToString());
                if (generalEnumaratorId == 0)
                {
                    _coditechLogging.LogMessage("EmployeeRegistration is null", CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
                    status = false;
                }
            }
            else if (generalPersonModel.UserType.Equals(UserTypeEnum.Employee.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                if (string.IsNullOrEmpty(generalPersonModel.SelectedCentreCode) || string.IsNullOrEmpty(generalPersonModel.SelectedDepartmentId))
                {
                    status = false;
                    _coditechLogging.LogMessage("SelectedCentreCode or SelectedDepartmentId is null", CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
                }

                generalEnumaratorId = GetEnumIdByEnumCode(GeneralRunningNumberForEnum.EmployeeRegistration.ToString());
                if (generalEnumaratorId == 0)
                {
                    _coditechLogging.LogMessage("EmployeeRegistration is null", CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
                    status = false;
                }
            }
            else if (generalPersonModel.UserType.Equals(UserTypeEnum.Patient.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                if (string.IsNullOrEmpty(generalPersonModel.SelectedCentreCode))
                {
                    status = false;
                    _coditechLogging.LogMessage("SelectedCentreCode is null", CoditechLoggingEnum.Components.HospitalPatientRegistration.ToString(), TraceLevel.Error);
                }

                generalEnumaratorId = GetEnumIdByEnumCode(GeneralRunningNumberForEnum.PatientUAHNumber.ToString());
                if (generalEnumaratorId == 0)
                {
                    _coditechLogging.LogMessage("PatientRegistration is null", CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
                    status = false;
                }
            }

            if (!new CoditechRepository<GeneralRunningNumbers>(_serviceProvider.GetService<Coditech_Entities>()).Table.Any(x => x.KeyFieldEnumId == generalEnumaratorId && x.IsActive && !x.IsRowLock && x.CentreCode == generalPersonModel.SelectedCentreCode))
            {
                status = false;
                _coditechLogging.LogMessage("General Running Numbers row not present", generalPersonModel.UserType.ToString(), TraceLevel.Error);
            }
            return status;
        }

        protected virtual void InsertPatient(GeneralPersonModel generalPersonModel, List<GeneralSystemGlobleSettingModel> settingMasterList)
        {
            generalPersonModel.PersonCode = GenerateRegistrationCode(GeneralRunningNumberForEnum.PatientUAHNumber.ToString(), generalPersonModel.SelectedCentreCode);
            HospitalPatientRegistration hospitalPatientRegistration = new HospitalPatientRegistration()
            {
                PersonId = generalPersonModel.PersonId,
                UAHNumber = generalPersonModel.PersonCode,
                UserType = generalPersonModel.UserType,
                CentreCode = generalPersonModel.SelectedCentreCode,
                RegistrationDate = DateTime.Now
            };
            hospitalPatientRegistration = _hospitalPatientRegistrationRepository.Insert(hospitalPatientRegistration);
            //Check Is Patient need to Login
            if (hospitalPatientRegistration?.HospitalPatientRegistrationId > 0 && settingMasterList?.FirstOrDefault(x => x.FeatureName.Equals(GeneralSystemGlobleSettingEnum.IsPatientLogin.ToString(), StringComparison.InvariantCultureIgnoreCase)).FeatureValue == "1")
            {
                InsertUserMasterDetails(generalPersonModel, hospitalPatientRegistration.HospitalPatientRegistrationId);
            }
        }

        protected virtual void InsertEmployee(GeneralPersonModel generalPersonModel, List<GeneralSystemGlobleSettingModel> settingMasterList)
        {
            generalPersonModel.PersonCode = GenerateRegistrationCode(GeneralRunningNumberForEnum.EmployeeRegistration.ToString(), generalPersonModel.SelectedCentreCode);
            EmployeeMaster employeeMaster = new EmployeeMaster()
            {
                PersonId = generalPersonModel.PersonId,
                PersonCode = generalPersonModel.PersonCode,
                UserType = generalPersonModel.UserType,
                CentreCode = generalPersonModel.SelectedCentreCode,
                GeneralDepartmentMasterId = Convert.ToInt16(generalPersonModel.SelectedDepartmentId)
            };
            employeeMaster = _employeeMasterRepository.Insert(employeeMaster);
            //Check Is Employee need to Login
            if (employeeMaster?.EmployeeId > 0)
            {
                generalPersonModel.EntityId = employeeMaster.EmployeeId;
                EmployeeService employeeService = new EmployeeService()
                {
                    EmployeeId = employeeMaster.EmployeeId,
                    EmployeeCode = generalPersonModel.PersonCode,
                    IsCurrentPosition = true
                };
                _employeeServiceRepository.Insert(employeeService);
                if (settingMasterList?.FirstOrDefault(x => x.FeatureName.Equals(GeneralSystemGlobleSettingEnum.IsEmployeeLogin.ToString(), StringComparison.InvariantCultureIgnoreCase)).FeatureValue == "1")
                {   //GeneralEmailTemplateModel emailTemplateModel = GetEmailTemplateByCode(employeeMaster.CentreCode, EmailTemplateCodeEnum.EmployeeRegistration.ToString());
                    //if (IsNotNull(emailTemplateModel) && !string.IsNullOrEmpty(generalPersonModel.EmailId))
                    //{
                    //    _coditechEmail.SendEmail(employeeMaster.CentreCode, generalPersonModel.EmailId, "", emailTemplateModel.Subject, emailTemplateModel.EmailTemplate);
                    //}

                    InsertUserMasterDetails(generalPersonModel, employeeMaster.EmployeeId);
                }
            }
        }

        protected virtual void InsertGymMember(GeneralPersonModel generalPersonModel, List<GeneralSystemGlobleSettingModel> settingMasterList)
        {
            generalPersonModel.PersonCode = GenerateRegistrationCode(GeneralRunningNumberForEnum.GymMemberRegistration.ToString(), generalPersonModel.SelectedCentreCode);
            GymMemberDetails gymMemberDetails = new GymMemberDetails()
            {
                CentreCode = generalPersonModel.SelectedCentreCode,
                PersonId = generalPersonModel.PersonId,
                PersonCode = generalPersonModel.PersonCode,
                UserType = generalPersonModel.UserType
            };
            gymMemberDetails = _gymMemberDetailsRepository.Insert(gymMemberDetails);

            //Check Is Gym Member need to Login
            if (gymMemberDetails?.GymMemberDetailId > 0 && settingMasterList?.FirstOrDefault(x => x.FeatureName.Equals(GeneralSystemGlobleSettingEnum.IsGymMemberLogin.ToString(), StringComparison.InvariantCultureIgnoreCase)).FeatureValue == "1")
            {
                InsertUserMasterDetails(generalPersonModel, gymMemberDetails.GymMemberDetailId);
            }
        }

        #endregion
    }
}

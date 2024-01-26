using Coditech.API.Data;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;

using System.Data;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Service
{
    public class UserService : BaseService, IUserService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AdminRoleApplicableDetail> _adminRoleApplicableDetailsRepository;
        private readonly ICoditechRepository<AdminRoleMenuDetail> _adminRoleMenuDetailsRepository;
        private readonly ICoditechRepository<UserMaster> _userMasterRepository;
        private readonly ICoditechRepository<GeneralEnumaratorGroup> _generalEnumaratorGroupRepository;
        private readonly ICoditechRepository<GeneralEnumaratorMaster> _generalEnumaratorRepository;
        private readonly ICoditechRepository<GeneralPerson> _generalPersonRepository;
        private readonly ICoditechRepository<GeneralPersonAddress> _generalPersonAddressRepository;
        private readonly ICoditechRepository<GymMemberDetails> _gymMemberDetailsRepository;
        private readonly ICoditechRepository<UserType> _userTypeRepository;
        private readonly ICoditechRepository<EmployeeMaster> _employeeMasterRepository;
        public UserService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _adminRoleApplicableDetailsRepository = new CoditechRepository<AdminRoleApplicableDetail>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleMenuDetailsRepository = new CoditechRepository<AdminRoleMenuDetail>(_serviceProvider.GetService<Coditech_Entities>());
            _userMasterRepository = new CoditechRepository<UserMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _generalEnumaratorGroupRepository = new CoditechRepository<GeneralEnumaratorGroup>(_serviceProvider.GetService<Coditech_Entities>());
            _generalEnumaratorRepository = new CoditechRepository<GeneralEnumaratorMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _generalPersonRepository = new CoditechRepository<GeneralPerson>(_serviceProvider.GetService<Coditech_Entities>());
            _generalPersonAddressRepository = new CoditechRepository<GeneralPersonAddress>(_serviceProvider.GetService<Coditech_Entities>());
            _gymMemberDetailsRepository = new CoditechRepository<GymMemberDetails>(_serviceProvider.GetService<Coditech_Entities>());
            _userTypeRepository = new CoditechRepository<UserType>(_serviceProvider.GetService<Coditech_Entities>());
            _employeeMasterRepository = new CoditechRepository<EmployeeMaster>(_serviceProvider.GetService<Coditech_Entities>());
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
            else if (userMasterData.IsActive == false)
                throw new CoditechException(ErrorCodes.ContactAdministrator, null);

            UserModel userModel = userMasterData?.FromEntityToModel<UserModel>();

            userModel.IsAdminUser = IsAdminUser(userModel.UserType);
            //Bind Role
            BindRoleTypes(userModel);

            List<UserModuleMaster> userAllModuleList = GetAllActiveModuleList();
            List<UserMainMenuMaster> userAllMenuList = GetAllActiveMenuList();
            List<AdminRoleMenuDetail> userRoleMenuList = new List<AdminRoleMenuDetail>();
            if (!userModel.IsAdminUser)
            {
                userRoleMenuList = _adminRoleMenuDetailsRepository.Table.Where(x => x.IsActive && x.AdminRoleMasterId == userModel.SelectedRoleId)?.ToList();
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
                    userModel.BalanceSheetList = BindAccountBalanceSheetByRoleID(userModel);
                }
            }
            else
            {
                //Bind Menu And Modules For Non Admin User
                BindMenuAndModulesForAdminUser(userModel, userAllModuleList, userAllMenuList);
                userModel.AccessibleCentreList = OrganisationCentreList();
            }
            userModel.SelectedCentreCode = userModel.AccessibleCentreList?.FirstOrDefault().CentreCode;

            userModel.GeneralEnumaratorList = BindEnumarator();

            return userModel;
        }

        public virtual GeneralPersonModel InsertPersonInformation(GeneralPersonModel generalPersonModel)
        {
            GeneralPerson generalPerson = generalPersonModel.FromModelToEntity<GeneralPerson>();

            // Create new Person and return it.
            GeneralPerson personData = _generalPersonRepository.Insert(generalPerson);
            if (personData?.PersonId > 0)
            {
                generalPersonModel.PersonId = personData.PersonId;
                List<GeneralSystemGlobleSettingMaster> settingMasterList = GetSystemGlobleSettingList();
                string password = settingMasterList?.FirstOrDefault(x => x.FeatureName.Equals(GeneralSystemGlobleSettingEnum.DefaultPassword.ToString(), StringComparison.InvariantCultureIgnoreCase)).FeatureValue;
                string registrationFormat = _userTypeRepository.Table.FirstOrDefault(x => x.UserTypeCode == generalPersonModel.UserType)?.RegistrationFormat;
                generalPersonModel.Password = MD5Hash(password);
                generalPersonModel.PersonCode = registrationFormat;
                if (generalPersonModel.UserType.Equals(UserTypeEnum.GymMember.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    GymMemberDetails gymMemberDetails = new GymMemberDetails()
                    {
                        PersonId = generalPersonModel.PersonId,
                        PersonCode = generalPersonModel.PersonCode,
                        UserType = generalPersonModel.UserType
                    };
                    gymMemberDetails = _gymMemberDetailsRepository.Insert(gymMemberDetails);

                    //Check Is Gym Member need to Login
                    if (settingMasterList?.FirstOrDefault(x => x.FeatureName.Equals(GeneralSystemGlobleSettingEnum.IsGymMemberLogin.ToString(), StringComparison.InvariantCultureIgnoreCase)).FeatureValue == "1" && gymMemberDetails?.GymMemberDetailId > 0)
                    {
                        if (gymMemberDetails?.GymMemberDetailId > 0)
                            InsertUserMasterDetails(generalPersonModel);
                    }
                }
                else if (generalPersonModel.UserType.Equals(UserTypeEnum.Employee.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    EmployeeMaster employeeMaster = new EmployeeMaster()
                    {
                        PersonId = generalPersonModel.PersonId,
                        PersonCode = generalPersonModel.PersonCode,
                        UserType = generalPersonModel.UserType,
                        EmployeeDesignationMasterId = generalPersonModel.EmployeeDesignationMasterId,
                        OrganisationCentrewiseDepartmentId = generalPersonModel.OrganisationCentrewiseDepartmentId
                    };
                    employeeMaster = _employeeMasterRepository.Insert(employeeMaster);

                    //Check Is Gym Member need to Login
                    if (settingMasterList?.FirstOrDefault(x => x.FeatureName.Equals(GeneralSystemGlobleSettingEnum.IsEmployeeLogin.ToString(), StringComparison.InvariantCultureIgnoreCase)).FeatureValue == "1" && employeeMaster?.EmployeeId > 0)
                    {
                        if (employeeMaster?.EmployeeId > 0)
                            InsertUserMasterDetails(generalPersonModel);
                    }
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

            GeneralPerson generalPerson = generalPersonModel.FromModelToEntity<GeneralPerson>();

            //Update Gym Member
            bool isPersonUpdated = _generalPersonRepository.Update(generalPerson);
            if (isPersonUpdated)
            {
                UserMaster userMasterData = _userMasterRepository.Table.FirstOrDefault(x => x.PersonId == generalPersonModel.PersonId);
                if (userMasterData.EmailId != generalPersonModel.EmailId)
                {
                    userMasterData.EmailId = generalPersonModel.EmailId;
                    _userMasterRepository.Update(userMasterData);
                }
            }
            else
            {
                generalPersonModel.HasError = true;
                generalPersonModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isPersonUpdated;
        }

        public virtual List<UserModuleMaster> GetActiveModuleList()
        {
            return base.GetAllActiveModuleList();
        }

        public virtual List<UserMainMenuMaster> GetActiveMenuList(string moduleCodel)
        {
            return base.GetAllActiveMenuList(moduleCodel);
        }

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
            return generalPersonAddressListModel;
        }

        public virtual GeneralPersonAddressModel InsertUpdateGeneralPersonAddress(GeneralPersonAddressModel generalPersonAddressModel)
        {

            if (IsNull(generalPersonAddressModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalPersonAddressModel.PersonId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PersonId"));

            return new GeneralPersonAddressModel();
            GeneralPerson generalPerson = generalPersonAddressModel.FromModelToEntity<GeneralPerson>();

            //Update Gym Member
            bool isPersonUpdated = _generalPersonRepository.Update(generalPerson);
            if (isPersonUpdated)
            {
                UserMaster userMasterData = _userMasterRepository.Table.FirstOrDefault(x => x.PersonId == generalPersonAddressModel.PersonId);
                if (userMasterData.EmailId != generalPersonAddressModel.EmailAddress)
                {
                    userMasterData.EmailId = generalPersonAddressModel.EmailAddress;
                    _userMasterRepository.Update(userMasterData);
                }
            }
            else
            {
                generalPersonAddressModel.HasError = true;
                generalPersonAddressModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            //return isPersonUpdated;
        }
        #endregion

        #region Protected Method

        //Bind Role Types
        protected virtual void BindRoleTypes(UserModel userModel)
        {
            if (!userModel.IsAdminUser)
            {
                List<AdminRoleApplicableDetail> roleList = _adminRoleApplicableDetailsRepository.Table.Where(x => x.EmployeeId == userModel.UserMasterId && x.IsActive)?.ToList();
                if (roleList?.Count() == 0)
                {
                    throw new CoditechException(ErrorCodes.ContactAdministrator, null);
                }
                else
                {
                    userModel.SelectedRoleId = roleList.FirstOrDefault(x => x.RoleType == APIConstant.Regular).AdminRoleMasterId;
                    //userModel.SelectedRoleCode = roleList.FirstOrDefault(x => x.RoleType == RARIndiaConstant.Regular). AdminRoleCode;
                    foreach (AdminRoleApplicableDetail item in roleList)
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
                userModel.MenuList.Add(new UserMenuModel()
                {
                    UserMainMenuMasterId = item.UserMainMenuMasterId,
                    ModuleCode = item.ModuleCode,
                    MenuCode = item.MenuCode,
                    MenuName = item.MenuName,
                    ParentMenuID = item.ParentMenuId,
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
        protected virtual void BindMenuAndModulesForNonAdminUser(UserModel userModel, List<UserModuleMaster> userAllModuleList, List<UserMainMenuMaster> userAllMenuList, List<AdminRoleMenuDetail> userRoleMenuList)
        {
            //Bind Menu & Module for non admin user
            foreach (AdminRoleMenuDetail item in userRoleMenuList)
            {
                UserMainMenuMaster userMenuModel = userAllMenuList.FirstOrDefault(x => x.MenuCode == item.MenuCode);
                if (IsNotNull(userMenuModel))
                {
                    userModel.MenuList.Add(new UserMenuModel()
                    {
                        UserMainMenuMasterId = userMenuModel.UserMainMenuMasterId,
                        ModuleCode = userMenuModel.ModuleCode,
                        MenuCode = userMenuModel.MenuCode,
                        MenuName = userMenuModel.MenuName,
                        ParentMenuID = userMenuModel.ParentMenuId,
                        MenuDisplaySeqNo = userMenuModel.MenuDisplaySeqNo,
                        ControllerName = userMenuModel.ControllerName?.ToLower(),
                        ActionName = userMenuModel.ActionName?.ToLower(),
                        MenuLink = $"{userMenuModel.ControllerName?.ToLower()}/{userMenuModel.ActionName?.ToLower()}",
                        MenuToolTip = userMenuModel.MenuToolTip,
                        MenuIconName = userMenuModel.MenuIconName
                    });

                    if (!userModel.ModuleList.Any(x => x.ModuleCode == userMenuModel.ModuleCode))
                    {
                        userModel.ModuleList.Add(userAllModuleList.FirstOrDefault(x => x.ModuleCode == userMenuModel.ModuleCode).FromEntityToModel<UserModuleModel>());
                    }
                }
            }
        }

        protected virtual List<UserBalanceSheetModel> BindAccountBalanceSheetByRoleID(UserModel userModel)
        {
            int errorCode = 0;
            CoditechViewRepository<UserBalanceSheetModel> objStoredProc = new CoditechViewRepository<UserBalanceSheetModel>();
            objStoredProc.SetParameter("@iAdminRoleId", userModel.SelectedRoleId, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@iErrorCode", userModel.ErrorCode, ParameterDirection.Output, DbType.Int32);
            List<UserBalanceSheetModel> accountBalanceSheetList = objStoredProc.ExecuteStoredProcedureList("USP_GetBalancesheetList @iAdminRoleId,@iErrorCode OUT", 1, out errorCode)?.ToList();
            if (errorCode == 0 && accountBalanceSheetList?.Count > 0)
            {
                userModel.SelectedBalanceId = accountBalanceSheetList.FirstOrDefault().BalsheetID;
                userModel.SelectedBalanceSheet = accountBalanceSheetList.FirstOrDefault().ActBalsheetHeadDesc;
            }
            return accountBalanceSheetList;
        }

        protected virtual void UpdateUserMasterDetails(GeneralPersonModel model)
        {
            UserMaster userMaster = _userMasterRepository.Table.FirstOrDefault(x => x.PersonId == model.PersonId);
            if (userMaster != null)
            {
                userMaster.FirstName = model.FirstName;
                userMaster.MiddleName = model.MiddleName;
                userMaster.LastName = model.LastName;
                userMaster.EmailId = model.EmailId;
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

        protected virtual void InsertUserMasterDetails(GeneralPersonModel generalPersonModel)
        {
            UserMaster userMaster = generalPersonModel.FromModelToEntity<UserMaster>();
            userMaster.UserName = generalPersonModel.PersonCode;
            userMaster = _userMasterRepository.Insert(userMaster);
        }
        #endregion
    }
}

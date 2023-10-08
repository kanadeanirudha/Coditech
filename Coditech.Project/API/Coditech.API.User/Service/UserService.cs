﻿using Coditech.API.Data;
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
        private readonly ICoditechRepository<UserModuleMaster> _userModuleMasterRepository;
        private readonly ICoditechRepository<UserMainMenuMaster> _userMainMenuMasterRepository;
        private readonly ICoditechRepository<OrganisationCentreMaster> _organisationCentreMasterRepository;
        public UserService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _adminRoleApplicableDetailsRepository = new CoditechRepository<AdminRoleApplicableDetail>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleMenuDetailsRepository = new CoditechRepository<AdminRoleMenuDetail>(_serviceProvider.GetService<Coditech_Entities>());
            _userMasterRepository = new CoditechRepository<UserMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _userModuleMasterRepository = new CoditechRepository<UserModuleMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _userMainMenuMasterRepository = new CoditechRepository<UserMainMenuMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _organisationCentreMasterRepository = new CoditechRepository<OrganisationCentreMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public UserModel Login(UserLoginModel userLoginModel)
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

            List<UserModuleMaster> userAllModuleList = _userModuleMasterRepository.Table.Where(x => x.ModuleActiveFlag == true)?.ToList();
            List<UserMainMenuMaster> userAllMenuList = _userMainMenuMasterRepository.Table.Where(x => x.IsEnable == true)?.ToList();
            List<AdminRoleMenuDetail> userRoleMenuList = new List<AdminRoleMenuDetail>();
            if (!userModel.IsAdminUser)
            {
                userRoleMenuList = _adminRoleMenuDetailsRepository.Table.Where(x => x.IsActive == true && x.AdminRoleMasterId == userModel.SelectedRoleId)?.ToList();
                if (userRoleMenuList?.Count == 0)
                {
                    throw new CoditechException(ErrorCodes.ContactAdministrator, null);
                }
                else
                {
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
            return userModel;
        }

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
                userModel.ModuleList.Add(new UserModuleModel()
                {
                    UserModuleMasterId = item.UserModuleMasterId,
                    ModuleCode = item.ModuleCode,
                    ModuleName = item.ModuleName,
                    ModuleSeqNumber = item.ModuleSeqNumber,
                    ModuleTooltip = item.ModuleTooltip,
                    ModuleIconName = item.ModuleIconName,
                    ModuleColorClass = item.ModuleColorClass,
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
                    MenuLink = item.ControllerName?.ToLower() + "/" + item.ActionName?.ToLower(),
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
                        MenuLink = userMenuModel.ControllerName?.ToLower() + "/" + userMenuModel.ActionName?.ToLower(),
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

        protected virtual List<UserAccessibleCentreModel> OrganisationCentreList()
        {
            List<OrganisationCentreMaster> centreList = _organisationCentreMasterRepository.Table.ToList();
            List<UserAccessibleCentreModel> organisationCentreList = new List<UserAccessibleCentreModel>();
            foreach (OrganisationCentreMaster item in centreList)
            {
                organisationCentreList.Add(new UserAccessibleCentreModel()
                {
                    CentreCode = item.CentreCode,
                    CentreName = item.CentreName,
                    ScopeIdentity = "Centre"
                });
            }

            return organisationCentreList;
        }
        #endregion
    }
}

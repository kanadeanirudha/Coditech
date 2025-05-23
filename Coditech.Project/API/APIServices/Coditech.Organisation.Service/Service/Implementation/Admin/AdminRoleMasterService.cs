﻿using Coditech.API.Data;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Service
{
    public class AdminRoleMasterService : BaseService, IAdminRoleMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AdminRoleMaster> _adminRoleMasterRepository;
        private readonly ICoditechRepository<AdminRoleCentreRights> _adminRoleCentreRightsRepository;
        private readonly ICoditechRepository<AdminSanctionPost> _adminSanctionPostRepository;
        private readonly ICoditechRepository<AdminRoleMenuDetails> _adminRoleMenuDetailsRepository;
        private readonly ICoditechRepository<AdminRoleApplicableDetails> _adminRoleApplicableDetailsRepository;
        private readonly ICoditechRepository<AdminRoleMediaFolderAction> _adminRoleMediaFolderActionRepository;
        private readonly ICoditechRepository<AdminRoleMediaFolders> _adminRoleMediaFoldersRepository;
        private readonly ICoditechRepository<MediaFolderMaster> _mediaFolderMasterRepository;
        public AdminRoleMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _adminRoleMasterRepository = new CoditechRepository<AdminRoleMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleCentreRightsRepository = new CoditechRepository<AdminRoleCentreRights>(_serviceProvider.GetService<Coditech_Entities>());
            _adminSanctionPostRepository = new CoditechRepository<AdminSanctionPost>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleMenuDetailsRepository = new CoditechRepository<AdminRoleMenuDetails>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleApplicableDetailsRepository = new CoditechRepository<AdminRoleApplicableDetails>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleMediaFolderActionRepository = new CoditechRepository<AdminRoleMediaFolderAction>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleMediaFoldersRepository = new CoditechRepository<AdminRoleMediaFolders>(_serviceProvider.GetService<Coditech_Entities>());
            _mediaFolderMasterRepository = new CoditechRepository<MediaFolderMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        #region Public
        public virtual AdminRoleListModel GetAdminRoleList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            int selectedDepartmentId = 0;
            int.TryParse(filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedDepartmentId, StringComparison.CurrentCultureIgnoreCase))?.FilterValue, out selectedDepartmentId);

            string selectedCentreCode = filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedCentreCode, StringComparison.CurrentCultureIgnoreCase))?.FilterValue;

            filters.RemoveAll(x => x.FilterName == FilterKeys.SelectedDepartmentId || x.FilterName == FilterKeys.SelectedCentreCode);

            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<AdminRoleModel> objStoredProc = new CoditechViewRepository<AdminRoleModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@DepartmentId", selectedDepartmentId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AdminRoleModel> adminRoleMasterList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetAdminRoleList @CentreCode,@DepartmentId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            AdminRoleListModel listModel = new AdminRoleListModel();

            listModel.AdminRoleList = adminRoleMasterList?.Count > 0 ? adminRoleMasterList : new List<AdminRoleModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Get adminRoleMaster by adminRoleMaster id.
        public virtual AdminRoleModel GetAdminRoleDetailsById(int adminRoleMasterId)
        {
            if (adminRoleMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminRoleMasterId"));

            //Get the adminRoleMaster Details based on id.
            AdminRoleMaster adminRoleMasterData = _adminRoleMasterRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMasterId)?.FirstOrDefault();
            AdminRoleModel adminRoleMasterModel = adminRoleMasterData.FromEntityToModel<AdminRoleModel>();
            adminRoleMasterModel.SelectedRoleWiseCentres = _adminRoleCentreRightsRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMasterId && x.IsActive == true)?.Select(y => y.CentreCode)?.Distinct().ToList();
            AdminSanctionPost adminSanctionPost = _adminSanctionPostRepository.GetById(adminRoleMasterData.AdminSanctionPostId);
            adminRoleMasterModel.SelectedCentreCode = adminSanctionPost.CentreCode;
            adminRoleMasterModel.SelectedDepartmentId = Convert.ToString(adminSanctionPost.DepartmentId);
            adminRoleMasterModel.SelectedCentreCodeForSelf = adminSanctionPost.CentreCode;
            adminRoleMasterModel.AllCentreList = OrganisationCentreList();
            return adminRoleMasterModel;
        }

        //Update adminRoleMaster.
        public virtual bool UpdateAdminRole(AdminRoleModel adminRoleMasterModel)
        {
            bool isAdminRoleMasterUpdated = false;
            if (IsNull(adminRoleMasterModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);
            if (adminRoleMasterModel.AdminRoleMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminRoleMasterId"));

            AdminRoleMaster adminRoleMasterData = _adminRoleMasterRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMasterModel.AdminRoleMasterId)?.FirstOrDefault();
            adminRoleMasterData.MonitoringLevel = adminRoleMasterModel.MonitoringLevel;
            adminRoleMasterData.OthCentreLevel = adminRoleMasterModel.MonitoringLevel == APIConstant.Self ? string.Empty : "Selected";
            adminRoleMasterData.IsLoginAllowFromOutside = adminRoleMasterModel.IsLoginAllowFromOutside;
            adminRoleMasterData.IsAttendaceAllowFromOutside = adminRoleMasterModel.IsAttendaceAllowFromOutside;
            adminRoleMasterData.IsActive = adminRoleMasterModel.IsActive;
            adminRoleMasterData.DashboardFormEnumId = adminRoleMasterModel.DashboardFormEnumId;
            adminRoleMasterData.LimitedDataAccessEnumId = adminRoleMasterModel.LimitedDataAccessEnumId;
            adminRoleMasterData.ModifiedBy = adminRoleMasterModel.ModifiedBy;

            //Update adminRoleMaster
            isAdminRoleMasterUpdated = _adminRoleMasterRepository.Update(adminRoleMasterData);
            if (!isAdminRoleMasterUpdated)
            {
                adminRoleMasterModel.HasError = true;
                adminRoleMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
                return isAdminRoleMasterUpdated;
            }

            //update  Admin Role Centre Right
            List<AdminRoleCentreRights> adminRoleCentreRightList = _adminRoleCentreRightsRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMasterModel.AdminRoleMasterId && x.CentreCode != adminRoleMasterModel.SelectedCentreCodeForSelf)?.ToList();

            if (adminRoleMasterModel.MonitoringLevel == APIConstant.Self || (adminRoleMasterModel.MonitoringLevel == APIConstant.Other && adminRoleMasterModel?.SelectedRoleWiseCentres?.Count == 0))
            {
                adminRoleCentreRightList = adminRoleCentreRightList.Where(x => x.IsActive == true)?.ToList();
                if (adminRoleCentreRightList?.Count > 0 && adminRoleCentreRightList.Any(x => x.IsActive == true))
                {
                    adminRoleCentreRightList.ForEach(x => { x.IsActive = false; x.ModifiedBy = adminRoleMasterModel.ModifiedBy; });
                    _adminRoleCentreRightsRepository.BatchUpdate(adminRoleCentreRightList);
                }
            }
            else
            {
                adminRoleMasterModel.AllCentreList = OrganisationCentreList();
                foreach (UserAccessibleCentreModel item in adminRoleMasterModel?.AllCentreList?.Where(x => x.CentreCode != adminRoleMasterModel.SelectedCentreCodeForSelf))
                {
                    string selectedCentreCode = adminRoleMasterModel?.SelectedRoleWiseCentres?.FirstOrDefault(x => x == item.CentreCode);
                    AdminRoleCentreRights adminRoleCentreRight = adminRoleCentreRightList?.FirstOrDefault(x => x.CentreCode == item.CentreCode && x.AdminRoleMasterId == adminRoleMasterModel.AdminRoleMasterId);

                    if (adminRoleCentreRight == null && !string.IsNullOrEmpty(selectedCentreCode))
                    {
                        adminRoleCentreRight = new AdminRoleCentreRights()
                        {
                            AdminRoleMasterId = adminRoleMasterModel.AdminRoleMasterId,
                            CentreCode = selectedCentreCode,
                            IsActive = true,
                            CreatedBy = adminRoleMasterModel.CreatedBy,
                            ModifiedBy = adminRoleMasterModel.ModifiedBy
                        };
                        _adminRoleCentreRightsRepository.Insert(adminRoleCentreRight);
                    }
                    else if (adminRoleCentreRight?.CentreCode == selectedCentreCode && adminRoleCentreRight?.IsActive == false)
                    {
                        adminRoleCentreRight.IsActive = true;
                        adminRoleCentreRight.ModifiedBy = adminRoleMasterModel.ModifiedBy;
                        _adminRoleCentreRightsRepository.Update(adminRoleCentreRight);
                    }
                    else if (selectedCentreCode == null && adminRoleCentreRight?.IsActive == true)
                    {
                        adminRoleCentreRight.IsActive = false;
                        adminRoleCentreRight.ModifiedBy = adminRoleMasterModel.ModifiedBy;
                        _adminRoleCentreRightsRepository.Update(adminRoleCentreRight);
                    }
                }
            }
            return isAdminRoleMasterUpdated;
        }

        //Delete AdminRoleMaster.
        public virtual bool DeleteAdminRoleMaster(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminRoleMasterId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("AdminRoleMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteAdminRoleMaster @AdminRoleMasterId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }

        //Get Admin Role Menu Details By Id.
        public virtual AdminRoleMenuDetailsModel GetAdminRoleMenuDetailsById(int adminRoleMasterId, string moduleCode)
        {
            if (adminRoleMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminRoleMasterId"));

            //Get the adminRoleMaster Details based on id.
            AdminRoleMaster adminRoleMasterData = _adminRoleMasterRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMasterId)?.FirstOrDefault();
            AdminRoleMenuDetailsModel adminRoleMenuDetailsModel = new AdminRoleMenuDetailsModel();
            if (IsNotNull(adminRoleMasterData))
            {
                adminRoleMenuDetailsModel.AdminRoleMasterId = adminRoleMasterData.AdminRoleMasterId;
                adminRoleMenuDetailsModel.AdminRoleCode = adminRoleMasterData.AdminRoleCode;
                adminRoleMenuDetailsModel.SanctionPostName = adminRoleMasterData.SanctionPostName;

                AdminSanctionPost adminSanctionPost = _adminSanctionPostRepository.GetById(adminRoleMasterData.AdminSanctionPostId);
                if (IsNotNull(adminSanctionPost))
                {
                    adminRoleMenuDetailsModel.SelectedCentreCode = adminSanctionPost.CentreCode;
                    adminRoleMenuDetailsModel.SelectedDepartmentId = Convert.ToString(adminSanctionPost.DepartmentId);
                }
                if (!string.IsNullOrEmpty(moduleCode))
                {
                    List<AdminRoleMenuDetails> associatedMenuCodeList = _adminRoleMenuDetailsRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMenuDetailsModel.AdminRoleMasterId)?.ToList();
                    adminRoleMenuDetailsModel.MenuList = GetActiveMenuList(moduleCode, associatedMenuCodeList);
                }
            }
            return adminRoleMenuDetailsModel;
        }

        //Insert Update Admin Role Menu Details
        public virtual bool InsertUpdateAdminRoleMenuDetails(AdminRoleMenuDetailsModel adminRoleMenuDetailsModel)
        {
            if (IsNull(adminRoleMenuDetailsModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);
            if (adminRoleMenuDetailsModel.AdminRoleMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminRoleMasterId"));

            List<AdminRoleMenuDetails> associatedMenuCodeList = _adminRoleMenuDetailsRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMenuDetailsModel.AdminRoleMasterId && x.ModuleCode == adminRoleMenuDetailsModel.ModuleCode)?.ToList();
            List<AdminRoleMenuDetails> insertList = new List<AdminRoleMenuDetails>();
            List<AdminRoleMenuDetails> updateList = new List<AdminRoleMenuDetails>();
            if (associatedMenuCodeList?.Count > 0)
            {
                if (string.IsNullOrEmpty(adminRoleMenuDetailsModel.SelectedMenuList))
                {
                    associatedMenuCodeList.ForEach(x => { x.IsActive = false; x.DisableDate = DateTime.Now; });
                    _adminRoleMenuDetailsRepository.BatchUpdate(associatedMenuCodeList);
                }
                else
                {
                    List<string> selectedMenuList = adminRoleMenuDetailsModel.SelectedMenuList.Split(',')?.ToList();
                    foreach (AdminRoleMenuDetails item in associatedMenuCodeList)
                    {
                        if (!selectedMenuList.Where(x => x == item.MenuCode).Any())
                        {
                            item.IsActive = false;
                            item.DisableDate = DateTime.Now;
                            updateList.Add(item);
                        }
                        else
                        {
                            item.IsActive = true;
                            item.EnableDate = DateTime.Now;
                            updateList.Add(item);
                        }
                    }

                    foreach (string item in selectedMenuList)
                    {
                        if (!associatedMenuCodeList.Where(x => x.MenuCode == item).Any())
                        {
                            BindAdminRoleMenuDetails(adminRoleMenuDetailsModel, insertList, item);
                        }
                    }

                    if (updateList?.Count > 0)
                    {
                        _adminRoleMenuDetailsRepository.BatchUpdate(updateList);
                    }
                    if (insertList?.Count > 0)
                    {
                        _adminRoleMenuDetailsRepository.Insert(insertList);
                    }
                }
            }
            else
            {

                foreach (string item in adminRoleMenuDetailsModel.SelectedMenuList.Split(','))
                {
                    BindAdminRoleMenuDetails(adminRoleMenuDetailsModel, insertList, item);
                }
                _adminRoleMenuDetailsRepository.Insert(insertList);
            }

            return true;
        }

        public virtual AdminRoleApplicableDetailsListModel RoleAllocatedToUserList(int adminRoleMasterId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);

            AdminRoleApplicableDetailsListModel listModel = GetAssociateUnAssociatedRoleUserList(adminRoleMasterId, true, pageListModel);
            listModel.BindPageListModel(pageListModel);

            AdminRoleMaster adminRoleMasterData = _adminRoleMasterRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMasterId)?.FirstOrDefault();
            listModel.AdminRoleCode = adminRoleMasterData.AdminRoleCode;
            listModel.SanctionPostName = adminRoleMasterData.SanctionPostName;
            return listModel;
        }

        public virtual AdminRoleApplicableDetailsModel GetAssociateUnAssociateAdminRoleToUser(int adminRoleMasterId, int adminRoleApplicableDetailId)
        {
            if (adminRoleMasterId < 1 && adminRoleApplicableDetailId == 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminRoleMasterId"));

            AdminRoleApplicableDetailsModel adminRoleApplicableDetailsModel = new AdminRoleApplicableDetailsModel();
            if (adminRoleApplicableDetailId > 0)
            {
                AdminRoleApplicableDetails adminRoleApplicableDetails = _adminRoleApplicableDetailsRepository.GetById(adminRoleApplicableDetailId);
                if (IsNotNull(adminRoleApplicableDetailsModel))
                {
                    adminRoleApplicableDetailsModel = adminRoleApplicableDetails.FromEntityToModel<AdminRoleApplicableDetailsModel>();
                    adminRoleMasterId = adminRoleApplicableDetailsModel.AdminRoleMasterId;
                    GeneralPersonModel generalPersonModel = GetGeneralPersonDetailsByEntityType(adminRoleApplicableDetails.EmployeeId, UserTypeEnum.Employee.ToString());
                    if (IsNotNull(generalPersonModel))
                    {
                        adminRoleApplicableDetailsModel.EmployeeList = new List<EmployeeMasterModel>();
                        adminRoleApplicableDetailsModel.EmployeeList.Add(new EmployeeMasterModel()
                        {
                            EmployeeId = adminRoleApplicableDetailsModel.EmployeeId,
                            FullName = $"{generalPersonModel.FirstName} {generalPersonModel.LastName}",
                            FullNameWithPersonCode = $"{generalPersonModel.FirstName} {generalPersonModel.LastName}({generalPersonModel.PersonCode})"
                        });
                    }
                }
            }

            AdminRoleMaster adminRoleMasterData = _adminRoleMasterRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMasterId)?.FirstOrDefault();
            if (IsNotNull(adminRoleMasterData))
            {
                //Bind the Filter, sorts & Paging details.
                adminRoleApplicableDetailsModel.AdminRoleCode = adminRoleMasterData.AdminRoleCode;
                adminRoleApplicableDetailsModel.SanctionPostName = adminRoleMasterData.SanctionPostName;
                adminRoleApplicableDetailsModel.AdminRoleMasterId = adminRoleMasterData.AdminRoleMasterId;

                if (adminRoleMasterId > 0 && adminRoleApplicableDetailId == 0)
                {
                    //Bind the Filter, sorts & Paging details.
                    PageListModel pageListModel = new PageListModel(null, null, 1, int.MaxValue);

                    AdminRoleApplicableDetailsListModel listModel = GetAssociateUnAssociatedRoleUserList(adminRoleMasterId, false, pageListModel);
                    if (listModel?.AdminRoleApplicableDetailsList?.Count > 0)
                    {
                        adminRoleApplicableDetailsModel.EmployeeList = new List<EmployeeMasterModel>();
                        foreach (AdminRoleApplicableDetailsModel item in listModel.AdminRoleApplicableDetailsList)
                        {
                            adminRoleApplicableDetailsModel.EmployeeList.Add(new EmployeeMasterModel()
                            {
                                EmployeeId = item.EmployeeId,
                                FullName = $"{item.FirstName} {item.LastName}",
                                FullNameWithPersonCode = $"{item.FirstName} {item.LastName} ({item.PersonCode})",
                            });
                        }
                    }
                }
            }
            return adminRoleApplicableDetailsModel;
        }

        //Associate UnAssociate Admin Role To User
        public virtual bool AssociateUnAssociateAdminRoleToUser(AdminRoleApplicableDetailsModel adminRoleApplicableDetailsModel)
        {
            if (IsNull(adminRoleApplicableDetailsModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);
            if (adminRoleApplicableDetailsModel.AdminRoleMasterId < 1 && adminRoleApplicableDetailsModel.AdminRoleApplicableDetailId == 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminRoleMasterId"));

            AdminRoleApplicableDetails adminRoleApplicableDetails = adminRoleApplicableDetailsModel.FromModelToEntity<AdminRoleApplicableDetails>();
            if (adminRoleApplicableDetailsModel.AdminRoleApplicableDetailId > 0)
                return _adminRoleApplicableDetailsRepository.Update(adminRoleApplicableDetails);
            else
            {
                AdminSanctionPost adminSanctionPost = _adminSanctionPostRepository.GetById(adminRoleApplicableDetailsModel.AdminRoleMasterId);
                adminRoleApplicableDetails.RoleType = adminSanctionPost.DesignationType;
                adminRoleApplicableDetails = _adminRoleApplicableDetailsRepository.Insert(adminRoleApplicableDetails);
            }
            return adminRoleApplicableDetails?.AdminRoleApplicableDetailId > 0 ? true : false;
        }

        //Get adminRoleMaster by adminRoleMaster id.
        public virtual AdminRoleMediaFolderActionModel GetAdminRoleWiseMediaFolderActionById(int adminRoleMasterId)
        {
            if (adminRoleMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminRoleMasterId"));

            //Get the adminRoleMaster Details based on id.
            AdminRoleMaster adminRoleMasterData = _adminRoleMasterRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMasterId)?.FirstOrDefault();
            AdminSanctionPost adminSanctionPost = _adminSanctionPostRepository.GetById(adminRoleMasterData.AdminSanctionPostId);
            AdminRoleMediaFolderActionModel adminRoleMediaFolderActionModel = new AdminRoleMediaFolderActionModel()
            {
                SelectedCentreCode = adminSanctionPost.CentreCode,
                SelectedDepartmentId = Convert.ToString(adminSanctionPost.DepartmentId),
                AdminRoleCode = adminRoleMasterData.AdminRoleCode,
                SanctionPostName = adminSanctionPost.SanctionedPostDescription,
                AdminRoleMasterId = adminRoleMasterId,
            };
            AdminRoleMediaFolderAction adminRoleMediaFolderAction = _adminRoleMediaFolderActionRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMasterId)?.FirstOrDefault();
            if (IsNotNull(adminRoleMediaFolderAction))
            {
                adminRoleMediaFolderActionModel.AdminRoleMediaFolderActionId = adminRoleMediaFolderAction.AdminRoleMediaFolderActionId;
                if (!string.IsNullOrEmpty(adminRoleMediaFolderAction?.MediaAction))
                {
                    adminRoleMediaFolderActionModel.SelectedMediaActions = new List<string>();
                    foreach (string item in adminRoleMediaFolderAction?.MediaAction?.Split(','))
                    {
                        adminRoleMediaFolderActionModel.SelectedMediaActions.Add(item);
                    }
                }
            }
            return adminRoleMediaFolderActionModel;
        }

        //Insert Update Admin RoleWise Media Folder Action.
        public virtual bool InsertUpdateAdminRoleWiseMediaFolderAction(AdminRoleMediaFolderActionModel adminRoleMediaFolderActionModel)
        {
            if (IsNull(adminRoleMediaFolderActionModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            AdminRoleMediaFolderAction adminRoleMediaFolderAction = adminRoleMediaFolderActionModel.FromModelToEntity<AdminRoleMediaFolderAction>();
            adminRoleMediaFolderAction.MediaAction = adminRoleMediaFolderActionModel?.SelectedMediaActions?.Count > 0 ? string.Join(',', adminRoleMediaFolderActionModel.SelectedMediaActions) : "";

            bool status = false;
            if (adminRoleMediaFolderActionModel.AdminRoleMediaFolderActionId > 0)
            {
                //Update MediaSettingMaster
                status = _adminRoleMediaFolderActionRepository.Update(adminRoleMediaFolderAction);
            }
            else
            {
                adminRoleMediaFolderAction = _adminRoleMediaFolderActionRepository.Insert(adminRoleMediaFolderAction);
                if (adminRoleMediaFolderAction.AdminRoleMediaFolderActionId > 0)
                {
                    status = true;
                }
            }

            if (!status)
            {
                adminRoleMediaFolderActionModel.HasError = true;
                adminRoleMediaFolderActionModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return status;
        }

        //Get Admin Role Wise Folder Access By Id
        public virtual AdminRoleMediaFoldersModel GetAdminRoleWiseMediaFoldersById(int adminRoleMasterId)
        {
            if (adminRoleMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminRoleMasterId"));

            //Get the adminRoleMaster Details based on id.
            AdminRoleMaster adminRoleMasterData = _adminRoleMasterRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMasterId)?.FirstOrDefault();
            AdminRoleMediaFoldersModel adminRoleMediaFoldersModel = new AdminRoleMediaFoldersModel();
            if (IsNotNull(adminRoleMasterData))
            {
                adminRoleMediaFoldersModel.AdminRoleMasterId = adminRoleMasterData.AdminRoleMasterId;
                adminRoleMediaFoldersModel.AdminRoleCode = adminRoleMasterData.AdminRoleCode;
                adminRoleMediaFoldersModel.SanctionPostName = adminRoleMasterData.SanctionPostName;

                AdminSanctionPost adminSanctionPost = _adminSanctionPostRepository.GetById(adminRoleMasterData.AdminSanctionPostId);
                if (IsNotNull(adminSanctionPost))
                {
                    adminRoleMediaFoldersModel.SelectedCentreCode = adminSanctionPost.CentreCode;
                    adminRoleMediaFoldersModel.SelectedDepartmentId = Convert.ToString(adminSanctionPost.DepartmentId);
                }
                List<MediaFolderMaster> list = _mediaFolderMasterRepository.Table.Where(x => x.IsActive)?.ToList();
                if (list?.Count > 0)
                {
                    adminRoleMediaFoldersModel.TreeViewList = new List<TreeViewModel>();
                    List<AdminRoleMediaFolders> adminRoleMediaFolderList = _adminRoleMediaFoldersRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMasterId && x.IsActive)?.ToList();
                    foreach (MediaFolderMaster mediaFolderMaster in list)
                    {
                        adminRoleMediaFoldersModel.TreeViewList.Add(new TreeViewModel()
                        {
                            id = Convert.ToString(mediaFolderMaster.MediaFolderMasterId),
                            text = mediaFolderMaster.FolderName,
                            parent = mediaFolderMaster.MediaFolderParentId > 0 ? Convert.ToString(mediaFolderMaster.MediaFolderParentId) : "#",
                            state = new State()
                            {
                                opened = true,
                                selected = adminRoleMediaFolderList?.Count > 0 ? adminRoleMediaFolderList.Any(x => x.MediaFolderMasterId == mediaFolderMaster.MediaFolderMasterId) : false,
                            }
                        });
                    }
                }
            }
            return adminRoleMediaFoldersModel;
        }

        //Insert Update Admin Role Media Folder
        public virtual bool InsertUpdateAdminRoleWiseMediaFolders(AdminRoleMediaFoldersModel adminRoleMediaFoldersModel)
        {
            if (IsNull(adminRoleMediaFoldersModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);
            if (adminRoleMediaFoldersModel.AdminRoleMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminRoleMasterId"));

            List<AdminRoleMediaFolders> associatedMediaFolderList = _adminRoleMediaFoldersRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMediaFoldersModel.AdminRoleMasterId)?.ToList();
            List<AdminRoleMediaFolders> insertList = new List<AdminRoleMediaFolders>();
            if (associatedMediaFolderList?.Count > 0)
            {
                List<AdminRoleMediaFolders> updateList = new List<AdminRoleMediaFolders>();
                if (string.IsNullOrEmpty(adminRoleMediaFoldersModel.SelectedMediaFolderList))
                {
                    associatedMediaFolderList.ForEach(x => { x.IsActive = false; });
                    _adminRoleMediaFoldersRepository.BatchUpdate(associatedMediaFolderList);
                }
                else
                {
                    List<string> selectedMediaFolderList = adminRoleMediaFoldersModel.SelectedMediaFolderList.Split(',')?.ToList();
                    foreach (AdminRoleMediaFolders item in associatedMediaFolderList)
                    {
                        if (!selectedMediaFolderList.Where(x => x == Convert.ToString(item.MediaFolderMasterId)).Any())
                        {
                            item.IsActive = false;
                            updateList.Add(item);
                        }
                        else
                        {
                            item.IsActive = true;
                            updateList.Add(item);
                        }
                    }

                    foreach (string item in selectedMediaFolderList)
                    {
                        if (!associatedMediaFolderList.Where(x => x.MediaFolderMasterId == Convert.ToInt32(item)).Any())
                        {
                            insertList.Add(new AdminRoleMediaFolders()
                            {
                                AdminRoleMasterId = adminRoleMediaFoldersModel.AdminRoleMasterId,
                                MediaFolderMasterId = Convert.ToInt32(item),
                                IsActive = true
                            });
                        }
                    }

                    if (updateList?.Count > 0)
                    {
                        _adminRoleMediaFoldersRepository.BatchUpdate(updateList);
                    }
                    if (insertList?.Count > 0)
                    {
                        _adminRoleMediaFoldersRepository.Insert(insertList);
                    }
                }
            }
            else
            {

                foreach (string item in adminRoleMediaFoldersModel.SelectedMediaFolderList.Split(','))
                {
                    insertList.Add(new AdminRoleMediaFolders()
                    {
                        AdminRoleMasterId = adminRoleMediaFoldersModel.AdminRoleMasterId,
                        MediaFolderMasterId = Convert.ToInt32(item),
                        IsActive = true
                    });
                }
                _adminRoleMediaFoldersRepository.Insert(insertList);
            }

            return true;
        }

        #endregion

        #region protected
        protected virtual List<UserMainMenuModel> GetActiveMenuList(string moduleCodel, List<AdminRoleMenuDetails> associatedMenuCodeList)
        {
            List<UserMainMenuModel> menuList = new List<UserMainMenuModel>();
            foreach (UserMainMenuMaster item in base.GetAllActiveMenuList(moduleCodel))
            {
                bool isAssociatedToAdminRole = associatedMenuCodeList?.Count > 0 ? associatedMenuCodeList.Any(x => x.MenuCode == item.MenuCode && x.IsActive) : false;
                menuList.Add(new UserMainMenuModel()
                {
                    UserMainMenuMasterId = item.UserMainMenuMasterId,
                    ModuleCode = item.ModuleCode,
                    MenuCode = item.MenuCode,
                    MenuName = item.MenuName,
                    ParentMenuCode = item.ParentMenuCode,
                    MenuDisplaySeqNo = item.MenuDisplaySeqNo,
                    IsAssociatedToAdminRole = isAssociatedToAdminRole
                });
            }
            return menuList;
        }
        protected virtual void BindAdminRoleMenuDetails(AdminRoleMenuDetailsModel adminRoleMenuDetailsModel, List<AdminRoleMenuDetails> insertList, string item)
        {
            insertList.Add(new AdminRoleMenuDetails()
            {
                AdminRoleMasterId = adminRoleMenuDetailsModel.AdminRoleMasterId,
                AdminRoleCode = adminRoleMenuDetailsModel.AdminRoleCode,
                ModuleCode = adminRoleMenuDetailsModel.ModuleCode,
                MenuCode = item,
                EnableDate = DateTime.Now,
                IsActive = true
            });
        }
        protected virtual AdminRoleApplicableDetailsListModel GetAssociateUnAssociatedRoleUserList(int adminRoleMasterId, bool isAssociated, PageListModel pageListModel)
        {
            CoditechViewRepository<AdminRoleApplicableDetailsModel> objStoredProc = new CoditechViewRepository<AdminRoleApplicableDetailsModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@AdminRoleMasterId", adminRoleMasterId, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@IsAssociated", isAssociated, ParameterDirection.Input, DbType.Boolean);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AdminRoleApplicableDetailsModel> adminRoleApplicableDetailsList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetAdminRoleApplicableDetailsList @AdminRoleMasterId,@IsAssociated,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            AdminRoleApplicableDetailsListModel listModel = new AdminRoleApplicableDetailsListModel();
            listModel.AdminRoleApplicableDetailsList = adminRoleApplicableDetailsList?.Count > 0 ? adminRoleApplicableDetailsList : new List<AdminRoleApplicableDetailsModel>();
            return listModel;
        }
        #endregion
    }
}

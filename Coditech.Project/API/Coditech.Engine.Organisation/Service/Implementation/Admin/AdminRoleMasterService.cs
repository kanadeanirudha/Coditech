using Coditech.API.Data;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;

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
        public AdminRoleMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _adminRoleMasterRepository = new CoditechRepository<AdminRoleMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleCentreRightsRepository = new CoditechRepository<AdminRoleCentreRights>(_serviceProvider.GetService<Coditech_Entities>());
            _adminSanctionPostRepository = new CoditechRepository<AdminSanctionPost>(_serviceProvider.GetService<Coditech_Entities>());
            _adminRoleMenuDetailsRepository = new CoditechRepository<AdminRoleMenuDetails>(_serviceProvider.GetService<Coditech_Entities>());
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
            AdminRoleMaster adminRoleMasterData = _adminRoleMasterRepository.Table.FirstOrDefault(x => x.AdminRoleMasterId == adminRoleMasterId);
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

            AdminRoleMaster adminRoleMasterData = _adminRoleMasterRepository.Table.FirstOrDefault(x => x.AdminRoleMasterId == adminRoleMasterModel.AdminRoleMasterId);
            adminRoleMasterData.MonitoringLevel = adminRoleMasterModel.MonitoringLevel;
            adminRoleMasterData.OthCentreLevel = adminRoleMasterModel.MonitoringLevel == APIConstant.Self ? string.Empty : "Selected";
            adminRoleMasterData.IsLoginAllowFromOutside = adminRoleMasterModel.IsLoginAllowFromOutside;
            adminRoleMasterData.IsAttendaceAllowFromOutside = adminRoleMasterModel.IsAttendaceAllowFromOutside;
            adminRoleMasterData.IsActive = adminRoleMasterModel.IsActive;
            adminRoleMasterData.DashboardFormEnumId = adminRoleMasterModel.DashboardFormEnumId;
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
            AdminRoleMaster adminRoleMasterData = _adminRoleMasterRepository.Table.FirstOrDefault(x => x.AdminRoleMasterId == adminRoleMasterId);
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

        //Update adminRoleMaster.
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
            CoditechViewRepository<AdminRoleApplicableDetailsModel> objStoredProc = new CoditechViewRepository<AdminRoleApplicableDetailsModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@AdminRoleMasterId", adminRoleMasterId, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AdminRoleApplicableDetailsModel> adminRoleApplicableDetailsList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetAdminRoleApplicableDetailsList @AdminRoleMasterId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            AdminRoleApplicableDetailsListModel listModel = new AdminRoleApplicableDetailsListModel();

            listModel.AdminRoleApplicableDetailsList = adminRoleApplicableDetailsList?.Count > 0 ? adminRoleApplicableDetailsList : new List<AdminRoleApplicableDetailsModel>();
            listModel.BindPageListModel(pageListModel);

            AdminRoleMaster adminRoleMasterData = _adminRoleMasterRepository.Table.FirstOrDefault(x => x.AdminRoleMasterId == adminRoleMasterId);
            listModel.AdminRoleCode = adminRoleMasterData.AdminRoleCode;
            listModel.SanctionPostName = adminRoleMasterData.SanctionPostName;
            return listModel;
        }

        #endregion

        #region protected
        protected virtual List<UserMenuModel> GetActiveMenuList(string moduleCodel, List<AdminRoleMenuDetails> associatedMenuCodeList)
        {
            List<UserMenuModel> menuList = new List<UserMenuModel>();
            foreach (UserMainMenuMaster item in base.GetAllActiveMenuList(moduleCodel))
            {
                bool isAssociatedToAdminRole = associatedMenuCodeList?.Count > 0 ? associatedMenuCodeList.Any(x => x.MenuCode == item.MenuCode && x.IsActive) : false;
                menuList.Add(new UserMenuModel()
                {
                    UserMainMenuMasterId = item.UserMainMenuMasterId,
                    ModuleCode = item.ModuleCode,
                    MenuCode = item.MenuCode,
                    MenuName = item.MenuName,
                    ParentMenuId = item.ParentMenuId,
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
        #endregion
    }
}

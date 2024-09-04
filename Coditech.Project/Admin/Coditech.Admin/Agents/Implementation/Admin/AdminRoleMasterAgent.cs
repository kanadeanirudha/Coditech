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

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.Admin.Agents
{
    public class AdminRoleMasterAgent : BaseAgent, IAdminRoleMasterAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IAdminRoleMasterClient _adminRoleMasterClient;
        #endregion

        #region Public Constructor
        public AdminRoleMasterAgent(ICoditechLogging coditechLogging, IAdminRoleMasterClient adminRoleMasterClient)
        {
            _coditechLogging = coditechLogging;
            _adminRoleMasterClient = GetClient<IAdminRoleMasterClient>(adminRoleMasterClient);
        }
        #endregion

        #region Public Methods

        public virtual AdminRoleListViewModel GetAdminRoleList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("AdminRoleCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("SanctionPostName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            filters.Add(FilterKeys.SelectedCentreCode, ProcedureFilterOperators.Equals, dataTableModel.SelectedCentreCode);
            filters.Add(FilterKeys.SelectedDepartmentId, ProcedureFilterOperators.Equals, Convert.ToString(dataTableModel.SelectedDepartmentId));

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "SanctionPostName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            AdminRoleListResponse response = _adminRoleMasterClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            AdminRoleListModel departmentList = new AdminRoleListModel { AdminRoleList = response?.AdminRoleList };
            AdminRoleListViewModel listViewModel = new AdminRoleListViewModel();
            listViewModel.AdminRoleList = departmentList?.AdminRoleList?.ToViewModel<AdminRoleViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.AdminRoleList.Count, BindColumns());
            return listViewModel;
        }

        //Get general AdminRoleMaster by general department master id.
        public virtual AdminRoleViewModel GetAdminRoleDetailsById(int adminRoleMasterId)
        {
            AdminRoleResponse response = _adminRoleMasterClient.GetAdminRoleDetailsById(adminRoleMasterId);
            return response?.AdminRoleModel.ToViewModel<AdminRoleViewModel>();
        }

        //Update adminRoleMaster.
        public virtual AdminRoleViewModel UpdateAdminRole(AdminRoleViewModel adminRoleMasterViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Info);
                AdminRoleResponse response = _adminRoleMasterClient.UpdateAdminRole(adminRoleMasterViewModel.ToModel<AdminRoleModel>());
                AdminRoleModel adminRoleMasterModel = response?.AdminRoleModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Info);
                return IsNotNull(adminRoleMasterModel) ? adminRoleMasterModel.ToViewModel<AdminRoleViewModel>() : (AdminRoleViewModel)GetViewModelWithErrorMessage(new AdminRoleViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return (AdminRoleViewModel)GetViewModelWithErrorMessage(adminRoleMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete adminRoleMaster.
        public virtual bool DeleteAdminRole(string adminRoleMasterId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _adminRoleMasterClient.DeleteAdminRole(new ParameterModel { Ids = adminRoleMasterId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteAdminRoleMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }


        //Get Admin Role Menu Details By AadminRoleMasterId
        public virtual AdminRoleMenuDetailsViewModel GetAdminRoleMenuDetailsById(int adminRoleMasterId, string moduleCode)
        {
            AdminRoleMenuDetailsResponse response = _adminRoleMasterClient.GetAdminRoleMenuDetailsById(adminRoleMasterId, moduleCode);
            return response?.AdminRoleMenuDetailsModel.ToViewModel<AdminRoleMenuDetailsViewModel>();
        }

        //Insert Update Admin Role Menu Details
        public virtual AdminRoleMenuDetailsViewModel InsertUpdateAdminRoleMenuDetails(AdminRoleMenuDetailsViewModel adminRoleMenuDetailsViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Info);
                AdminRoleMenuDetailsResponse response = _adminRoleMasterClient.InsertUpdateAdminRoleMenuDetails(adminRoleMenuDetailsViewModel.ToModel<AdminRoleMenuDetailsModel>());
                AdminRoleMenuDetailsModel adminRoleMenuDetailsModel = response?.AdminRoleMenuDetailsModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Info);
                return IsNotNull(adminRoleMenuDetailsModel) ? adminRoleMenuDetailsModel.ToViewModel<AdminRoleMenuDetailsViewModel>() : (AdminRoleMenuDetailsViewModel)GetViewModelWithErrorMessage(new AdminRoleMenuDetailsViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return (AdminRoleMenuDetailsViewModel)GetViewModelWithErrorMessage(adminRoleMenuDetailsViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        public virtual AdminRoleApplicableDetailsListViewModel RoleAllocatedToUserList(int adminRoleMasterId, DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("FirstName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("LastName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("EmailId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("PersonCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "FirstName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            AdminRoleApplicableDetailsListResponse response = _adminRoleMasterClient.RoleAllocatedToUserList(adminRoleMasterId, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            AdminRoleApplicableDetailsListModel adminRoleApplicableDetailsList = response?.AdminRoleApplicableDetailsList;
            AdminRoleApplicableDetailsListViewModel listViewModel = new AdminRoleApplicableDetailsListViewModel();
            listViewModel.AdminRoleApplicableDetailsList = adminRoleApplicableDetailsList?.AdminRoleApplicableDetailsList?.ToViewModel<AdminRoleApplicableDetailsViewModel>()?.ToList();
            listViewModel.AdminRoleCode = response?.AdminRoleApplicableDetailsList?.AdminRoleCode;
            listViewModel.SanctionPostName = response?.AdminRoleApplicableDetailsList?.SanctionPostName;
            listViewModel.AdminRoleApplicableDetailsList = adminRoleApplicableDetailsList?.AdminRoleApplicableDetailsList?.ToViewModel<AdminRoleApplicableDetailsViewModel>()?.ToList();
            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.AdminRoleApplicableDetailsList.Count, BindRoleAllocatedToUserListColumns());
            return listViewModel;
        }

        //Get Associate UnAssociate Admin Role To User
        public virtual AdminRoleApplicableDetailsViewModel GetAssociateUnAssociateAdminRoleToUser(int adminRoleMasterId, int adminRoleApplicableDetailId)
        {
            AdminRoleApplicableDetailsResponse response = _adminRoleMasterClient.GetAssociateUnAssociateAdminRoleToUser(adminRoleMasterId, adminRoleApplicableDetailId);
            return response?.AdminRoleApplicableDetailsModel.ToViewModel<AdminRoleApplicableDetailsViewModel>();
        }

        //Associate UnAssociate Admin Role To User
        public virtual AdminRoleApplicableDetailsViewModel AssociateUnAssociateAdminRoleToUser(AdminRoleApplicableDetailsViewModel adminRoleApplicableDetailsViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Info);
                AdminRoleApplicableDetailsResponse response = _adminRoleMasterClient.AssociateUnAssociateAdminRoleToUser(adminRoleApplicableDetailsViewModel.ToModel<AdminRoleApplicableDetailsModel>());
                AdminRoleApplicableDetailsModel adminRoleApplicableDetailsModel = response?.AdminRoleApplicableDetailsModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Info);
                return IsNotNull(adminRoleApplicableDetailsModel) ? adminRoleApplicableDetailsModel.ToViewModel<AdminRoleApplicableDetailsViewModel>() : (AdminRoleApplicableDetailsViewModel)GetViewModelWithErrorMessage(new AdminRoleApplicableDetailsViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return (AdminRoleApplicableDetailsViewModel)GetViewModelWithErrorMessage(adminRoleApplicableDetailsViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Get Admin Role Wise Media Folder Action By Id
        public virtual AdminRoleMediaFolderActionViewModel GetAdminRoleWiseMediaFolderActionById(int adminRoleMasterId)
        {
            AdminRoleMediaFolderActionResponse response = _adminRoleMasterClient.GetAdminRoleWiseMediaFolderActionById(adminRoleMasterId);
            return response?.AdminRoleMediaFolderActionModel.ToViewModel<AdminRoleMediaFolderActionViewModel>();
        }

        //Update Admin RoleWise Media Folder Action.
        public virtual AdminRoleMediaFolderActionViewModel InsertUpdateAdminRoleWiseMediaFolderAction(AdminRoleMediaFolderActionViewModel adminRoleMediaFolderActionViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Info);
                AdminRoleMediaFolderActionResponse response = _adminRoleMasterClient.InsertUpdateAdminRoleWiseMediaFolderAction(adminRoleMediaFolderActionViewModel.ToModel<AdminRoleMediaFolderActionModel>());
                AdminRoleMediaFolderActionModel mediaSettingMasterModel = response?.AdminRoleMediaFolderActionModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Info);
                return IsNotNull(mediaSettingMasterModel) ? mediaSettingMasterModel.ToViewModel<AdminRoleMediaFolderActionViewModel>() : (AdminRoleMediaFolderActionViewModel)GetViewModelWithErrorMessage(new AdminRoleMediaFolderActionViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return (AdminRoleMediaFolderActionViewModel)GetViewModelWithErrorMessage(adminRoleMediaFolderActionViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Get Admin Role Wise Media Folders By Id
        public virtual AdminRoleMediaFoldersViewModel GetAdminRoleWiseMediaFoldersById(int adminRoleMasterId)
        {
            AdminRoleMediaFoldersResponse response = _adminRoleMasterClient.GetAdminRoleWiseMediaFoldersById(adminRoleMasterId);
            AdminRoleMediaFoldersViewModel adminRoleMediaFoldersViewModel = response?.AdminRoleMediaFoldersModel.ToViewModel<AdminRoleMediaFoldersViewModel>();
            adminRoleMediaFoldersViewModel.TreeViewJson = adminRoleMediaFoldersViewModel.TreeViewList?.Count > 0 ? Newtonsoft.Json.JsonConvert.SerializeObject(adminRoleMediaFoldersViewModel.TreeViewList) : string.Empty;
            return adminRoleMediaFoldersViewModel;
        }

        //Update Admin RoleWise Media Folders.
        public virtual AdminRoleMediaFoldersViewModel InsertUpdateAdminRoleWiseMediaFolders(AdminRoleMediaFoldersViewModel adminRoleMediaFoldersViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Info);
                AdminRoleMediaFoldersResponse response = _adminRoleMasterClient.InsertUpdateAdminRoleWiseMediaFolders(adminRoleMediaFoldersViewModel.ToModel<AdminRoleMediaFoldersModel>());
                AdminRoleMediaFoldersModel mediaSettingMasterModel = response?.AdminRoleMediaFoldersModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Info);
                return IsNotNull(mediaSettingMasterModel) ? mediaSettingMasterModel.ToViewModel<AdminRoleMediaFoldersViewModel>() : (AdminRoleMediaFoldersViewModel)GetViewModelWithErrorMessage(new AdminRoleMediaFoldersViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return (AdminRoleMediaFoldersViewModel)GetViewModelWithErrorMessage(adminRoleMediaFoldersViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Role Code",
                ColumnCode = "AdminRoleCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Sanction Post Name",
                ColumnCode = "SanctionPostName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Monitoring Level",
                ColumnCode = "MonitoringLevel",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Active",
                ColumnCode = "IsActive",
                IsSortable = true,
            });
            return datatableColumnList;
        }

        protected virtual List<DatatableColumns> BindRoleAllocatedToUserListColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "First Name",
                ColumnCode = "FirstName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Last Name",
                ColumnCode = "LastName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "PersonCode",
                ColumnCode = "Person Code",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Email Id",
                ColumnCode = "EmailId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Work To Date",
                ColumnCode = "WorkToDate",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Work From Date",
                ColumnCode = "WorkFromDate",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Active",
                ColumnCode = "IsActive",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}

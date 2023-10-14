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

        public AdminRoleListViewModel GetAdminRoleMasterList(DataTableViewModel dataTableModel)
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
            AdminRoleMasterListModel departmentList = new AdminRoleMasterListModel { AdminRoleMasterList = response?.AdminRoleMasterList };
            AdminRoleListViewModel listViewModel = new AdminRoleListViewModel();
            listViewModel.AdminRoleList = departmentList?.AdminRoleMasterList?.ToViewModel<AdminRoleViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.AdminRoleList.Count, BindColumns());
            return listViewModel;
        }

        ////Create AdminRoleMaster.
        //public virtual AdminRoleMasterViewModel CreateAdminRoleMaster(AdminRoleMasterViewModel adminRoleMasterViewModel)
        //{
        //    try
        //    {
        //        adminRoleMasterViewModel.CentreCode = SpiltCentreCode(adminRoleMasterViewModel.SelectedCentreCode);
        //        adminRoleMasterViewModel.DepartmentId = Convert.ToInt16(adminRoleMasterViewModel.SelectedDepartmentId);
        //        adminRoleMasterViewModel.IsActive = true;
        //        AdminRoleMasterResponse response = _adminRoleMasterClient.CreateAdminRoleMaster(adminRoleMasterViewModel.ToModel<AdminRoleMasterModel>());
        //        AdminRoleMasterModel adminRoleMasterModel = response?.AdminRoleMasterModel;
        //        return IsNotNull(adminRoleMasterModel) ? adminRoleMasterModel.ToViewModel<AdminRoleMasterViewModel>() : new AdminRoleMasterViewModel();
        //    }
        //    catch (CoditechException ex)
        //    {
        //        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
        //        switch (ex.ErrorCode)
        //        {
        //            case ErrorCodes.AlreadyExist:
        //                return (AdminRoleMasterViewModel)GetViewModelWithErrorMessage(adminRoleMasterViewModel, ex.ErrorMessage);
        //            default:
        //                return (AdminRoleMasterViewModel)GetViewModelWithErrorMessage(adminRoleMasterViewModel, GeneralResources.ErrorFailedToCreate);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
        //        return (AdminRoleMasterViewModel)GetViewModelWithErrorMessage(adminRoleMasterViewModel, GeneralResources.ErrorFailedToCreate);
        //    }
        //}

        ////Get general AdminRoleMaster by general department master id.
        //public virtual AdminRoleMasterViewModel GetAdminRoleMaster(int adminRoleMasterId)
        //{
        //    AdminRoleMasterResponse response = _adminRoleMasterClient.GetAdminRoleMaster(adminRoleMasterId);
        //    return response?.AdminRoleMasterModel.ToViewModel<AdminRoleMasterViewModel>();
        //}

        ////Update adminRoleMaster.
        //public virtual AdminRoleMasterViewModel UpdateAdminRoleMaster(AdminRoleMasterViewModel adminRoleMasterViewModel)
        //{
        //    try
        //    {
        //        _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Info);
        //        AdminRoleMasterResponse response = _adminRoleMasterClient.UpdateAdminRoleMaster(adminRoleMasterViewModel.ToModel<AdminRoleMasterModel>());
        //        AdminRoleMasterModel adminRoleMasterModel = response?.AdminRoleMasterModel;
        //        _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Info);
        //        return IsNotNull(adminRoleMasterModel) ? adminRoleMasterModel.ToViewModel<AdminRoleMasterViewModel>() : (AdminRoleMasterViewModel)GetViewModelWithErrorMessage(new AdminRoleMasterViewModel(), GeneralResources.UpdateErrorMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
        //        return (AdminRoleMasterViewModel)GetViewModelWithErrorMessage(adminRoleMasterViewModel, GeneralResources.UpdateErrorMessage);
        //    }
        //}

        ////Delete adminRoleMaster.
        //public virtual bool DeleteAdminRoleMaster(string adminRoleMasterId, out string errorMessage)
        //{
        //    errorMessage = GeneralResources.ErrorFailedToDelete;

        //    try
        //    {
        //        _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Info);
        //        TrueFalseResponse trueFalseResponse = _adminRoleMasterClient.DeleteAdminRoleMaster(new ParameterModel { Ids = adminRoleMasterId });
        //        return trueFalseResponse.IsSuccess;
        //    }
        //    catch (CoditechException ex)
        //    {
        //        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
        //        switch (ex.ErrorCode)
        //        {
        //            case ErrorCodes.AssociationDeleteError:
        //                errorMessage = AdminResources.ErrorDeleteAdminRoleMaster;
        //                return false;
        //            default:
        //                errorMessage = GeneralResources.ErrorFailedToDelete;
        //                return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
        //        errorMessage = GeneralResources.ErrorFailedToDelete;
        //        return false;
        //    }
        //}
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
        #endregion
    }
}

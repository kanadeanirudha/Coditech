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
    public class EmployeeServiceAgent : BaseAgent, IEmployeeServiceAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IEmployeeServiceClient _employeeServiceClient;
        private readonly IUserClient _userClient;
        #endregion

        #region Public Constructor
        public EmployeeServiceAgent(ICoditechLogging coditechLogging, IEmployeeServiceClient employeeServiceClient, IUserClient userClient)
        {
            _coditechLogging = coditechLogging;
            _employeeServiceClient = GetClient<IEmployeeServiceClient>(employeeServiceClient);
            _userClient = GetClient<IUserClient>(userClient);
        }
        #endregion

        #region Public Methods
        #region Employee
        public virtual EmployeeServiceListViewModel GetEmployeeServiceList(DataTableViewModel dataTableModel,int employeeId)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("EmployeeId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("EmployeeCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            filters.Add(FilterKeys.EmployeeId, ProcedureFilterOperators.Equals, Convert.ToString(employeeId));

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            EmployeeServiceListResponse response = _employeeServiceClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            EmployeeServiceListModel employeeServiceList = new EmployeeServiceListModel { EmployeeServiceList = response?.EmployeeServiceList };
            EmployeeServiceListViewModel listViewModel = new EmployeeServiceListViewModel();
            listViewModel.EmployeeServiceList = employeeServiceList?.EmployeeServiceList?.ToViewModel<EmployeeServiceViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.EmployeeServiceList.Count, BindColumns());
            return listViewModel;
        }


        //Get Employee Service
        public virtual EmployeeServiceViewModel GetEmployeeService(long employeeId)
        {
            EmployeeServiceResponse response = _employeeServiceClient.GetEmployeeService(employeeId);
            EmployeeServiceViewModel employeeServiceViewModel = response?.EmployeeServiceModel.ToViewModel<EmployeeServiceViewModel>();
            return employeeServiceViewModel;
        }

        //Update Employee Service
        public virtual EmployeeServiceViewModel UpdateEmployeeService(EmployeeServiceViewModel employeeServiceViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Info);
                EmployeeServiceResponse response = _employeeServiceClient.UpdateEmployeeService(employeeServiceViewModel.ToModel<EmployeeServiceModel>());
                EmployeeServiceModel employeeServiceModel = response?.EmployeeServiceModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Info);
                return IsNotNull(employeeServiceModel) ? employeeServiceModel.ToViewModel<EmployeeServiceViewModel>() : (EmployeeServiceViewModel)GetViewModelWithErrorMessage(new EmployeeServiceViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Error);
                return (EmployeeServiceViewModel)GetViewModelWithErrorMessage(employeeServiceViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete Employee.
        public virtual bool DeleteEmployee(string employeeIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _employeeServiceClient.DeleteEmployee(new ParameterModel { Ids = employeeIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteEmployeeDetails;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }

        #endregion
        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Current Position",
                ColumnCode = "IsCurrentPosition",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Employee Id",
                ColumnCode = "EmployeeId",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Employee Code",
                ColumnCode = "EmployeeCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Employee Designation Master Id",
                ColumnCode = "EmployeeDesignationMasterId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Joining Date",
                ColumnCode = "JoiningDate",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Promotion Demotion Date",
                ColumnCode = "PromotionDemotionDate",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Employee Stage Enum Id",
                ColumnCode = "EmployeeStageEnumId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Date Of Leavingd",
                ColumnCode = "DateOfLeaving",
                IsSortable = true,
            });
            return datatableColumnList;
        }

    }
    #endregion
}


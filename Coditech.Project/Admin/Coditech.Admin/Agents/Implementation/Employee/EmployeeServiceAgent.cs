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
        #endregion

        #region Public Constructor
        public EmployeeServiceAgent(ICoditechLogging coditechLogging, IEmployeeServiceClient employeeServiceClient)
        {
            _coditechLogging = coditechLogging;
            _employeeServiceClient = GetClient<IEmployeeServiceClient>(employeeServiceClient);
        }
        #endregion

        #region Public Methods
        public virtual EmployeeServiceListViewModel GetEmployeeServiceList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("Description", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ShortCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "Description" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            EmployeeServiceListResponse response = _employeeServiceClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            EmployeeServiceListModel serviceList = new EmployeeServiceListModel { EmployeeServiceList = response?.EmployeeServiceList };
            EmployeeServiceListViewModel listViewModel = new EmployeeServiceListViewModel();
            listViewModel.EmployeeServiceList = serviceList?.EmployeeServiceList?.ToViewModel<EmployeeServiceViewModel>().ToList();
            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.EmployeeServiceList.Count, BindColumns());
            return listViewModel;
        }

        //Create Employee Service.
        //public virtual EmployeeServiceViewModel CreateEmployeeService(EmployeeServiceViewModel employeeServiceViewModel)
        //{
        //    try
        //    {
        //        EmployeeServiceResponse response = _employeeServiceClient.CreateEmployeeService(employeeServiceViewModel.ToModel<EmployeeServiceModel>());
        //        EmployeeServiceModel employeeServiceModel = response?.EmployeeServiceModel;
        //        return IsNotNull(employeeServiceModel) ? employeeServiceModel.ToViewModel<EmployeeServiceViewModel>() : new EmployeeServiceViewModel();
        //    }
        //    catch (CoditechException ex)
        //    {
        //        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Warning);
        //        switch (ex.ErrorCode)
        //        {
        //            case ErrorCodes.AlreadyExist:
        //                return (EmployeeServiceViewModel)GetViewModelWithErrorMessage(employeeServiceViewModel, ex.ErrorMessage);
        //            default:
        //                return (EmployeeServiceViewModel)GetViewModelWithErrorMessage(employeeServiceViewModel, GeneralResources.ErrorFailedToCreate);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Error);
        //        return (EmployeeServiceViewModel)GetViewModelWithErrorMessage(employeeServiceViewModel, GeneralResources.ErrorFailedToCreate);
        //    }
        //}

        //Get general Service by general service master id.
        public virtual EmployeeServiceViewModel GetEmployeeService(long employeeServiceId)
        {
            EmployeeServiceResponse response = _employeeServiceClient.GetEmployeeService(employeeServiceId);
            return response?.EmployeeServiceModel.ToViewModel<EmployeeServiceViewModel>();
        }

        //Update Service.
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

        //Delete Service.
        public virtual bool DeleteEmployeeService(string employeeServiceId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _employeeServiceClient.DeleteEmployeeService(new ParameterModel { Ids = employeeServiceId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteEmployeeService;
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

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Service Name",
                ColumnCode = "Description",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Print Short Desc",
                ColumnCode = "ServiceLevel",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Grade",
                ColumnCode = "Grade",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Dept Short Code",
                ColumnCode = "ShortCode",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Active",
                ColumnCode = "IsActive",
            });
            return datatableColumnList;
        }
        #endregion
    }
}

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
    public class EmployeeMasterAgent : BaseAgent, IEmployeeMasterAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IEmployeeMasterClient _employeeMasterClient;        
        #endregion

        #region Public Constructor
        public EmployeeMasterAgent(ICoditechLogging coditechLogging, IEmployeeMasterClient employeeMasterClient)
        {
            _coditechLogging = coditechLogging;
            _employeeMasterClient = GetClient<IEmployeeMasterClient>(employeeMasterClient);            
        }
        #endregion

        #region Public Methods
        public virtual EmployeeMasterListViewModel GetEmployeeMasterList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("EmployeeId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("PersonCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);                
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "FirstName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            EmployeeMasterListResponse response = _employeeMasterClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            EmployeeMasterListModel employeeMasterList = new EmployeeMasterListModel { EmployeeMasterList = response?.EmployeeMasterList };
            EmployeeMasterListViewModel listViewModel = new EmployeeMasterListViewModel();
            listViewModel.EmployeeMasterList = employeeMasterList?.EmployeeMasterList?.ToViewModel<EmployeeMasterViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.EmployeeMasterList.Count, BindColumns());
            return listViewModel;
        }

        #region Employee
        //Create Employee
        public virtual EmployeeMasterViewModel CreateEmployee(EmployeeMasterViewModel employeeMasterViewModel)
        {
            try
            {

                EmployeeMasterResponse response = _employeeMasterClient.CreateEmployee(employeeMasterViewModel.ToModel<EmployeeMasterModel>());  
                EmployeeMasterModel employeeMasterModel = response?.EmployeeMasterModel;
                return IsNotNull(employeeMasterModel) ? employeeMasterModel.ToViewModel<EmployeeMasterViewModel>() : new EmployeeMasterViewModel();                                
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (EmployeeMasterViewModel)GetViewModelWithErrorMessage(employeeMasterViewModel, ex.ErrorMessage);
                    default:
                        return (EmployeeMasterViewModel)GetViewModelWithErrorMessage(employeeMasterViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
                return (EmployeeMasterViewModel)GetViewModelWithErrorMessage(employeeMasterViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get Employee Details by employeeId.
        public virtual EmployeeMasterViewModel GetEmployeeDetails(int employeeId)
        {
            EmployeeMasterResponse response = _employeeMasterClient.GetEmployeeDetails(employeeId);
            return response?.EmployeeMasterModel.ToViewModel<EmployeeMasterViewModel>();
        }

        //Update Employee Details
        public virtual EmployeeMasterViewModel UpdateEmployeeDetails(EmployeeMasterViewModel employeeMasterViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Info);
                EmployeeMasterResponse response = _employeeMasterClient.UpdateEmployeeDetails(employeeMasterViewModel.ToModel<EmployeeMasterModel>());
                EmployeeMasterModel employeeMasterModel = response?.EmployeeMasterModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Info);
                return IsNotNull(employeeMasterModel) ? employeeMasterModel.ToViewModel<EmployeeMasterViewModel>() : (EmployeeMasterViewModel)GetViewModelWithErrorMessage(new EmployeeMasterViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
                return (EmployeeMasterViewModel)GetViewModelWithErrorMessage(employeeMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
        #endregion

        //#region Employee Other Details
        ////Get Member Other Details
        //public virtual EmployeeMasterViewModel GetEmployeeDetails(int employeeId) a
        //{
        //    EmployeeMasterResponse response = _employeeMasterClient.GetEmployeeDetails(employeeId);
        //    EmployeeMasterViewModel employeeMasterViewModel = response?.EmployeeMasterModel.ToViewModel<EmployeeMasterViewModel>();
        //    return employeeMasterViewModel;
        //}

        //Update EmployeeMaster Details.
        //public virtual EmployeeMasterViewModel UpdateEmployeeDetails(EmployeeMasterViewModel employeeMasterViewModel)
        //{
        //    try
        //    {
        //        _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Info);
        //        EmployeeMasterResponse response = _employeeMasterClient.UpdateEmployeeDetails(employeeMasterViewModel.ToModel<EmployeeMasterModel>());
        //        EmployeeMasterModel employeeMasterModel = response?.EmployeeMasterModel;
        //        _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Info);
        //        return IsNotNull(employeeMasterModel) ? employeeMasterModel.ToViewModel<EmployeeMasterViewModel>() : (EmployeeMasterViewModel)GetViewModelWithErrorMessage(new EmployeeMasterViewModel(), GeneralResources.UpdateErrorMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
        //        return (EmployeeMasterViewModel)GetViewModelWithErrorMessage(employeeMasterViewModel, GeneralResources.UpdateErrorMessage);
        //    }
        //}
        //Delete gym Member Details.
        public virtual bool DeleteEmployee(string employeeIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _employeeMasterClient.DeleteEmployee(new ParameterModel { Ids = employeeIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Warning);
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
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "Image",
                ColumnCode = "Image",
            });
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
                ColumnName = "Contact",
                ColumnCode = "MobileNumber",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Email Id",
                ColumnCode = "EmailId",
                IsSortable = true,
            });
            return datatableColumnList;
        }
       
    }
}
#endregion
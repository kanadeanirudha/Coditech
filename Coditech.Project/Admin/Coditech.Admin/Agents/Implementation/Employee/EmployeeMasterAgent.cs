using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.API.Model.Responses.EmployeeMaster;
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
        private readonly IUserClient _userClient;
        #endregion

        #region Public Constructor
        public EmployeeMasterAgent(ICoditechLogging coditechLogging, IEmployeeMasterClient employeeMasterClient, IUserClient userClient)
        {
            _coditechLogging = coditechLogging;
            _employeeMasterClient = GetClient<IEmployeeMasterClient>(employeeMasterClient);
            _userClient = GetClient<IUserClient>(userClient);
        }
        #endregion

        #region Public Methods
        #region Employee
        public virtual EmployeeMasterListViewModel GetEmployeeMasterList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("EmployeeId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("PersonCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            filters.Add(FilterKeys.SelectedCentreCode, ProcedureFilterOperators.Equals, dataTableModel.SelectedCentreCode);
            filters.Add(FilterKeys.SelectedDepartmentId, ProcedureFilterOperators.Equals, Convert.ToString(dataTableModel.SelectedDepartmentId));

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "FirstName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            EmployeeMasterListResponse response = _employeeMasterClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            EmployeeMasterListModel employeeMasterList = new EmployeeMasterListModel { EmployeeMasterList = response?.EmployeeMasterList };
            EmployeeMasterListViewModel listViewModel = new EmployeeMasterListViewModel();
            listViewModel.EmployeeMasterList = employeeMasterList?.EmployeeMasterList?.ToViewModel<EmployeeMasterViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.EmployeeMasterList.Count, BindColumns());
            return listViewModel;
        }
       
        //Create Employee
        public virtual EmployeeCreateEditViewModel CreateEmployee(EmployeeCreateEditViewModel employeeCreateEditViewModel)
        {
            try
            {
                employeeCreateEditViewModel.UserType = UserTypeEnum.Employee.ToString();
                GeneralPersonResponse response = _userClient.InsertPersonInformation(employeeCreateEditViewModel.ToModel<GeneralPersonModel>());
                GeneralPersonModel generalPersonModel = response?.GeneralPersonModel;
                return IsNotNull(generalPersonModel) ? generalPersonModel.ToViewModel<EmployeeCreateEditViewModel>() : new EmployeeCreateEditViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (EmployeeCreateEditViewModel)GetViewModelWithErrorMessage(employeeCreateEditViewModel, ex.ErrorMessage);
                    default:
                        return (EmployeeCreateEditViewModel)GetViewModelWithErrorMessage(employeeCreateEditViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
                return (EmployeeCreateEditViewModel)GetViewModelWithErrorMessage(employeeCreateEditViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get Employee Details by personId.
        public virtual EmployeeCreateEditViewModel GetEmployeePersonalDetails(long personId)
        {
            GeneralPersonResponse response = _userClient.GetPersonInformation(personId);
            return response?.GeneralPersonModel.ToViewModel<EmployeeCreateEditViewModel>();
        }

        //Update Employee Personal Details
        public virtual EmployeeCreateEditViewModel UpdateEmployeePersonalDetails(EmployeeCreateEditViewModel employeeCreateEditViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                GeneralPersonResponse response = _userClient.UpdatePersonInformation(employeeCreateEditViewModel.ToModel<GeneralPersonModel>());
                GeneralPersonModel generalPersonModel = response?.GeneralPersonModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                return IsNotNull(generalPersonModel) ? generalPersonModel.ToViewModel<EmployeeCreateEditViewModel>() : (EmployeeCreateEditViewModel)GetViewModelWithErrorMessage(new EmployeeCreateEditViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return (EmployeeCreateEditViewModel)GetViewModelWithErrorMessage(employeeCreateEditViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Get Member Other Details
        public virtual EmployeeMasterViewModel GetEmployeeOtherDetail(long employeeId)
        {
            EmployeeMasterResponse response = _employeeMasterClient.GetEmployeeOtherDetail(employeeId);
            EmployeeMasterViewModel employeeMasterViewModel = response?.EmployeeMasterModel.ToViewModel<EmployeeMasterViewModel>();
            return employeeMasterViewModel;
        }

        //Update Gym Member Other Details.
        public virtual EmployeeMasterViewModel UpdateEmployeeOtherDetail(EmployeeMasterViewModel employeeMasterViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                EmployeeMasterResponse response = _employeeMasterClient.UpdateEmployeeOtherDetail(employeeMasterViewModel.ToModel<EmployeeMasterModel>());
                EmployeeMasterModel employeeMasterModel = response?.EmployeeMasterModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                return IsNotNull(employeeMasterModel) ? employeeMasterModel.ToViewModel<EmployeeMasterViewModel>() : (EmployeeMasterViewModel)GetViewModelWithErrorMessage(new EmployeeMasterViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return (EmployeeMasterViewModel)GetViewModelWithErrorMessage(employeeMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
       
        //Delete Employee.
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
                ColumnName = "Gender",
                ColumnCode = "Gender",
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
﻿using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.API.Data;
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
                filters.Add("FirstName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("LastName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("EmailId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MobileNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
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
                GeneralPersonModel generalPersonModel = employeeCreateEditViewModel.ToModel<GeneralPersonModel>();
                generalPersonModel.SelectedCentreCode = employeeCreateEditViewModel.SelectedCentreCode;
                generalPersonModel.SelectedDepartmentId = employeeCreateEditViewModel.SelectedDepartmentId;
                generalPersonModel.EmployeeDesignationMasterId = employeeCreateEditViewModel.EmployeeDesignationMasterId;

                GeneralPersonResponse response = _userClient.InsertPersonInformation(generalPersonModel);
                generalPersonModel = response?.GeneralPersonModel;
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
        public virtual EmployeeCreateEditViewModel GetEmployeePersonalDetails(long employeeId, long personId)
        {
            GeneralPersonResponse response = _userClient.GetPersonInformation(personId);
            EmployeeCreateEditViewModel employeeCreateEditViewModel = response?.GeneralPersonModel.ToViewModel<EmployeeCreateEditViewModel>();
            if (IsNotNull(employeeCreateEditViewModel))
            {
                EmployeeMasterResponse employeeMasterResponse = _employeeMasterClient.GetEmployeeOtherDetail(employeeId);
                if (IsNotNull(employeeMasterResponse))
                {
                    employeeCreateEditViewModel.SelectedCentreCode = employeeMasterResponse.EmployeeMasterModel.CentreCode;
                    employeeCreateEditViewModel.SelectedDepartmentId = Convert.ToString(employeeMasterResponse.EmployeeMasterModel.GeneralDepartmentMasterId);
                    employeeCreateEditViewModel.EmployeeDesignationMasterId = employeeMasterResponse.EmployeeMasterModel.EmployeeDesignationMasterId;
                    employeeCreateEditViewModel.IsActive = employeeMasterResponse.EmployeeMasterModel.IsActive;
                }
                employeeCreateEditViewModel.EmployeeId = employeeId;
                employeeCreateEditViewModel.PersonId = personId;
            }
            return employeeCreateEditViewModel;
        }

        //Update Employee Personal Details
        public virtual EmployeeCreateEditViewModel UpdateEmployeePersonalDetails(EmployeeCreateEditViewModel employeeCreateEditViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Info);
                GeneralPersonModel generalPersonModel = employeeCreateEditViewModel.ToModel<GeneralPersonModel>();
                generalPersonModel.EntityId = employeeCreateEditViewModel.EmployeeId;
                generalPersonModel.UserType = UserTypeEnum.Employee.ToString();
                GeneralPersonResponse response = _userClient.UpdatePersonInformation(generalPersonModel);
                generalPersonModel = response?.GeneralPersonModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalPersonModel) ? generalPersonModel.ToViewModel<EmployeeCreateEditViewModel>() : (EmployeeCreateEditViewModel)GetViewModelWithErrorMessage(new EmployeeCreateEditViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
                return (EmployeeCreateEditViewModel)GetViewModelWithErrorMessage(employeeCreateEditViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Get Employee Other Details
        public virtual EmployeeMasterViewModel GetEmployeeOtherDetail(long employeeId)
        {
            EmployeeMasterResponse response = _employeeMasterClient.GetEmployeeOtherDetail(employeeId);
            EmployeeMasterViewModel employeeMasterViewModel = response?.EmployeeMasterModel.ToViewModel<EmployeeMasterViewModel>();
            return employeeMasterViewModel;
        }

        //Update Employee Other Details.
        public virtual EmployeeMasterViewModel UpdateEmployeeOtherDetail(EmployeeMasterViewModel employeeMasterViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Info);
                EmployeeMasterResponse response = _employeeMasterClient.UpdateEmployeeOtherDetail(employeeMasterViewModel.ToModel<EmployeeMasterModel>());
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
                ColumnName = "Employee Code",
                ColumnCode = "PersonCode",
                IsSortable = true,
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
                ColumnCode = "GenderEnumId",
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
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Employee Role",
                ColumnCode = "SanctionPostName",
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

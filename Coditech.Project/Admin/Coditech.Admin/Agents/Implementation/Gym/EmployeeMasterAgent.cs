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
        public virtual EmployeeMasterListViewModel GetEmployeeMasterList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("FirstName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("LastName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("EmailId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MobileNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "FirstName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            EmployeeMasterListResponse response = _employeeMasterClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            EmployeeMasterListModel employeeMasterList = new EmployeeMasterListModel { EmployeeMasterList = response?.EmployeeMasterList };
            EmployeeMasterListViewModel listViewModel = new EmployeeMasterListViewModel();
            listViewModel.EmployeeMasterList = employeeMasterList?.EmployeeMasterList?.ToViewModel<EmployeeMasterViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.EmployeeMasterList.Count, BindColumns());
            return listViewModel;
        }

        #region General Person
        //Create Employee
        public virtual EmployeeCreateEditViewModel CreateEmployee(EmployeeCreateEditViewModel employeeCreateEditViewModel)
        {
            try
            {
                employeeCreateEditViewModel.UserType = UserTypeEnum.GymMember.ToString();
                GeneralPersonResponse response = _userClient.InsertPersonInformation(employeeCreateEditViewModel.ToModel<GeneralPersonModel>());
                GeneralPersonModel generalPersonModel = response?.GeneralPersonModel;
                return IsNotNull(generalPersonModel) ? generalPersonModel.ToViewModel<EmployeeCreateEditViewModel>() : new EmployeeCreateEditViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Warning);
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
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return (EmployeeCreateEditViewModel)GetViewModelWithErrorMessage(employeeCreateEditViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get Employee Personal Details by personId.
        public virtual EmployeeCreateEditViewModel GetEmployeePersonalDetails(long personId)
        {
            GeneralPersonResponse response = _userClient.GetPersonInformation(personId);
            return response?.GeneralPersonModel.ToViewModel<EmployeeCreateEditViewModel>();
        }

        //Update Employee Details
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
        #endregion

        #region Employee Other Details
        //Get Member Other Details
        public virtual EmployeeMasterViewModel GetEmployeeOtherDetails(int employeeId)
        {
            EmployeeMasterResponse response = _employeeMasterClient.GetEmployeeOtherDetails(employeeId);
            EmployeeMasterViewModel gymMemberDetailsViewModel = response?.EmployeeMasterModel.ToViewModel<EmployeeMasterViewModel>();
            return gymMemberDetailsViewModel;
        }

        //Update Gym Member Other Details.
        public virtual EmployeeMasterViewModel UpdateEmployeeOtherDetails(EmployeeMasterViewModel gymMemberDetailsViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                EmployeeMasterResponse response = _employeeMasterClient.UpdateEmployeeOtherDetails(gymMemberDetailsViewModel.ToModel<EmployeeMasterModel>());
                EmployeeMasterModel gymMemberDetailsModel = response?.EmployeeMasterModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                return IsNotNull(gymMemberDetailsModel) ? gymMemberDetailsModel.ToViewModel<EmployeeMasterViewModel>() : (EmployeeMasterViewModel)GetViewModelWithErrorMessage(new EmployeeMasterViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return (EmployeeMasterViewModel)GetViewModelWithErrorMessage(gymMemberDetailsViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
        //Delete gym Member Details.
        public virtual bool DeleteEmployee(string employeeIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _employeeMasterClient.DeleteEmployee(new ParameterModel { Ids = employeeIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGymMemberDetails;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }
        #endregion

        //#region Member Follow Up
        //public virtual GymMemberFollowUpListViewModel GymMemberFollowUpList(int employeeId, long personId, DataTableViewModel dataTableModel)
        //{
        //    FilterCollection filters = null;
        //    dataTableModel = dataTableModel ?? new DataTableViewModel();
        //    if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
        //    {
        //        filters = new FilterCollection();
        //        filters.Add("FollowupType", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
        //        filters.Add("FollowupComment", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
        //    }

        //    SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

        //    GymMemberFollowUpListResponse response = _employeeMasterClient.GymMemberFollowUpList(employeeId, personId, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
        //    GymMemberFollowUpListModel employeeMasterList = new GymMemberFollowUpListModel { GymMemberFollowUpList = response?.GymMemberFollowUpList };

        //    GymMemberFollowUpListViewModel listViewModel = new GymMemberFollowUpListViewModel()
        //    {
        //        PersonId = response.PersonId,
        //        GymMemberDetailId = response.GymMemberDetailId,
        //        FirstName = response?.FirstName,
        //        LastName = response?.LastName
        //    };
        //    listViewModel.GymMemberFollowUpList = employeeMasterList?.GymMemberFollowUpList?.ToViewModel<GymMemberFollowUpViewModel>().ToList();

        //    SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GymMemberFollowUpList.Count, BindGymMemberFollowUpColumns());
        //    return listViewModel;
        //}

        //#endregion
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

        //protected virtual List<DatatableColumns> BindGymMemberFollowUpColumns()
        //{
        //    List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
        //    datatableColumnList.Add(new DatatableColumns()
        //    {
        //        ColumnName = "Comments",
        //        ColumnCode = "FollowupComment",
        //    });
        //    datatableColumnList.Add(new DatatableColumns()
        //    {
        //        ColumnName = "Follow-Up Type",
        //        ColumnCode = "FollowupType",
        //        IsSortable = true,
        //    });
        //    datatableColumnList.Add(new DatatableColumns()
        //    {
        //        ColumnName = "Set Reminder",
        //        ColumnCode = "IsSetReminder",
        //        IsSortable = true,
        //    });
        //    datatableColumnList.Add(new DatatableColumns()
        //    {
        //        ColumnName = "Reminder Date",
        //        ColumnCode = "ReminderDate",
        //    });
        //    return datatableColumnList;
        //}
        //#endregion
    }
}

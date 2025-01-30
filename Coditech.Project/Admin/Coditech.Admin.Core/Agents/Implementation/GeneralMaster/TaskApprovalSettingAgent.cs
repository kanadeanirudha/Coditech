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
using Coditech.API.Data;

namespace Coditech.Admin.Agents
{
    public class TaskApprovalSettingAgent : BaseAgent, ITaskApprovalSettingAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ITaskApprovalSettingClient _taskApprovalSettingClient;
        private readonly IEmployeeMasterClient _employeeMasterClient;
        #endregion

        #region Public Constructor
        public TaskApprovalSettingAgent(ICoditechLogging coditechLogging, ITaskApprovalSettingClient taskApprovalSettingClient, IEmployeeMasterClient employeeMasterClient)
        {
            _coditechLogging = coditechLogging;
            _taskApprovalSettingClient = GetClient<ITaskApprovalSettingClient> (taskApprovalSettingClient);
            _employeeMasterClient = GetClient<IEmployeeMasterClient>(employeeMasterClient);
        }
        #endregion

        #region Public Methods
        public virtual TaskApprovalSettingListViewModel GetTaskApprovalSettingList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("TaskCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("TaskDescription", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            TaskApprovalSettingListResponse response = _taskApprovalSettingClient.List(dataTableModel.SelectedCentreCode, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            TaskApprovalSettingListModel taskApprovalSettingList = new TaskApprovalSettingListModel { TaskApprovalSettingList = response?.TaskApprovalSettingList };
            TaskApprovalSettingListViewModel listViewModel = new TaskApprovalSettingListViewModel();
            listViewModel.TaskApprovalSettingList = taskApprovalSettingList?.TaskApprovalSettingList?.ToViewModel<TaskApprovalSettingViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.TaskApprovalSettingList.Count, BindColumns());
            return listViewModel;
        }

        //Get taskMaster by taskMasterId.
        public virtual TaskApprovalSettingViewModel GetTaskApprovalSetting(short taskMasterId, string centreCode)
        {
            TaskApprovalSettingResponse response = _taskApprovalSettingClient.GetTaskApprovalSetting(taskMasterId, centreCode);
            return response?.TaskApprovalSettingModel.ToViewModel<TaskApprovalSettingViewModel>();
        }

        //Get Employee List By Centre Code
        public virtual List<EmployeeMasterModel> GetEmployeeListByCentreCode(string centreCode)
        {
            FilterCollection filters = new FilterCollection();
            EmployeeMasterListResponse response = _employeeMasterClient.ListByCentreCode(centreCode, null, null, null,1, int.MaxValue);            
            return response.EmployeeMasterList;
        }

        //AddEmployeeList
        public virtual TaskApprovalSettingViewModel AddUpdateTaskApprovalSetting(TaskApprovalSettingViewModel taskApprovalSettingViewModel)
        {
            try
            {
                TaskApprovalSettingResponse response = _taskApprovalSettingClient.AddUpdateTaskApprovalSetting(taskApprovalSettingViewModel.ToModel<TaskApprovalSettingModel>());
                TaskApprovalSettingModel taskApprovalSettingModel = response?.TaskApprovalSettingModel;
                return HelperUtility.IsNotNull(taskApprovalSettingModel) ? taskApprovalSettingModel.ToViewModel<TaskApprovalSettingViewModel>() : new TaskApprovalSettingViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskApprovalSetting.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (TaskApprovalSettingViewModel)GetViewModelWithErrorMessage(taskApprovalSettingViewModel, ex.ErrorMessage);
                    default:
                        return (TaskApprovalSettingViewModel)GetViewModelWithErrorMessage(taskApprovalSettingViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskApprovalSetting.ToString(), TraceLevel.Error);
                return (TaskApprovalSettingViewModel)GetViewModelWithErrorMessage(taskApprovalSettingViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get taskApprovalSetting by taskApprovalSettingId.
        public virtual TaskApprovalSettingViewModel GetUpdateTaskApprovalSetting(short taskMasterId, string centreCode, int taskApprovalSettingId)
        {
            TaskApprovalSettingResponse response = _taskApprovalSettingClient.GetUpdateTaskApprovalSetting(taskMasterId, centreCode, taskApprovalSettingId);
            return response?.TaskApprovalSettingModel.ToViewModel<TaskApprovalSettingViewModel>();
        }

        //Update TaskApprovalSetting.
        public virtual TaskApprovalSettingViewModel UpdateTaskApprovalSetting(TaskApprovalSettingViewModel taskApprovalSettingViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Info);
                TaskApprovalSettingResponse response = _taskApprovalSettingClient.UpdateTaskApprovalSetting(taskApprovalSettingViewModel.ToModel<TaskApprovalSettingModel>());
                TaskApprovalSettingModel taskApprovalSettingModel = response?.TaskApprovalSettingModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.TaskApprovalSetting.ToString(), TraceLevel.Info);
                return HelperUtility.IsNotNull(taskApprovalSettingModel) ? taskApprovalSettingModel.ToViewModel<TaskApprovalSettingViewModel>() : (TaskApprovalSettingViewModel)GetViewModelWithErrorMessage(new TaskApprovalSettingViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskApprovalSetting.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (TaskApprovalSettingViewModel)GetViewModelWithErrorMessage(taskApprovalSettingViewModel, ex.ErrorMessage);
                    default:
                        return (TaskApprovalSettingViewModel)GetViewModelWithErrorMessage(taskApprovalSettingViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskApprovalSetting.ToString(), TraceLevel.Error);
                return (TaskApprovalSettingViewModel)GetViewModelWithErrorMessage(taskApprovalSettingViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
        //Delete TaskApprovalSetting.
        public virtual bool DeleteTaskApprovalSetting(string taskApprovalSettingId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.TaskApprovalSetting.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _taskApprovalSettingClient.DeleteTaskApprovalSetting(new ParameterModel { Ids = taskApprovalSettingId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskApprovalSetting.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteTaskApprovalSetting;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskApprovalSetting.ToString(), TraceLevel.Error);
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
                ColumnName = "Task Code",
                ColumnCode = "TaskCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Task Description",
                ColumnCode = "TaskDescription",
                IsSortable = true,
            });
            return datatableColumnList;
        }

        #endregion
        
    }
}

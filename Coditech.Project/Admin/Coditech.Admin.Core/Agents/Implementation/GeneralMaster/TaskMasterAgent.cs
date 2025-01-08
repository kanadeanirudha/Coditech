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
    public class TaskMasterAgent : BaseAgent, ITaskMasterAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ITaskMasterClient _taskMasterClient;
        #endregion

        #region Public Constructor
        public TaskMasterAgent(ICoditechLogging coditechLogging, ITaskMasterClient taskMasterClient)
        {
            _coditechLogging = coditechLogging;
            _taskMasterClient = GetClient<ITaskMasterClient>(taskMasterClient);
        }
        #endregion

        #region Public Methods
        public virtual TaskMasterListViewModel GetTaskMasterList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("TaskCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("TaskDescription", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "TaskCode" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            TaskMasterListResponse response = _taskMasterClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            TaskMasterListModel taskMasterList = new TaskMasterListModel { TaskMasterList = response?.TaskMasterList };
            TaskMasterListViewModel listViewModel = new TaskMasterListViewModel();
            listViewModel.TaskMasterList = taskMasterList?.TaskMasterList?.ToViewModel<TaskMasterViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.TaskMasterList.Count, BindColumns());
            return listViewModel;
        }
        //Create General TaskMaster.
        public virtual TaskMasterViewModel CreateTaskMaster(TaskMasterViewModel taskMasterViewModel)
        {
            try
            {
                TaskMasterResponse response = _taskMasterClient.CreateTaskMaster(taskMasterViewModel.ToModel<TaskMasterModel>());
                TaskMasterModel taskMasterModel = response?.TaskMasterModel;
                return IsNotNull(taskMasterModel) ? taskMasterModel.ToViewModel<TaskMasterViewModel>() : new TaskMasterViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (TaskMasterViewModel)GetViewModelWithErrorMessage(taskMasterViewModel, ex.ErrorMessage);
                    default:
                        return (TaskMasterViewModel)GetViewModelWithErrorMessage(taskMasterViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Error);
                return (TaskMasterViewModel)GetViewModelWithErrorMessage(taskMasterViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get TaskMaster by TaskMaster id.
        public virtual TaskMasterViewModel GetTaskMaster(short taskMasterId)
        {
            TaskMasterResponse response = _taskMasterClient.GetTaskMaster(taskMasterId);
            return response?.TaskMasterModel.ToViewModel<TaskMasterViewModel>();
        }
        //Update TaskMaster.
        public virtual TaskMasterViewModel UpdateTaskMaster(TaskMasterViewModel taskMasterViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Info);
                TaskMasterResponse response = _taskMasterClient.UpdateTaskMaster(taskMasterViewModel.ToModel<TaskMasterModel>());
                TaskMasterModel taskMasterModel = response?.TaskMasterModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Info);
                return IsNotNull(taskMasterModel) ? taskMasterModel.ToViewModel<TaskMasterViewModel>() : (TaskMasterViewModel)GetViewModelWithErrorMessage(new TaskMasterViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (TaskMasterViewModel)GetViewModelWithErrorMessage(taskMasterViewModel, ex.ErrorMessage);
                    default:
                        return (TaskMasterViewModel)GetViewModelWithErrorMessage(taskMasterViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Error);
                return (TaskMasterViewModel)GetViewModelWithErrorMessage(taskMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
        //Delete TaskMaster.
        public virtual bool DeleteTaskMaster(string taskMasterId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _taskMasterClient.DeleteTaskMaster(new ParameterModel { Ids = taskMasterId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteTaskMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Error);
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
           
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Active",
                ColumnCode = "IsActive",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion

        #region
        // it will return get all TaskMaster list from database 
        public virtual TaskMasterListResponse GetTaskMasterList()
        {
            TaskMasterListResponse taskMasterList = _taskMasterClient.List(null, null, null, 1, int.MaxValue);
            return taskMasterList?.TaskMasterList?.Count > 0 ? taskMasterList : new TaskMasterListResponse();
        }
        #endregion
    }
}

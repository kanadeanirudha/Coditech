using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
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
    public class GeneralTaskSchedulerMasterAgent : BaseAgent, IGeneralTaskSchedulerMasterAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ITaskSchedulerClient _taskSchedulerClient;
        #endregion

        #region Public Constructor
        public GeneralTaskSchedulerMasterAgent(ICoditechLogging coditechLogging, ITaskSchedulerClient taskSchedulerClient)
        {
            _coditechLogging = coditechLogging;
            _taskSchedulerClient = GetClient<ITaskSchedulerClient>(taskSchedulerClient);
        }
        #endregion

        #region Public Methods

        #region TaskScheduler
        public virtual TaskSchedulerListViewModel GetTaskSchedulerList(DataTableViewModel dataTableModel)
        {
            //dataTableModel = dataTableModel ?? new DataTableViewModel();

            TaskSchedulerListResponse response = _taskSchedulerClient.List(null);
            TaskSchedulerListModel taskSchedulerList = new TaskSchedulerListModel { TaskSchedulerList = response?.TaskSchedulerList };
            TaskSchedulerListViewModel listViewModel = new TaskSchedulerListViewModel();
            List<GeneralEnumaratorModel> generalEnumaratorList = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.GeneralEnumaratorList;
            if (generalEnumaratorList == null)
            {
                generalEnumaratorList = new GeneralCommonClient().GetDropdownListByCode("SchedulerCallFor")?.GeneralEnumaratorList;
            }
            foreach (var item in generalEnumaratorList?.Where(x => x.EnumGroupCode == "SchedulerCallFor")?.OrderBy(y => y.SequenceNumber))
            {
                if (taskSchedulerList?.TaskSchedulerList?.Count > 0)
                {
                    TaskSchedulerModel taskSchedulerModel = taskSchedulerList.TaskSchedulerList.FirstOrDefault(x => x.SchedulerCallFor == item.EnumName);
                    if (taskSchedulerModel != null)
                    {
                        taskSchedulerModel.SchedulerCallForDisplayText = item.EnumDisplayText;
                        listViewModel.TaskSchedulerList.Add(taskSchedulerModel.ToViewModel<TaskSchedulerViewModel>());
                    }
                    else
                    {
                        listViewModel.TaskSchedulerList.Add(new TaskSchedulerViewModel()
                        {
                            SchedulerCallFor = item.EnumName,
                            SchedulerCallForDisplayText = item.EnumDisplayText
                        });
                    }
                }
                else
                {
                    listViewModel.TaskSchedulerList.Add(new TaskSchedulerViewModel()
                    {
                        SchedulerCallFor = item.EnumName,
                        SchedulerCallForDisplayText = item.EnumDisplayText
                    });
                }

            }
            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.TaskSchedulerList.Count, BindColumns());
            return listViewModel;
        }

        //Create TaskScheduler.
        public virtual TaskSchedulerViewModel CreateTaskScheduler(TaskSchedulerViewModel taskSchedulerViewModel)
        {
            try
            {
                taskSchedulerViewModel.SchedulerType = SchedulerTypeEnum.Scheduled.ToString();
                taskSchedulerViewModel.RecurEvery = 1;
                taskSchedulerViewModel.IsCronJob = true;
                TaskSchedulerResponse response = _taskSchedulerClient.CreateTaskScheduler(taskSchedulerViewModel.ToModel<TaskSchedulerModel>());
                TaskSchedulerModel taskSchedulerModel = response?.TaskSchedulerModel;
                return IsNotNull(taskSchedulerModel) ? taskSchedulerModel.ToViewModel<TaskSchedulerViewModel>() : new TaskSchedulerViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (TaskSchedulerViewModel)GetViewModelWithErrorMessage(taskSchedulerViewModel, ex.ErrorMessage);
                    default:
                        return (TaskSchedulerViewModel)GetViewModelWithErrorMessage(taskSchedulerViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Error);
                return (TaskSchedulerViewModel)GetViewModelWithErrorMessage(taskSchedulerViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get TaskScheduler by TaskSchedulerMasterId.
        public virtual TaskSchedulerViewModel GetTaskSchedulerDetails(int configuratorId, string schedulerCallFor)
        {
            TaskSchedulerResponse response = _taskSchedulerClient.GetTaskSchedulerDetails(configuratorId,schedulerCallFor);
            return response?.TaskSchedulerModel.ToViewModel<TaskSchedulerViewModel>();
        }

        //Update  Task Scheduler.
        public virtual TaskSchedulerViewModel UpdateTaskSchedulerDetails(TaskSchedulerViewModel taskSchedulerViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Info);
                taskSchedulerViewModel.IsCronJob = true;
                TaskSchedulerResponse response = _taskSchedulerClient.UpdateTaskSchedulerDetails(taskSchedulerViewModel.ToModel<TaskSchedulerModel>());
                TaskSchedulerModel taskSchedulerModel = response?.TaskSchedulerModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Info);
                return IsNotNull(taskSchedulerModel) ? taskSchedulerModel.ToViewModel<TaskSchedulerViewModel>() : (TaskSchedulerViewModel)GetViewModelWithErrorMessage(new TaskSchedulerViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Error);
                return (TaskSchedulerViewModel)GetViewModelWithErrorMessage(taskSchedulerViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete Task Scheduler.
        public virtual bool DeleteTaskScheduler(string taskSchedulerMasterId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _taskSchedulerClient.DeleteTaskScheduler(new ParameterModel { Ids = taskSchedulerMasterId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteTaskSchedulerDetails;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }

        //Get Execute Task Scheduler.
        public virtual TaskSchedulerViewModel GetExecuteTaskScheduler(DateTime startDate)
        {
            TaskSchedulerResponse response = _taskSchedulerClient.GetExecuteTaskScheduler(startDate);
            return response?.TaskSchedulerModel.ToViewModel<TaskSchedulerViewModel>();
        }
        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();    
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Scheduler Call For",
                ColumnCode = "SchedulerCallFor",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Scheduler Name",
                ColumnCode = "SchedulerName",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Scheduler Frequency",
                ColumnCode = "SchedulerFrequency",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Start Date",
                ColumnCode = "StartDate",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Expire Date",
                ColumnCode = "ExpireDate",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Active",
                ColumnCode = "IsEnabled",
            });
            return datatableColumnList;
        }
        #endregion

        #endregion
    }
}

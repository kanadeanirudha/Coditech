using Coditech.API.Data;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Hangfire;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System.Data;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class TaskSchedulerService : BaseService, ITaskSchedulerService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<TaskSchedulerMaster> _taskSchedulerRepository;
        protected readonly IERPJobs _eRPJob;
        public TaskSchedulerService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _eRPJob = _serviceProvider.GetService<IERPJobs>();
            _taskSchedulerRepository = new CoditechRepository<TaskSchedulerMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual TaskSchedulerListModel GetTaskSchedulerList()
        {
            TaskSchedulerListModel listModel = new TaskSchedulerListModel();
            var taskSchedulerEntities = _taskSchedulerRepository.Table.Where(entity => !entity.SchedulerName.Contains("Batch")).ToList();

            listModel.TaskSchedulerList = taskSchedulerEntities?.Select(entity => new TaskSchedulerModel
            {
                TaskSchedulerMasterId = entity.TaskSchedulerMasterId,
                SchedulerCallFor = entity.SchedulerCallFor,
                SchedulerName = entity.SchedulerName,
                SchedulerFrequency = entity.SchedulerFrequency,
                StartDate = entity.StartDate,
                ExpireDate = entity.ExpireDate,
                IsEnabled = entity.IsEnabled
            }).ToList() ?? new List<TaskSchedulerModel>();
            return listModel;
        }

        public virtual TaskSchedulerModel CreateTaskScheduler(TaskSchedulerModel taskSchedulerModel)
        {
            if (IsNull(taskSchedulerModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            BindSchedulerOtherDetails(taskSchedulerModel);
            TaskSchedulerMaster taskScheduler = taskSchedulerModel.FromModelToEntity<TaskSchedulerMaster>();
            taskScheduler.WeekDays = taskSchedulerModel.SchedulerFrequency == "Weekly" && taskSchedulerModel.SelectedWeekDays != null && taskSchedulerModel.SelectedWeekDays.Any() ? string.Join(",", taskSchedulerModel.SelectedWeekDays) : "";

            //Create new TaskScheduler and return it.
            TaskSchedulerMaster taskSchedulerData = _taskSchedulerRepository.Insert(taskScheduler);
            if (taskSchedulerData?.TaskSchedulerMasterId > 0)
            {
                taskSchedulerModel.TaskSchedulerMasterId = taskSchedulerData.TaskSchedulerMasterId;
                if (taskSchedulerModel.IsCronJob && taskSchedulerModel.IsEnabled)
                {
                    SetSchedulerParameters(taskSchedulerModel, ',');
                    string hangfireJobId = null;
                    bool schedulerStatus = true;
                    schedulerStatus = _eRPJob.ConfigureJobs(taskSchedulerModel, out hangfireJobId, _coditechLogging);
                    if (schedulerStatus)
                    {
                        taskSchedulerData.HangfireJobId = hangfireJobId;
                        _taskSchedulerRepository.Update(taskSchedulerData);
                    }
                }
                taskSchedulerModel.TaskSchedulerMasterId = taskSchedulerData.TaskSchedulerMasterId;
                taskSchedulerModel.WeekDays = taskSchedulerData.WeekDays ?? "";
                taskSchedulerModel.SelectedWeekDays = taskSchedulerData.WeekDays?.Split(',').ToList() ?? new List<string>();
            }
            else
            {
                taskSchedulerModel.HasError = true;
                taskSchedulerModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return taskSchedulerModel;
        }

        //Get TaskScheduler by taskScheduler id.
        public virtual TaskSchedulerModel GetTaskSchedulerDetails(int configuratorId, string schedulerCallFor)
        {
            if (configuratorId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "ConfiguratorId"));

            //Get the TaskScheduler Details based on id.
            TaskSchedulerMaster taskScheduler = _taskSchedulerRepository.Table.Where(x => x.ConfiguratorId == configuratorId && x.SchedulerCallFor == schedulerCallFor)?.FirstOrDefault();
            TaskSchedulerModel taskSchedulerModel = taskScheduler?.FromEntityToModel<TaskSchedulerModel>() ?? new TaskSchedulerModel();
            if (taskSchedulerModel.TaskSchedulerMasterId > 0)
            {
                if (taskSchedulerModel.StartDate.HasValue)
                {
                    taskSchedulerModel.StartTime = taskScheduler.StartDate.Value.TimeOfDay;
                }

                if (taskSchedulerModel.ExpireDate.HasValue)
                {
                    taskSchedulerModel.ExpireTime = taskScheduler.ExpireDate.Value.TimeOfDay;
                }
            }
            return taskSchedulerModel;

        }

        //Update TaskSchedulerDetails.
        public virtual bool UpdateTaskSchedulerDetails(TaskSchedulerModel taskSchedulerModel)
        {
            if (IsNull(taskSchedulerModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (taskSchedulerModel.TaskSchedulerMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaskSchedulerMasterId"));
            BindSchedulerOtherDetails(taskSchedulerModel);
            TaskSchedulerMaster taskScheduler = taskSchedulerModel.FromModelToEntity<TaskSchedulerMaster>();
            TaskSchedulerMaster taskSchedulerMaster = _taskSchedulerRepository.Table.Where(x => x.TaskSchedulerMasterId == taskSchedulerModel.TaskSchedulerMasterId)?.FirstOrDefault();

            taskScheduler.ConfiguratorId = taskSchedulerMaster.ConfiguratorId;
            taskScheduler.SchedulerName = taskSchedulerMaster.SchedulerName;
            taskScheduler.WeekDays = taskSchedulerModel.SchedulerFrequency == "Weekly" && taskSchedulerModel.SelectedWeekDays != null && taskSchedulerModel.SelectedWeekDays.Any() ? string.Join(",", taskSchedulerModel.SelectedWeekDays) : "";

            //Update TaskScheduler
            bool isTaskSchedulerUpdated = _taskSchedulerRepository.Update(taskScheduler);
            if (!isTaskSchedulerUpdated)
            {
                taskSchedulerModel.HasError = true;
                taskSchedulerModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            else if (taskSchedulerModel.IsCronJob)
            {
                if (!taskSchedulerModel.IsEnabled)
                {
                    if (_eRPJob.RemoveJob(taskSchedulerModel))
                    {
                        taskSchedulerMaster.HangfireJobId = null;
                        _taskSchedulerRepository.Update(taskSchedulerMaster);
                    }
                }
                else
                {
                    string hangfireJobId = string.Empty;
                    bool schedulerStatus = true;
                    SetSchedulerParameters(taskSchedulerModel, ',');
                    schedulerStatus = _eRPJob.ConfigureJobs(taskSchedulerModel, out hangfireJobId, _coditechLogging);
                    if (schedulerStatus)
                    {
                        if (!string.IsNullOrEmpty(taskSchedulerMaster.HangfireJobId))
                        {
                            taskSchedulerMaster.HangfireJobId = hangfireJobId;
                            _taskSchedulerRepository.Update(taskSchedulerMaster);
                        }
                    }
                    else if (!schedulerStatus)
                    {
                        taskSchedulerModel.HasError = true;
                        taskSchedulerModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
                    }
                }
            }
            return isTaskSchedulerUpdated;
        }

        //Delete TaskScheduler.
        public virtual bool DeleteTaskScheduler(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaskSchedulerMasterId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("TaskSchedulerMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteTaskScheduler @TaskSchedulerMasterId,  @Status OUT", 1, out status);
            if (status == 1)
            {
                int[] erpTaskSchedulerIdsArray = parameterModel.Ids.Split(',').Select(int.Parse).ToArray();
                List<TaskSchedulerModel> taskSchedulerModelList = _taskSchedulerRepository.Table.Where(item => erpTaskSchedulerIdsArray.Contains(item.TaskSchedulerMasterId) && item.ConfiguratorId == 0)?.Select(x => x.FromEntityToModel<TaskSchedulerModel>())?.ToList();
                if(taskSchedulerModelList?.Count > 0)
                {
                    _eRPJob.RemoveJob(taskSchedulerModelList);
                }
            }
            return status == 1 ? true : false;
        }

        #region Protected Method
        protected virtual void BindSchedulerOtherDetails(TaskSchedulerModel taskSchedulerModel)
        {
            if (taskSchedulerModel.SchedulerCallFor == SchedulerCallForEnum.Batch.ToString())
            {
                GeneralBatchMaster model = new CoditechRepository<GeneralBatchMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.GeneralBatchMasterId == taskSchedulerModel.ConfiguratorId)?.FirstOrDefault();
                taskSchedulerModel.SchedulerName = model.BatchName;
                taskSchedulerModel.StartDate = taskSchedulerModel.StartDate + model.BatchStartTime;
                taskSchedulerModel.ExpireDate = taskSchedulerModel.ExpireDate + taskSchedulerModel.ExpireTime;
            }
            else
            {
                taskSchedulerModel.StartDate = taskSchedulerModel.StartDate + taskSchedulerModel.StartTime;
            }
        }

        //Set Scheduler Parameters
        protected virtual void SetSchedulerParameters(TaskSchedulerModel erpTaskSchedulerModel, char separator = ' ')
        {
            switch (erpTaskSchedulerModel.SchedulerCallFor)
            {
                case APIConstant.DeleteLogMessage:
                    erpTaskSchedulerModel.ExeParameters = $"{BaseParameter(separator)}{erpTaskSchedulerModel.SchedulerCallFor}";
                    break;
            }
            _coditechLogging.LogMessage("ExeParameters value: ", CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Verbose, erpTaskSchedulerModel?.ExeParameters);
        }

        private string BaseParameter(char separator = ' ')
        {
            string apiDomainUrl = $"{HttpContextHelper.Current.Request.Scheme + "://"}{HttpContextHelper.Request.Headers[HeaderNames.Host]}";
            string authValue = HttpContextHelper.Request.Headers[APIConstant.Authorization].ToString()?.Replace("Basic ", "");
            long loginUserId = HelperMethods.GetLoginUserId();
            return $"{apiDomainUrl}{separator}{authValue}{separator}{ApiSettings.ApiRequestTimeout}{separator}{loginUserId}";
        }
        #endregion
    }
}

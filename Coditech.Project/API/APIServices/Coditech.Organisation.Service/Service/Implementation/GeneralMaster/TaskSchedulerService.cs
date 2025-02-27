using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class TaskSchedulerService : BaseService, ITaskSchedulerService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<TaskSchedulerMaster> _taskSchedulerRepository;
        public TaskSchedulerService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _taskSchedulerRepository = new CoditechRepository<TaskSchedulerMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual TaskSchedulerModel CreateBatchTaskScheduler(TaskSchedulerModel taskSchedulerModel)
        {
            if (IsNull(taskSchedulerModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            BindSchedulerOtherDetails(taskSchedulerModel);         
            TaskSchedulerMaster taskScheduler = taskSchedulerModel.FromModelToEntity<TaskSchedulerMaster>();
            taskScheduler.WeekDays = taskSchedulerModel.SchedulerFrequency == "Weekly" && taskSchedulerModel.SelectedWeekDays != null && taskSchedulerModel.SelectedWeekDays.Any()? string.Join(",", taskSchedulerModel.SelectedWeekDays): "";

            //Create new TaskScheduler and return it.
            TaskSchedulerMaster taskSchedulerData = _taskSchedulerRepository.Insert(taskScheduler);
            if (taskSchedulerData?.TaskSchedulerMasterId > 0)
            {
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
        public virtual TaskSchedulerModel GetBatchTaskSchedulerDetails(int configuratorId, string schedulerCallFor)
        {
            if (configuratorId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "ConfiguratorId"));

            //Get the TaskScheduler Details based on id.
            TaskSchedulerMaster taskScheduler = _taskSchedulerRepository.Table.Where(x =>  x.ConfiguratorId == configuratorId && x.SchedulerCallFor == schedulerCallFor)?.FirstOrDefault();
            TaskSchedulerModel taskSchedulerModel = taskScheduler?.FromEntityToModel<TaskSchedulerModel>() ?? new TaskSchedulerModel();
            return taskSchedulerModel;

        }

        //Update BatchTaskSchedulerDetails.
        public virtual bool UpdateBatchTaskSchedulerDetails(TaskSchedulerModel taskSchedulerModel)
        {
            if (IsNull(taskSchedulerModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (taskSchedulerModel.TaskSchedulerMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaskSchedulerMasterId"));

            TaskSchedulerMaster taskScheduler = taskSchedulerModel.FromModelToEntity<TaskSchedulerMaster>();
            TaskSchedulerMaster taskSchedulerMaster = _taskSchedulerRepository.Table.Where(x => x.TaskSchedulerMasterId == taskSchedulerModel.TaskSchedulerMasterId)?.FirstOrDefault();
            BindSchedulerOtherDetails(taskSchedulerModel);
            taskScheduler.ConfiguratorId = taskSchedulerMaster.ConfiguratorId;
            taskScheduler.SchedulerName = taskSchedulerMaster.SchedulerName;
            taskScheduler.SchedulerCallFor = taskSchedulerMaster.SchedulerCallFor;
            taskScheduler.SchedulerType = taskSchedulerMaster.SchedulerType;
            taskScheduler.RecurEvery = taskSchedulerMaster.RecurEvery;          
            taskScheduler.WeekDays = taskSchedulerModel.SchedulerFrequency == "Weekly" && taskSchedulerModel.SelectedWeekDays != null && taskSchedulerModel.SelectedWeekDays.Any()? string.Join(",", taskSchedulerModel.SelectedWeekDays): "";

            //Update TaskScheduler
            bool isBatchTaskSchedulerUpdated = _taskSchedulerRepository.Update(taskScheduler);
            if (!isBatchTaskSchedulerUpdated)
            {
                taskSchedulerModel.HasError = true;
                taskSchedulerModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isBatchTaskSchedulerUpdated;
        }



        ////Delete TaskScheduler.
        //public virtual bool DeleteBatchTaskScheduler(ParameterModel parameterModel)
        //{
        //    if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
        //        throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralBatchMasterId"));

        //    CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
        //    objStoredProc.SetParameter("GeneralBatchMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
        //    objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
        //    int status = 0;
        //    objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteGeneralBatch @GeneralBatchMasterId,  @Status OUT", 1, out status);

        //    return status == 1 ? true : false;
        //}
        

        #region Protected Method
        protected virtual void BindSchedulerOtherDetails(TaskSchedulerModel taskSchedulerModel)
        {
            if (taskSchedulerModel.SchedulerCallFor == SchedulerCallForEnum.Batch.ToString())
            {
                GeneralBatchMaster model = new CoditechRepository<GeneralBatchMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x=>x.GeneralBatchMasterId == taskSchedulerModel.ConfiguratorId)?.FirstOrDefault();
                taskSchedulerModel.SchedulerName = model.BatchName;
                taskSchedulerModel.StartDate = taskSchedulerModel.StartDate + model.BatchStartTime;
            }
        }
        #endregion
    }
}

using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class TaskMasterService : ITaskMasterService
    {
        protected readonly IServiceProvider serviceProvider;
        protected readonly ICoditechLogging coditechLogging;
        private readonly ICoditechRepository<TaskMaster> _taskMasterRepository;
        private IServiceProvider _serviceProvider;
        private readonly ICoditechLogging _coditechLogging;
        public TaskMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _taskMasterRepository = new CoditechRepository<TaskMaster>(_serviceProvider.GetService<Coditech_Entities>());

        }
        public virtual TaskMasterListModel GetTaskMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<TaskMasterModel> objStoredProc = new CoditechViewRepository<TaskMasterModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<TaskMasterModel> TaskMasterList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetTaskMasterList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            TaskMasterListModel listModel = new TaskMasterListModel();

            listModel.TaskMasterList = TaskMasterList?.Count > 0 ? TaskMasterList : new List<TaskMasterModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create TaskMaster.
        public virtual TaskMasterModel CreateTaskMaster(TaskMasterModel taskMasterModel)
        {
            if (IsNull(taskMasterModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsTaskCodeAlreadyExist(taskMasterModel.TaskCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Task Code"));

            TaskMaster taskMaster = taskMasterModel.FromModelToEntity<TaskMaster>();

            //Create new TaskMaster and return it.
            TaskMaster taskMasterData = _taskMasterRepository.Insert(taskMaster);
            if (taskMasterData?.TaskMasterId > 0)
            {
                taskMasterModel.TaskMasterId = taskMasterData.TaskMasterId;
            }
            else
            {
                taskMasterModel.HasError = true;
                taskMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return taskMasterModel;
        }
        //Get TaskMaster by TaskMaster id.
        public virtual TaskMasterModel GetTaskMaster(short taskMasterId)
        {
            if (taskMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaskMasterID"));

            //Get the TaskMaster Details based on id.
            TaskMaster taskMaster = _taskMasterRepository.Table.FirstOrDefault(x => x.TaskMasterId == taskMasterId);
            TaskMasterModel taskMasterModel = taskMaster?.FromEntityToModel<TaskMasterModel>();
            return taskMasterModel;
        }
        //Update TaskMaster.
        public virtual bool UpdateTaskMaster(TaskMasterModel taskMasterModel)
        {
            if (IsNull(taskMasterModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (taskMasterModel.TaskMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaskMasterID"));

            if (IsTaskCodeAlreadyExist(taskMasterModel.TaskCode, taskMasterModel.TaskMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Task Code"));

            TaskMaster taskMaster = taskMasterModel.FromModelToEntity<TaskMaster>();

            //Update TaskMaster
            bool isTaskMasterUpdated = _taskMasterRepository.Update(taskMaster);
            if (!isTaskMasterUpdated)
            {
                taskMasterModel.HasError = true;
                taskMasterModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isTaskMasterUpdated;
        }
        //Delete Country.
        public virtual bool DeleteTaskMaster(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaskMasterID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("TaskMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteTaskMaster @TaskMasterId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Country code is already present or not.
        protected virtual bool IsTaskCodeAlreadyExist(string taskCode, short taskMasterId = 0)
         => _taskMasterRepository.Table.Any(x => x.TaskCode == taskCode && (x.TaskMasterId != taskMasterId || taskMasterId == 0));
        #endregion
    }
}

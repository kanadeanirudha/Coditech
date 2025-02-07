using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class TaskApprovalSettingService : BaseService, ITaskApprovalSettingService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<TaskApprovalSetting> _taskApprovalSettingRepository;
        private readonly ICoditechRepository<TaskMaster> _taskMasterRepository;
        private readonly ICoditechRepository<EmployeeMaster> _employeeMasterRepository;
        private readonly ICoditechRepository<UserMaster> _userMasterRepository;

        public TaskApprovalSettingService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _taskApprovalSettingRepository = new CoditechRepository<TaskApprovalSetting>(_serviceProvider.GetService<Coditech_Entities>());
            _taskMasterRepository = new CoditechRepository<TaskMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _employeeMasterRepository = new CoditechRepository<EmployeeMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _userMasterRepository = new CoditechRepository<UserMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual TaskApprovalSettingListModel GetTaskApprovalSettingList(string selectedCentreCode, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<TaskApprovalSettingModel> objStoredProc = new CoditechViewRepository<TaskApprovalSettingModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<TaskApprovalSettingModel> taskApprovalSettingList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetTaskApprovalSettingList @CentreCode, @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            TaskApprovalSettingListModel listModel = new TaskApprovalSettingListModel();

            listModel.TaskApprovalSettingList = taskApprovalSettingList?.Count > 0 ? taskApprovalSettingList : new List<TaskApprovalSettingModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Get TaskApprovalSetting by taskApprovalSetting id.
        public virtual TaskApprovalSettingModel GetTaskApprovalSetting(short taskMasterId, string centreCode)
        {
            if (taskMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaskMasterId"));

            TaskApprovalSettingModel taskApprovalSettingModel = new TaskApprovalSettingModel();
            TaskMaster taskMaster = _taskMasterRepository.Table.Where(x => x.TaskMasterId == taskMasterId)?.FirstOrDefault();
            if (IsNotNull(taskMaster))
            {
                taskApprovalSettingModel.TaskCode = taskMaster.TaskCode;
                taskApprovalSettingModel.TaskDescription = taskMaster.TaskDescription;

            }
            if (!string.IsNullOrEmpty(centreCode))
            {
                taskApprovalSettingModel.CentreName = GetOrganisationCentreDetails(centreCode).CentreName;
                taskApprovalSettingModel.CentreCode = centreCode;
            }

            List<TaskApprovalSetting> taskApprovalSettings = _taskApprovalSettingRepository.Table
                                .Where(x => x.TaskMasterId == taskMasterId && x.CentreCode == centreCode)
                                .OrderBy(x => x.ApprovalSequenceNumber)
                                .ToList();

            TaskApprovalSetting taskApproval = taskApprovalSettings.FirstOrDefault();
            if (taskApproval != null)
            {
                taskApprovalSettingModel.TaskApprovalSettingId = taskApproval.TaskApprovalSettingId;
                taskApprovalSettingModel.ApprovalSequenceNumber = taskApproval.ApprovalSequenceNumber;
                taskApprovalSettingModel.IsFinalApproval = taskApproval.IsFinalApproval;
            }

            taskApprovalSettingModel.EmployeeList = new List<EmployeeMasterModel>();
            taskApprovalSettingModel.EmployeeList = (from t in taskApprovalSettings
                                                     join u in _userMasterRepository.Table
                                                     .Where(u => u.UserType == UserTypeEnum.Employee.ToString())
                                                     on t.EmployeeId equals u.EntityId
                                                     select new EmployeeMasterModel
                                                     {
                                                         EmployeeId = t.EmployeeId,
                                                         FirstName = u.FirstName,
                                                         LastName = u.LastName,
                                                         IsFinalApproval = t.IsFinalApproval
                                                     }).OrderBy(e => taskApprovalSettings
                                                 .FirstOrDefault(x => x.EmployeeId == e.EmployeeId)?.ApprovalSequenceNumber).ToList();


            return taskApprovalSettingModel;
        }

        //TaskApprovalSetting for EmployeeList.
        public virtual TaskApprovalSettingModel AddUpdateTaskApprovalSetting(TaskApprovalSettingModel taskApprovalSettingModel)
        {
            if (IsNull(taskApprovalSettingModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsTaskApprovalSettingAlreadyExist(taskApprovalSettingModel))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Task Approval Setting"));

            List<long> employeeIdList = taskApprovalSettingModel.EmployeeIds?.Split(',').Where(id => !string.IsNullOrWhiteSpace(id)).Select(long.Parse).ToList();

            List<TaskApprovalSetting> insertList = new List<TaskApprovalSetting>();
            for (int i = 1; i <= employeeIdList.Count; i++)
            {
                insertList.Add(new TaskApprovalSetting
                {
                    CentreCode = taskApprovalSettingModel.CentreCode,
                    TaskMasterId = taskApprovalSettingModel.TaskMasterId,
                    EmployeeId = employeeIdList[i - 1],
                    ApprovalSequenceNumber = (byte)i,
                    IsFinalApproval = (i == employeeIdList.Count)
                });
            }
            if (employeeIdList.Count > 0)
            {
                _taskApprovalSettingRepository.Insert(insertList);
            }
            return taskApprovalSettingModel;

        }

        //Get UpdateTaskApprovalSetting by taskApprovalSetting id.
        public virtual TaskApprovalSettingModel GetUpdateTaskApprovalSetting(short taskMasterId, string centreCode, int taskApprovalSettingId)
        {
            if (taskMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaskMasterId"));

            TaskApprovalSettingModel taskApprovalSettingModel = new TaskApprovalSettingModel();
            TaskMaster taskMaster = _taskMasterRepository.Table.Where(x => x.TaskMasterId == taskMasterId)?.FirstOrDefault();
            if (IsNotNull(taskMaster))
            {
                taskApprovalSettingModel.TaskCode = taskMaster.TaskCode;
                taskApprovalSettingModel.TaskDescription = taskMaster.TaskDescription;

            }
            if (!string.IsNullOrEmpty(centreCode))
            {
                taskApprovalSettingModel.CentreName = GetOrganisationCentreDetails(centreCode).CentreName;
                taskApprovalSettingModel.CentreCode = centreCode;
            }
            List<TaskApprovalSetting> taskApprovalSettings = _taskApprovalSettingRepository.Table
                                     .Where(x => x.TaskMasterId == taskMasterId && x.CentreCode == centreCode)
                                     .OrderBy(x => x.ApprovalSequenceNumber)
                                     .ToList();

            TaskApprovalSetting taskApproval = taskApprovalSettings.FirstOrDefault();
            if (taskApproval != null)
            {
                taskApprovalSettingModel.TaskApprovalSettingId = taskApproval.TaskApprovalSettingId;
                taskApprovalSettingModel.ApprovalSequenceNumber = taskApproval.ApprovalSequenceNumber;
                taskApprovalSettingModel.IsFinalApproval = taskApproval.IsFinalApproval;
            }

            taskApprovalSettingModel.EmployeeList = new List<EmployeeMasterModel>();
            taskApprovalSettingModel.EmployeeList = (from t in taskApprovalSettings
                                                     join u in _userMasterRepository.Table
                                                     .Where(u => u.UserType == UserTypeEnum.Employee.ToString())
                                                     on t.EmployeeId equals u.EntityId
                                                     select new EmployeeMasterModel
                                                     {
                                                         EmployeeId = t.EmployeeId,
                                                         FirstName = u.FirstName,
                                                         LastName = u.LastName,
                                                         IsFinalApproval = t.IsFinalApproval
                                                     }).OrderBy(e => taskApprovalSettings
                                                 .FirstOrDefault(x => x.EmployeeId == e.EmployeeId)?.ApprovalSequenceNumber).ToList();
            return taskApprovalSettingModel;
        }


        //Update TaskApprovalSetting.
        public virtual bool UpdateTaskApprovalSetting(TaskApprovalSettingModel taskApprovalSettingModel)
        {
            if (IsNull(taskApprovalSettingModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            List<long> employeeIdList = taskApprovalSettingModel.EmployeeIds?.Split(',')
                .Where(id => !string.IsNullOrWhiteSpace(id))
                .Select(long.Parse)
                .ToList();

            List<TaskApprovalSetting> existingApprovalSettings = _taskApprovalSettingRepository.Table
                .Where(x => x.CentreCode == taskApprovalSettingModel.CentreCode && x.TaskMasterId == taskApprovalSettingModel.TaskMasterId)
                .OrderBy(x => x.ApprovalSequenceNumber)
                .ToList();

            byte newEmployeePosition = taskApprovalSettingModel.ApprovalSequenceNumber;

            if (newEmployeePosition < 1 || newEmployeePosition > existingApprovalSettings.Count + 1)
                throw new CoditechException(ErrorCodes.InvalidData, "Invalid Approval Sequence Number.");

            int sequenceCounter = 1;
            List<TaskApprovalSetting> updateList = new List<TaskApprovalSetting>();

            for (int i = 0; i < existingApprovalSettings.Count; i++)
            {
                if (sequenceCounter == newEmployeePosition)
                {
                    foreach (var employeeId in employeeIdList)
                    {
                        var newSetting = new TaskApprovalSetting
                        {
                            EmployeeId = employeeId,
                            CentreCode = taskApprovalSettingModel.CentreCode,
                            TaskMasterId = taskApprovalSettingModel.TaskMasterId,
                            ApprovalSequenceNumber = (byte)sequenceCounter,
                            IsFinalApproval = (i == existingApprovalSettings.Count)
                        };
                        updateList.Add(newSetting);
                        sequenceCounter++;
                    }
                }

                TaskApprovalSetting existingSetting = existingApprovalSettings[i];
                existingSetting.ApprovalSequenceNumber = (byte)sequenceCounter;
                updateList.Add(existingSetting);
                sequenceCounter++;
            }

            // updateList.Last().IsFinalApproval = true;

            bool isTaskApprovalSettingUpdated = true;

            foreach (var taskApprovalSetting in updateList)
            {
                bool isUpdated = _taskApprovalSettingRepository.Update(taskApprovalSetting);
                if (!isUpdated)
                {
                    //isTaskApprovalSettingUpdated = false;
                    taskApprovalSettingModel.HasError = true;
                    taskApprovalSettingModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
                }
            }

            return isTaskApprovalSettingUpdated;

        }

        //Delete TaskApprovalSetting.
        public virtual bool DeleteTaskApprovalSetting(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaskApprovalSettingId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("TaskApprovalSettingId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteTaskApprovalSetting @TaskApprovalSettingId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        protected virtual bool IsTaskApprovalSettingAlreadyExist(TaskApprovalSettingModel taskApprovalSettingModel)
         => _taskApprovalSettingRepository.Table.Any(x => x.TaskMasterId == taskApprovalSettingModel.TaskMasterId && x.CentreCode == taskApprovalSettingModel.CentreCode);
        #endregion
    }

}






































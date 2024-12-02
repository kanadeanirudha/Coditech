using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;
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

        public TaskApprovalSettingService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _taskApprovalSettingRepository = new CoditechRepository<TaskApprovalSetting>(_serviceProvider.GetService<Coditech_Entities>());
            _taskMasterRepository = new CoditechRepository<TaskMaster>(_serviceProvider.GetService<Coditech_Entities>());
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

        #region Protected Method
        protected virtual bool IsTaskApprovalSettingAlreadyExist(TaskApprovalSettingModel taskApprovalSettingModel)
         => _taskApprovalSettingRepository.Table.Any(x => x.TaskMasterId == taskApprovalSettingModel.TaskMasterId && x.CentreCode == taskApprovalSettingModel .CentreCode);
        #endregion
    }

}





























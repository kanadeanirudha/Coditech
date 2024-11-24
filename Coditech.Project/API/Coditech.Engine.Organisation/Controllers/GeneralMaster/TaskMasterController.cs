using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Controllers
{
    public class TaskMasterController : BaseController
    {
        private readonly ITaskMasterService _taskMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public TaskMasterController(ICoditechLogging coditechLogging, ITaskMasterService taskMasterService)
        {
            _taskMasterService = taskMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/TaskMaster/GetTaskMasterList")]
        [Produces(typeof(TaskMasterListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]

        public virtual IActionResult GetTaskMasterList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                TaskMasterListModel list = _taskMasterService.GetTaskMasterList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<TaskMasterListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TaskMasterListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TaskMasterListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/TaskMaster/CreateTaskMaster")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TaskMasterResponse))]
        public virtual IActionResult CreateTaskMaster([FromBody] TaskMasterModel model)
        {
            try
            {
                TaskMasterModel taskMaster = _taskMasterService.CreateTaskMaster(model);
                return IsNotNull(taskMaster) ? CreateCreatedResponse(new TaskMasterResponse { TaskMasterModel = taskMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TaskMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TaskMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/TaskMaster/GetTaskMaster")]
        [HttpGet]
        [Produces(typeof(TaskMasterResponse))]
        public virtual IActionResult GetTaskMaster(short taskMasterId)
        {
            try
            {
                TaskMasterModel taskMasterModel = _taskMasterService.GetTaskMaster(taskMasterId);
                return IsNotNull(taskMasterModel) ? CreateOKResponse(new TaskMasterResponse { TaskMasterModel = taskMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TaskMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TaskMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/TaskMaster/UpdateTaskMaster")]
        [HttpPut, ValidateModel]
        [Produces(typeof(TaskMasterResponse))]
        public virtual IActionResult UpdateTaskMaster([FromBody] TaskMasterModel model)
        {
            try
            {
                bool isUpdated = _taskMasterService.UpdateTaskMaster(model);
                return isUpdated ? CreateOKResponse(new TaskMasterResponse { TaskMasterModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TaskMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TaskMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/TaskMaster/DeleteTaskMaster")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteTaskMaster([FromBody] ParameterModel taskMasterIds)
        {
            try
            {
                bool deleted = _taskMasterService.DeleteTaskMaster(taskMasterIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}

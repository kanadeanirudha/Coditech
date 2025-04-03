using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Controllers

{
    public class TaskSchedulerController : BaseController
    {
        private readonly ITaskSchedulerService _taskSchedulerService;
        protected readonly ICoditechLogging _coditechLogging;
        public TaskSchedulerController(ICoditechLogging coditechLogging, ITaskSchedulerService taskSchedulerService)
        {
            _taskSchedulerService = taskSchedulerService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/TaskScheduler/GetTaskSchedulerList")]
        [Produces(typeof(TaskSchedulerListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetTaskSchedulerList()
        {
            try
            {
                TaskSchedulerListModel list = _taskSchedulerService.GetTaskSchedulerList();
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<TaskSchedulerListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TaskSchedulerListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TaskSchedulerListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/TaskScheduler/CreateTaskScheduler")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TaskSchedulerResponse))]
        public virtual IActionResult CreateTaskScheduler([FromBody] TaskSchedulerModel model)
        {
            try
            {
                TaskSchedulerModel taskSchedulerModel = _taskSchedulerService.CreateTaskScheduler(model);
                return IsNotNull(taskSchedulerModel) ? CreateCreatedResponse(new TaskSchedulerResponse { TaskSchedulerModel = taskSchedulerModel }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TaskSchedulerResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TaskSchedulerResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/TaskScheduler/GetTaskSchedulerDetails")]
        [HttpGet]
        [Produces(typeof(TaskSchedulerResponse))]
        public virtual IActionResult GetTaskSchedulerDetails(int configuratorId, string schedulerCallFor)
        {
            try
            {
                TaskSchedulerModel taskSchedulerModel = _taskSchedulerService.GetTaskSchedulerDetails(configuratorId, schedulerCallFor);
                return IsNotNull(taskSchedulerModel) ? CreateOKResponse(new TaskSchedulerResponse { TaskSchedulerModel = taskSchedulerModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TaskSchedulerResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TaskSchedulerResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/TaskScheduler/UpdateTaskSchedulerDetails")]
        [HttpPut, ValidateModel]
        [Produces(typeof(TaskSchedulerResponse))]
        public virtual IActionResult UpdateTaskSchedulerDetails([FromBody] TaskSchedulerModel model)
        {
            try
            {
                bool isUpdated = _taskSchedulerService.UpdateTaskSchedulerDetails(model);
                return isUpdated ? CreateOKResponse(new TaskSchedulerResponse { TaskSchedulerModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TaskSchedulerResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TaskSchedulerResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/TaskScheduler/DeleteTaskScheduler")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteTaskScheduler([FromBody] ParameterModel taskSchedulerMasterIds)
        {
            try
            {
                bool deleted = _taskSchedulerService.DeleteTaskScheduler(taskSchedulerMasterIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
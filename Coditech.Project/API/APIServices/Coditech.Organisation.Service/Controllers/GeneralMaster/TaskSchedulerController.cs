using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
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


        [Route("/TaskScheduler/CreateBatchTaskScheduler")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TaskSchedulerResponse))]
        public virtual IActionResult CreateBatchTaskScheduler([FromBody] TaskSchedulerModel model)
        {
            try
            {
                TaskSchedulerModel batch = _taskSchedulerService.CreateBatchTaskScheduler(model);
                return IsNotNull(batch) ? CreateCreatedResponse(new TaskSchedulerResponse { TaskSchedulerModel = batch }) : CreateInternalServerErrorResponse();
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

        [Route("/TaskScheduler/GetBatchTaskSchedulerDetails")]
        [HttpGet]
        [Produces(typeof(TaskSchedulerResponse))]
        public virtual IActionResult GetBatchTaskSchedulerDetails(int configuratorId, string schedulerCallFor)
        {
            try
            {
                TaskSchedulerModel taskSchedulerModel = _taskSchedulerService.GetBatchTaskSchedulerDetails(configuratorId, schedulerCallFor);
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

        [Route("/TaskScheduler/UpdateBatchTaskSchedulerDetails")]
        [HttpPut, ValidateModel]
        [Produces(typeof(TaskSchedulerResponse))]
        public virtual IActionResult UpdateBatchTaskSchedulerDetails([FromBody] TaskSchedulerModel model)
        {
            try
            {
                bool isUpdated = _taskSchedulerService.UpdateBatchTaskSchedulerDetails(model);
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

        //[Route("/TaskScheduler/DeleteBatchTaskScheduler")]
        //[HttpPost, ValidateModel]
        //[Produces(typeof(TrueFalseResponse))]
        //public virtual IActionResult DeleteBatchTaskScheduler([FromBody] ParameterModel generalBatchMasterIds)
        //{
        //    try
        //    {
        //        bool deleted = _taskSchedulerService.DeleteBatchTaskScheduler(generalBatchMasterIds);
        //        return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
        //    }
        //    catch (CoditechException ex)
        //    {
        //        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Warning);
        //        return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
        //    }
        //    catch (Exception ex)
        //    {
        //        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskScheduler.ToString(), TraceLevel.Error);
        //        return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
        //    }
        //}
    }
}
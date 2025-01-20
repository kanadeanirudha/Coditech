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
    public class LogMessageController : BaseController
    {
        private readonly ILogMessageService _logMessageService;
        protected readonly ICoditechLogging _coditechLogging;
        public LogMessageController(ICoditechLogging coditechLogging, ILogMessageService logMessageService)
        {
            _logMessageService = logMessageService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/LogMessage/GetLogMessageList")]
        [Produces(typeof(LogMessageListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetLogMessageList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                LogMessageListModel list = _logMessageService.GetLogMessageList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<LogMessageListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LogMessage.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new LogMessageListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LogMessage.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new LogMessageListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/LogMessage/GetLogMessage")]
        [HttpGet]
        [Produces(typeof(LogMessageResponse))]
        public virtual IActionResult GetLogMessage(long logMessageId)
        {
            try
            {
                LogMessageModel logMessageModel = _logMessageService.GetLogMessage(logMessageId);
                return IsNotNull(logMessageModel) ? CreateOKResponse(new LogMessageResponse { LogMessageModel = logMessageModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LogMessage.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new LogMessageResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LogMessage.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new LogMessageResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/LogMessage/DeleteLogMessage")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteLogMessage([FromBody] ParameterModel logMessageIds)
        {
            try
            {
                bool deleted = _logMessageService.DeleteLogMessage(logMessageIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LogMessage.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LogMessage.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
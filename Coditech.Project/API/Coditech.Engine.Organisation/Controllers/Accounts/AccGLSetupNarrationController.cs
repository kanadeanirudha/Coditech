using Coditech.API.Data;
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
    public class AccGLSetupNarrationController : BaseController
    {
        private readonly IAccGLSetupNarrationService _accGLSetupNarrationService;
        protected readonly ICoditechLogging _coditechLogging;
        public AccGLSetupNarrationController(ICoditechLogging coditechLogging, IAccGLSetupNarrationService accGLSetupNarrationService)
        {
            _accGLSetupNarrationService = accGLSetupNarrationService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/AccGLSetupNarration/GetNarrationList")]
        [Produces(typeof(AccGLSetupNarrationListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetNarrationList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                AccGLSetupNarrationListModel list = _accGLSetupNarrationService.GetNarrationList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<AccGLSetupNarrationListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccGLSetupNarrationListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccGLSetupNarrationListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccGLSetupNarration/CreateNarration")]
        [HttpPost, ValidateModel]
        [Produces(typeof(AccGLSetupNarrationResponse))]
        public virtual IActionResult CreateNarration([FromBody] AccGLSetupNarrationModel model)
        {
            try
            {
                AccGLSetupNarrationModel createNarration = _accGLSetupNarrationService.CreateNarration(model);
                return IsNotNull(createNarration) ? CreateCreatedResponse(new AccGLSetupNarrationResponse { AccGLSetupNarrationModel = createNarration }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccGLSetupNarrationResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccGLSetupNarrationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccGLSetupNarration/GetNarration")]
        [HttpGet]
        [Produces(typeof(AccGLSetupNarrationResponse))]
        public virtual IActionResult GetNarration(int accGLSetupNarrationId)
        {
            try
            {
                AccGLSetupNarrationModel createNarrationModel = _accGLSetupNarrationService.GetNarration( accGLSetupNarrationId);
                return IsNotNull(createNarrationModel) ? CreateOKResponse(new AccGLSetupNarrationResponse { AccGLSetupNarrationModel = createNarrationModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccGLSetupNarrationResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccGLSetupNarrationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccGLSetupNarration/UpdateNarration")]
        [HttpPut, ValidateModel]
        [Produces(typeof(AccGLSetupNarrationResponse))]
        public virtual IActionResult UpdateNarration([FromBody] AccGLSetupNarrationModel model)
        {
            try
            {
                bool isUpdated = _accGLSetupNarrationService.UpdateNarration(model);
                return isUpdated ? CreateOKResponse(new AccGLSetupNarrationResponse { AccGLSetupNarrationModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccGLSetupNarrationResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccGLSetupNarrationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccGLSetupNarration/DeleteNarration")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteNarration([FromBody] ParameterModel accGLSetupNarrationId)
        {
            try
            {
                bool deleted = _accGLSetupNarrationService.DeleteNarration(accGLSetupNarrationId);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
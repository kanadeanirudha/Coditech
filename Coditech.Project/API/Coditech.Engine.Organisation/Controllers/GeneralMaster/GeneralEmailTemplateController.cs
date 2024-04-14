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
    public class GeneralEmailTemplateController : BaseController
    {

        private readonly IGeneralEmailTemplateService _generalEmailTemplateService;

        protected readonly ICoditechLogging _coditechLogging;
        public GeneralEmailTemplateController(ICoditechLogging coditechLogging, IGeneralEmailTemplateService generalEmailTemplateService)
        {
            _generalEmailTemplateService = generalEmailTemplateService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralEmailTemplate/GetEmailTemplateList")]
        [Produces(typeof(GeneralEmailTemplateListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetEmailTemplateList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralEmailTemplateListModel list = _generalEmailTemplateService.GetEmailTemplateList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralEmailTemplateListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEmailTemplateListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEmailTemplateListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEmailTemplate/CreateEmailTemplate")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralEmailTemplateResponse))]
        public virtual IActionResult CreateEmailTemplate([FromBody] GeneralEmailTemplateModel model)
        {
            try
            {
                GeneralEmailTemplateModel emailTemplate = _generalEmailTemplateService.CreateEmailTemplate(model);
                return IsNotNull(emailTemplate) ? CreateCreatedResponse(new GeneralEmailTemplateResponse { GeneralEmailTemplateModel = emailTemplate }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEmailTemplate/GetEmailTemplate")]
        [HttpGet]
        [Produces(typeof(GeneralEmailTemplateResponse))]
        public virtual IActionResult GetEmailTemplate(short generalEmailTemplateId)
        {
            try
            {
                GeneralEmailTemplateModel generalEmailTemplateModel = _generalEmailTemplateService.GetEmailTemplate(generalEmailTemplateId);
                return IsNotNull(generalEmailTemplateModel) ? CreateOKResponse(new GeneralEmailTemplateResponse { GeneralEmailTemplateModel = generalEmailTemplateModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEmailTemplate/UpdateEmailTemplate")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralEmailTemplateResponse))]
        public virtual IActionResult UpdateEmailTemplate([FromBody] GeneralEmailTemplateModel model)
        {
            try
            {
                bool isUpdated = _generalEmailTemplateService.UpdateEmailTemplate(model);
                return isUpdated ? CreateOKResponse(new GeneralEmailTemplateResponse { GeneralEmailTemplateModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEmailTemplate/DeleteEmailTemplate")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteEmailTemplate([FromBody] ParameterModel emailTemplateIds)
        {
            try
            {
                bool deleted = _generalEmailTemplateService.DeleteEmailTemplate(emailTemplateIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }

    public interface IGeneralEmailTemplateService
    {
        GeneralEmailTemplateListModel GetEmailTemplateList(FilterCollection filter, NameValueCollection nameValueCollection1, NameValueCollection nameValueCollection2, int pageIndex, int pageSize);
    }
}

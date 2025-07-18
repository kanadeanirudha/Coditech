﻿using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Controllers
{
    public class OrganisationCentreMasterController : BaseController
    {
        private readonly IOrganisationCentreMasterService _organisationCentreMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public OrganisationCentreMasterController(ICoditechLogging coditechLogging, IOrganisationCentreMasterService organisationCentreMasterService)
        {
            _organisationCentreMasterService = organisationCentreMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/OrganisationCentreMaster/GetOrganisationCentreList")]
        [Produces(typeof(OrganisationCentreListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetOrganisationCentreList(int adminRoleMasterId, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                OrganisationCentreListModel list = _organisationCentreMasterService.GetOrganisationCentreList(adminRoleMasterId, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<OrganisationCentreListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentreListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentreListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/CreateOrganisationCentre")]
        [HttpPost, ValidateModel]
        [Produces(typeof(OrganisationCentreResponse))]
        public virtual IActionResult CreateOrganisationCentre([FromBody] OrganisationCentreModel model)
        {
            try
            {
                OrganisationCentreModel organisationCentre = _organisationCentreMasterService.CreateOrganisationCentre(model);
                return IsNotNull(organisationCentre) ? CreateCreatedResponse(new OrganisationCentreResponse { OrganisationCentreModel = organisationCentre }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentreResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentreResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/GetOrganisationCentre")]
        [HttpGet]
        [Produces(typeof(OrganisationCentreResponse))]
        public virtual IActionResult GetOrganisationCentre(int organisationCentreMasterId)
        {
            try
            {
                OrganisationCentreModel organisationCentreModel = _organisationCentreMasterService.GetOrganisationCentre(organisationCentreMasterId);
                return IsNotNull(organisationCentreModel) ? CreateOKResponse(new OrganisationCentreResponse() { OrganisationCentreModel = organisationCentreModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentreResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentreResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/UpdateOrganisationCentre")]
        [HttpPut, ValidateModel]
        [Produces(typeof(OrganisationCentreResponse))]
        public virtual IActionResult UpdateOrganisationCentre([FromBody] OrganisationCentreModel model)
        {
            try
            {
                bool isUpdated = _organisationCentreMasterService.UpdateOrganisationCentre(model);
                return isUpdated ? CreateOKResponse(new OrganisationCentreResponse { OrganisationCentreModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentreResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentreResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/DeleteOrganisationCentre")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteOrganisationCentre([FromBody] ParameterModel organisationCentreIds)
        {
            try
            {
                bool deleted = _organisationCentreMasterService.DeleteOrganisationCentre(organisationCentreIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/GetPrintingFormat")]
        [HttpGet]
        [Produces(typeof(OrganisationCentrePrintingFormatResponse))]
        public virtual IActionResult GetPrintingFormat(int organisationCentreMasterId)
        {
            try
            {
                OrganisationCentrePrintingFormatModel organisationCentrePrintingFormatModel = _organisationCentreMasterService.GetPrintingFormat(organisationCentreMasterId);
                return IsNotNull(organisationCentrePrintingFormatModel) ? CreateOKResponse(new OrganisationCentrePrintingFormatResponse() { OrganisationCentrePrintingFormatModel = organisationCentrePrintingFormatModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PrintingFormat.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrePrintingFormatResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PrintingFormat.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrePrintingFormatResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/UpdatePrintingFormat")]
        [HttpPut, ValidateModel]
        [Produces(typeof(OrganisationCentrePrintingFormatResponse))]
        public virtual IActionResult UpdatePrintingFormat([FromBody] OrganisationCentrePrintingFormatModel model)
        {
            try
            {
                bool isUpdated = _organisationCentreMasterService.UpdatePrintingFormat(model);
                return isUpdated ? CreateOKResponse(new OrganisationCentrePrintingFormatResponse { OrganisationCentrePrintingFormatModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PrintingFormat.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrePrintingFormatResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PrintingFormat.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrePrintingFormatResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/GetCentrewiseGSTSetup")]
        [HttpGet]
        [Produces(typeof(OrganisationCentrewiseGSTCredentialResponse))]
        public virtual IActionResult GetCentrewiseGSTSetup(int organisationCentreMasterId)
        {
            try
            {
                OrganisationCentrewiseGSTCredentialModel organisationCentrewiseGSTCredentialModel = _organisationCentreMasterService.GetCentrewiseGSTSetup(organisationCentreMasterId);
                return IsNotNull(organisationCentrewiseGSTCredentialModel) ? CreateOKResponse(new OrganisationCentrewiseGSTCredentialResponse() { OrganisationCentrewiseGSTCredentialModel = organisationCentrewiseGSTCredentialModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseGST.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseGSTCredentialResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseGST.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseGSTCredentialResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/UpdateCentrewiseGSTSetup")]
        [HttpPut, ValidateModel]
        [Produces(typeof(OrganisationCentrewiseGSTCredentialResponse))]
        public virtual IActionResult UpdateCentrewiseGSTSetup([FromBody] OrganisationCentrewiseGSTCredentialModel model)
        {
            try
            {
                bool isUpdated = _organisationCentreMasterService.UpdateCentrewiseGSTSetup(model);
                return isUpdated ? CreateOKResponse(new OrganisationCentrewiseGSTCredentialResponse { OrganisationCentrewiseGSTCredentialModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseGST.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseGSTCredentialResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseGST.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseGSTCredentialResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/GetCentrewiseSmtpSetup")]
        [HttpGet]
        [Produces(typeof(OrganisationCentrewiseSmtpSettingResponse))]
        public virtual IActionResult GetCentrewiseSmtpSetup(int organisationCentreMasterId)
        {
            try
            {
                OrganisationCentrewiseSmtpSettingModel organisationCentrewiseSmtpSettingModel = _organisationCentreMasterService.GetCentrewiseSmtpSetup(organisationCentreMasterId);
                return IsNotNull(organisationCentrewiseSmtpSettingModel) ? CreateOKResponse(new OrganisationCentrewiseSmtpSettingResponse() { OrganisationCentrewiseSmtpSettingModel = organisationCentrewiseSmtpSettingModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseSmtp.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseSmtpSettingResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseSmtp.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseSmtpSettingResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/UpdateCentrewiseSmtpSetup")]
        [HttpPut, ValidateModel]
        [Produces(typeof(OrganisationCentrewiseSmtpSettingResponse))]
        public virtual IActionResult UpdateCentrewiseSmtpSetup([FromBody] OrganisationCentrewiseSmtpSettingModel model)
        {
            try
            {
                bool isUpdated = _organisationCentreMasterService.UpdateCentrewiseSmtpSetup(model);
                return isUpdated ? CreateOKResponse(new OrganisationCentrewiseSmtpSettingResponse { OrganisationCentrewiseSmtpSettingModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseSmtp.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseSmtpSettingResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseSmtp.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseSmtpSettingResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/GetCentrewiseSmsSetup")]
        [HttpGet]
        [Produces(typeof(OrganisationCentrewiseSmsSettingResponse))]
        public virtual IActionResult GetCentrewiseSmsSetup(int organisationCentreMasterId, byte generalSmsProviderId)
        {
            try
            {
                OrganisationCentrewiseSmsSettingModel organisationCentrewiseSmsSettingModel = _organisationCentreMasterService.GetCentrewiseSmsSetup(organisationCentreMasterId, generalSmsProviderId);
                return IsNotNull(organisationCentrewiseSmsSettingModel) ? CreateOKResponse(new OrganisationCentrewiseSmsSettingResponse() { OrganisationCentrewiseSmsSettingModel = organisationCentrewiseSmsSettingModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseSms.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseSmsSettingResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseSms.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseSmsSettingResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/UpdateCentrewiseSmsSetup")]
        [HttpPut, ValidateModel]
        [Produces(typeof(OrganisationCentrewiseSmsSettingResponse))]
        public virtual IActionResult UpdateCentrewiseSmsSetup([FromBody] OrganisationCentrewiseSmsSettingModel model)
        {
            try
            {
                bool isUpdated = _organisationCentreMasterService.UpdateCentrewiseSmsSetup(model);
                return isUpdated ? CreateOKResponse(new OrganisationCentrewiseSmsSettingResponse { OrganisationCentrewiseSmsSettingModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseSms.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseSmsSettingResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseSms.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseSmsSettingResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/GetCentrewiseWhatsAppSetup")]
        [HttpGet]
        [Produces(typeof(OrganisationCentrewiseWhatsAppSettingResponse))]
        public virtual IActionResult GetCentrewiseWhatsAppSetup(int organisationCentreMasterId, byte generalWhatsAppProviderId)
        {
            try
            {
                OrganisationCentrewiseWhatsAppSettingModel organisationCentrewiseWhatsAppSettingModel = _organisationCentreMasterService.GetCentrewiseWhatsAppSetup(organisationCentreMasterId, generalWhatsAppProviderId);
                return IsNotNull(organisationCentrewiseWhatsAppSettingModel) ? CreateOKResponse(new OrganisationCentrewiseWhatsAppSettingResponse() { OrganisationCentrewiseWhatsAppSettingModel = organisationCentrewiseWhatsAppSettingModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppService.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseWhatsAppSettingResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppService.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseWhatsAppSettingResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/UpdateCentrewiseWhatsAppSetup")]
        [HttpPut, ValidateModel]
        [Produces(typeof(OrganisationCentrewiseWhatsAppSettingResponse))]
        public virtual IActionResult UpdateCentrewiseWhatsAppSetup([FromBody] OrganisationCentrewiseWhatsAppSettingModel model)
        {
            try
            {
                bool isUpdated = _organisationCentreMasterService.UpdateCentrewiseWhatsAppSetup(model);
                return isUpdated ? CreateOKResponse(new OrganisationCentrewiseWhatsAppSettingResponse { OrganisationCentrewiseWhatsAppSettingModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppService.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseWhatsAppSettingResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppService.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseWhatsAppSettingResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/GetCentrewiseEmailTemplateSetup")]
        [HttpGet]
        [Produces(typeof(OrganisationCentrewiseEmailTemplateResponse))]
        public virtual IActionResult GetCentrewiseEmailTemplateSetup(int organisationCentreMasterId, string emailTemplateCode, string templateType)
        {
            try
            {
                OrganisationCentrewiseEmailTemplateModel organisationCentrewiseEmailTemplateModel = _organisationCentreMasterService.GetCentrewiseEmailTemplateSetup(organisationCentreMasterId, emailTemplateCode, templateType);
                return IsNotNull(organisationCentrewiseEmailTemplateModel) ? CreateOKResponse(new OrganisationCentrewiseEmailTemplateResponse() { OrganisationCentrewiseEmailTemplateModel = organisationCentrewiseEmailTemplateModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseEmailTemplate.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseEmailTemplate.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/UpdateCentrewiseEmailTemplateSetup")]
        [HttpPut, ValidateModel]
        [Produces(typeof(OrganisationCentrewiseEmailTemplateResponse))]
        public virtual IActionResult UpdateCentrewiseEmailTemplateSetup([FromBody] OrganisationCentrewiseEmailTemplateModel model)
        {
            try
            {
                bool isUpdated = _organisationCentreMasterService.UpdateCentrewiseEmailTemplateSetup(model);
                return isUpdated ? CreateOKResponse(new OrganisationCentrewiseEmailTemplateResponse { OrganisationCentrewiseEmailTemplateModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseEmailTemplate.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseEmailTemplate.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/GetCentrewiseSMSTemplateSetup")]
        [HttpGet]
        [Produces(typeof(OrganisationCentrewiseEmailTemplateResponse))]
        public virtual IActionResult GetCentrewiseSMSTemplateSetup(int organisationCentreMasterId, string emailTemplateCode)
        {
            try
            {
                OrganisationCentrewiseEmailTemplateModel organisationCentrewiseEmailTemplateModel = _organisationCentreMasterService.GetCentrewiseSMSTemplateSetup(organisationCentreMasterId, emailTemplateCode);
                return IsNotNull(organisationCentrewiseEmailTemplateModel) ? CreateOKResponse(new OrganisationCentrewiseEmailTemplateResponse() { OrganisationCentrewiseEmailTemplateModel = organisationCentrewiseEmailTemplateModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseSMSTemplate.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseSMSTemplate.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/UpdateCentrewiseSMSTemplateSetup")]
        [HttpPut, ValidateModel]
        [Produces(typeof(OrganisationCentrewiseEmailTemplateResponse))]
        public virtual IActionResult UpdateCentrewiseSMSTemplateSetup([FromBody] OrganisationCentrewiseEmailTemplateModel model)
        {
            try
            {
                bool isUpdated = _organisationCentreMasterService.UpdateCentrewiseSMSTemplateSetup(model);
                return isUpdated ? CreateOKResponse(new OrganisationCentrewiseEmailTemplateResponse { OrganisationCentrewiseEmailTemplateModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseSMSTemplate.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseSMSTemplate.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/GetCentrewiseWhatsAppTemplateSetup")]
        [HttpGet]
        [Produces(typeof(OrganisationCentrewiseEmailTemplateResponse))]
        public virtual IActionResult GetCentrewiseWhatsAppTemplateSetup(int organisationCentreMasterId, string emailTemplateCode)
        {
            try
            {
                OrganisationCentrewiseEmailTemplateModel organisationCentrewiseEmailTemplateModel = _organisationCentreMasterService.GetCentrewiseWhatsAppTemplateSetup(organisationCentreMasterId, emailTemplateCode);
                return IsNotNull(organisationCentrewiseEmailTemplateModel) ? CreateOKResponse(new OrganisationCentrewiseEmailTemplateResponse() { OrganisationCentrewiseEmailTemplateModel = organisationCentrewiseEmailTemplateModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseWhatsAppTemplate.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseWhatsAppTemplate.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/UpdateCentrewiseWhatsAppTemplateSetup")]
        [HttpPut, ValidateModel]
        [Produces(typeof(OrganisationCentrewiseEmailTemplateResponse))]
        public virtual IActionResult UpdateCentrewiseWhatsAppTemplateSetup([FromBody] OrganisationCentrewiseEmailTemplateModel model)
        {
            try
            {
                bool isUpdated = _organisationCentreMasterService.UpdateCentrewiseWhatsAppTemplateSetup(model);
                return isUpdated ? CreateOKResponse(new OrganisationCentrewiseEmailTemplateResponse { OrganisationCentrewiseEmailTemplateModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseWhatsAppTemplate.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseWhatsAppTemplate.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseEmailTemplateResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/GetCentrewiseUserName")]
        [HttpGet]
        [Produces(typeof(OrganisationCentrewiseUserNameRegistrationResponse))]
        public virtual IActionResult GetCentrewiseUserName(int organisationCentreMasterId, int organisationCentrewiseUserNameRegistrationId)
        {
            try
            {
                OrganisationCentrewiseUserNameRegistrationModel organisationCentrewiseUserNameRegistrationModel = _organisationCentreMasterService.GetCentrewiseUserName(organisationCentreMasterId, organisationCentrewiseUserNameRegistrationId);
                return IsNotNull(organisationCentrewiseUserNameRegistrationModel) ? CreateOKResponse(new OrganisationCentrewiseUserNameRegistrationResponse() { OrganisationCentrewiseUserNameRegistrationModel = organisationCentrewiseUserNameRegistrationModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseUserName.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseUserNameRegistrationResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseUserName.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseUserNameRegistrationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/UpdateCentrewiseUserName")]
        [HttpPut, ValidateModel]
        [Produces(typeof(OrganisationCentrewiseUserNameRegistrationResponse))]
        public virtual IActionResult UpdateCentrewiseUserName([FromBody] OrganisationCentrewiseUserNameRegistrationModel model)
        {
            try
            {
                bool isUpdated = _organisationCentreMasterService.UpdateCentrewiseUserName(model);
                return isUpdated ? CreateOKResponse(new OrganisationCentrewiseUserNameRegistrationResponse { OrganisationCentrewiseUserNameRegistrationModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseUserName.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseUserNameRegistrationResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseUserName.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseUserNameRegistrationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/IsCentreNameAlreadyExist")]
        [HttpPut, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult IsCentreNameAlreadyExist([FromBody] string centreName)
        {
            try
            {
                bool isUpdated = _organisationCentreMasterService.IsCentreNameAlreadyExist(centreName);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = isUpdated });

            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentreResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentreResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/SendTestModal")]
        [HttpPost, ValidateModel]
        [Produces(typeof(OrganisationCentrewiseSmtpSettingSendTestEmailResponse))]
        public virtual IActionResult SendTestModal([FromBody] OrganisationCentrewiseSmtpSettingSendTestEmailModel model)
        {
            try
            {
                OrganisationCentrewiseSmtpSettingSendTestEmailModel organisationCentrewiseSmtpSettingSendTestEmail = _organisationCentreMasterService.SendTestModal(model);
                return IsNotNull(organisationCentrewiseSmtpSettingSendTestEmail) ? CreateCreatedResponse(new OrganisationCentrewiseSmtpSettingSendTestEmailResponse { OrganisationCentrewiseSmtpSettingSendTestEmailModel = organisationCentrewiseSmtpSettingSendTestEmail }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SendTestEmail.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseSmtpSettingSendTestEmailResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SendTestEmail.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseSmtpSettingSendTestEmailResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}

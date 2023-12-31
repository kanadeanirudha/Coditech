﻿using Coditech.API.Organisation.Service.Interface.Organisation;
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
        public virtual IActionResult GetOrganisationCentreList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                OrganisationCentreListModel list = _organisationCentreMasterService.GetOrganisationCentreList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
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
        public virtual IActionResult GetOrganisationCentre(short organisationCentreMasterId)
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
        public virtual IActionResult GetPrintingFormat(short organisationCentreMasterId)
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
        public virtual IActionResult GetCentrewiseGSTSetup(short organisationCentreMasterId)
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
    }
}

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
        public IActionResult CreateOrganisationCentre([FromBody] OrganisationCentreModel model)
        {
            try
            {
                OrganisationCentreModel organisationCentre = _organisationCentreMasterService.CreateOrganisationCentre(model);
                return IsNotNull(organisationCentre) ? CreateCreatedResponse(new OrganisationCentreResponse { OrganisationCentreMasterModel = organisationCentre }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentreResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentreResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/GetOrganisationCentre")]
        [HttpGet]
        [Produces(typeof(OrganisationCentreModel))]
        public IActionResult GetOrganisationCentre(short organisationCentreMasterId)
        {
            try
            {
                OrganisationCentreModel organisationCentreMasterModel = _organisationCentreMasterService.GetOrganisationCentre(organisationCentreMasterId);
                return IsNotNull(organisationCentreMasterModel) ? CreateOKResponse(organisationCentreMasterModel) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentreModel { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentreModel { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentreMaster/UpdateOrganisationCentre")]
        [HttpPut, ValidateModel]
        [Produces(typeof(OrganisationCentreResponse))]
        public IActionResult UpdateOrganisationCentre([FromBody] OrganisationCentreModel model)
        {
            try
            {
                bool isUpdated = _organisationCentreMasterService.UpdateOrganisationCentre(model);
                return isUpdated ? CreateOKResponse(new OrganisationCentreResponse { OrganisationCentreMasterModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentreResponse { HasError = true, ErrorCode = ex.ErrorCode });
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
    }
} 

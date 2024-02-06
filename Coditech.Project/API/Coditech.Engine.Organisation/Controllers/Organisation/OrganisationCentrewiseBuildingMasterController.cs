using Coditech.API.Organisation.Service.Interface.Organisation;
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
    public class OrganisationCentrewiseBuildingMasterController : BaseController
    {
        private readonly IOrganisationCentrewiseBuildingMasterService _organisationCentrewiseBuildingMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public OrganisationCentrewiseBuildingMasterController(ICoditechLogging coditechLogging, IOrganisationCentrewiseBuildingMasterService organisationCentrewiseBuildingMasterService)
        {
            _organisationCentrewiseBuildingMasterService = organisationCentrewiseBuildingMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/OrganisationCentrewiseBuildingMaster/GetOrganisationCentrewiseBuildingList")]
        [Produces(typeof(OrganisationCentrewiseBuildingListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetOrganisationCentrewiseBuildingList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                OrganisationCentrewiseBuildingListModel list = _organisationCentrewiseBuildingMasterService.GetOrganisationCentrewiseBuildingList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<OrganisationCentrewiseBuildingListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseBuildingListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseBuildingListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentrewiseBuildingMaster/CreateOrganisationCentrewiseBuilding")]
        [HttpPost, ValidateModel]
        [Produces(typeof(OrganisationCentrewiseBuildingResponse))]
        public virtual IActionResult CreateOrganisationCentrewiseBuilding([FromBody] OrganisationCentrewiseBuildingModel model)
        {
            try
            {
                OrganisationCentrewiseBuildingModel organisationCentre = _organisationCentrewiseBuildingMasterService.CreateOrganisationCentrewiseBuilding(model);
                return IsNotNull(organisationCentre) ? CreateCreatedResponse(new OrganisationCentrewiseBuildingResponse { OrganisationCentrewiseBuildingModel = organisationCentre }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseBuildingResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseBuildingResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentrewiseBuildingMaster/GetOrganisationCentrewiseBuilding")]
        [HttpGet]
        [Produces(typeof(OrganisationCentrewiseBuildingResponse))]
        public virtual IActionResult GetOrganisationCentre(short organisationCentrewiseBuildingMasterId)
        {
            try
            {
                OrganisationCentrewiseBuildingModel organisationCentrewiseBuildingModel = _organisationCentrewiseBuildingMasterService.GetOrganisationCentrewiseBuilding(organisationCentrewiseBuildingMasterId);
                return IsNotNull(organisationCentrewiseBuildingModel) ? CreateOKResponse(new OrganisationCentrewiseBuildingResponse() { OrganisationCentrewiseBuildingModel = organisationCentrewiseBuildingModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseBuildingResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseBuildingResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentrewiseBuildingMaster/UpdateOrganisationCentrewiseBuilding")]
        [HttpPut, ValidateModel]
        [Produces(typeof(OrganisationCentrewiseBuildingResponse))]
        public virtual IActionResult UpdateOrganisationCentrewiseBuilding([FromBody] OrganisationCentrewiseBuildingModel model)
        {
            try
            {
                bool isUpdated = _organisationCentrewiseBuildingMasterService.UpdateOrganisationCentrewiseBuilding(model);
                return isUpdated ? CreateOKResponse(new OrganisationCentrewiseBuildingResponse { OrganisationCentrewiseBuildingModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseBuildingResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseBuildingResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentrewiseBuildingMaster/DeleteOrganisationCentrewiseBuilding")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteOrganisationCentrewiseBuilding([FromBody] ParameterModel organisationCentrewiseBuildingIds)
        {
            try
            {
                bool deleted = _organisationCentrewiseBuildingMasterService.DeleteOrganisationCentrewiseBuilding(organisationCentrewiseBuildingIds);
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

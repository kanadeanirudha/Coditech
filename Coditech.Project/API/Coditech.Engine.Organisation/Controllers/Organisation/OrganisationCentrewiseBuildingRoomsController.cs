using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Coditech.API.Controllers
{
    public class OrganisationCentrewiseBuildingRoomsController : BaseController
    {
        private readonly IOrganisationCentrewiseBuildingRoomsService _organisationCentrewiseBuildingRoomsService;
        protected readonly ICoditechLogging _coditechLogging;
        public OrganisationCentrewiseBuildingRoomsController(ICoditechLogging coditechLogging, IOrganisationCentrewiseBuildingRoomsService organisationCentrewiseBuildingRoomsService)
        {
            _organisationCentrewiseBuildingRoomsService = organisationCentrewiseBuildingRoomsService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/OrganisationCentrewiseBuildingRooms/GetOrganisationCentrewiseBuildingRoomsList")]
        [Produces(typeof(OrganisationCentrewiseBuildingRoomsListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetOrganisationCentrewiseBuildingRoomsList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                OrganisationCentrewiseBuildingRoomsListModel list = _organisationCentrewiseBuildingRoomsService.GetOrganisationCentrewiseBuildingRoomsList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<OrganisationCentrewiseBuildingRoomsListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseBuildingRoomsListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseBuildingRoomsListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentrewiseBuildingRooms/CreateOrganisationCentrewiseBuildingRooms")]
        [HttpPost, ValidateModel]
        [Produces(typeof(OrganisationCentrewiseBuildingRoomsResponse))]
        public virtual IActionResult CreateOrganisationCentrewiseBuildingRooms([FromBody] OrganisationCentrewiseBuildingRoomsModel model)
        {
            try
            {
                OrganisationCentrewiseBuildingRoomsModel organisationCentrewiseBuildingRooms = _organisationCentrewiseBuildingRoomsService.CreateOrganisationCentrewiseBuildingRooms(model);
                return HelperUtility.IsNotNull(organisationCentrewiseBuildingRooms) ? CreateCreatedResponse(new OrganisationCentrewiseBuildingRoomsResponse { OrganisationCentrewiseBuildingRoomsModel = organisationCentrewiseBuildingRooms }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseBuildingRoomsResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseBuildingRoomsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentrewiseBuildingRooms/GetOrganisationCentrewiseBuildingRooms")]
        [HttpGet]
        [Produces(typeof(OrganisationCentrewiseBuildingRoomsResponse))]
        public virtual IActionResult GetOrganisationCentrewiseBuildingRooms(short organisationCentrewiseBuildingRoomId)
        {
            try
            {
                OrganisationCentrewiseBuildingRoomsModel organisationCentrewiseBuildingRoomsModel = _organisationCentrewiseBuildingRoomsService.GetOrganisationCentrewiseBuildingRooms(organisationCentrewiseBuildingRoomId);
                return HelperUtility.IsNotNull(organisationCentrewiseBuildingRoomsModel) ? CreateOKResponse(new OrganisationCentrewiseBuildingRoomsResponse { OrganisationCentrewiseBuildingRoomsModel = organisationCentrewiseBuildingRoomsModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseBuildingRoomsResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseBuildingRoomsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentrewiseBuildingRooms/UpdateOrganisationCentrewiseBuildingRooms")]
        [HttpPut, ValidateModel]
        [Produces(typeof(OrganisationCentrewiseBuildingRoomsResponse))]
        public virtual IActionResult UpdateOrganisationCentrewiseBuildingRooms([FromBody] OrganisationCentrewiseBuildingRoomsModel model)
        {
            try
            {
                bool isUpdated = _organisationCentrewiseBuildingRoomsService.UpdateOrganisationCentrewiseBuildingRooms(model);
                return isUpdated ? CreateOKResponse(new OrganisationCentrewiseBuildingRoomsResponse { OrganisationCentrewiseBuildingRoomsModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseBuildingRoomsResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseBuildingRoomsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentrewiseBuildingRooms/DeleteOrganisationCentrewiseBuildingRooms")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteOrganisationCentrewiseBuildingRooms([FromBody] ParameterModel organisationCentrewiseBuildingRoomIds)
        {
            try
            {
                bool deleted = _organisationCentrewiseBuildingRoomsService.DeleteOrganisationCentrewiseBuildingRooms(organisationCentrewiseBuildingRoomIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseBuildingRooms.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}

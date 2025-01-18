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
    public class GeneralOccupationMasterController : BaseController
    {
        private readonly IGeneralOccupationMasterService _generalOccupationMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralOccupationMasterController(ICoditechLogging coditechLogging, IGeneralOccupationMasterService generalOccupationMasterService)
        {
            _generalOccupationMasterService = generalOccupationMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralOccupationMaster/GetOccupationList")]
        [Produces(typeof(GeneralOccupationListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetOccupationList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralOccupationListModel list = _generalOccupationMasterService.GetOccupationList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralOccupationListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralOccupationListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralOccupationListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralOccupationMaster/CreateOccupation")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralOccupationResponse))]
        public virtual IActionResult CreateOccupation([FromBody] GeneralOccupationModel model)
        {
            try
            {
                GeneralOccupationModel OccupationMaster = _generalOccupationMasterService.CreateOccupation(model);
                return IsNotNull(OccupationMaster) ? CreateCreatedResponse(new GeneralOccupationResponse { GeneralOccupationModel = OccupationMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralOccupationResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralOccupationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralOccupationMaster/GetOccupation")]
        [HttpGet]
        [Produces(typeof(GeneralOccupationResponse))]
        public virtual IActionResult GetOccupation(short generalOccupationMasterId)
        {
            try
            {
                GeneralOccupationModel generalOccupationMasterModel = _generalOccupationMasterService.GetOccupation(generalOccupationMasterId);
                return IsNotNull(generalOccupationMasterModel) ? CreateOKResponse(new GeneralOccupationResponse { GeneralOccupationModel = generalOccupationMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralOccupationResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralOccupationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralOccupationMaster/UpdateOccupation")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralOccupationResponse))]
        public virtual IActionResult UpdateOccupation([FromBody] GeneralOccupationModel model)
        {
            try
            {
                bool isUpdated = _generalOccupationMasterService.UpdateOccupation(model);
                return isUpdated ? CreateOKResponse(new GeneralOccupationResponse { GeneralOccupationModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralOccupationResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralOccupationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralOccupationMaster/DeleteOccupation")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteOccupation([FromBody] ParameterModel OccupationIds)
        {
            try
            {
                bool deleted = _generalOccupationMasterService.DeleteOccupation(OccupationIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OccupationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
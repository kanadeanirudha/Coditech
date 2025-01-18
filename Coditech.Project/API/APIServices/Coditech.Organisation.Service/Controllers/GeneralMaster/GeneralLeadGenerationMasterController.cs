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
    public class GeneralLeadGenerationMasterController : BaseController
    {
        private readonly IGeneralLeadGenerationMasterService _generalLeadGenerationMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralLeadGenerationMasterController(ICoditechLogging coditechLogging, IGeneralLeadGenerationMasterService generalLeadGenerationMasterService)
        {
            _generalLeadGenerationMasterService = generalLeadGenerationMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralLeadGenerationMaster/GetLeadGenerationList")]
        [Produces(typeof(GeneralLeadGenerationListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetLeadGenerationList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralLeadGenerationListModel list = _generalLeadGenerationMasterService.GetLeadGenerationList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralLeadGenerationListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralLeadGenerationListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralLeadGenerationListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralLeadGenerationMaster/CreateLeadGeneration")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralLeadGenerationResponse))]
        public virtual IActionResult CreateLeadGeneration([FromBody] GeneralLeadGenerationModel model)
        {
            try
            {
                GeneralLeadGenerationModel LeadGenerationMaster = _generalLeadGenerationMasterService.CreateLeadGeneration(model);
                return IsNotNull(LeadGenerationMaster) ? CreateCreatedResponse(new GeneralLeadGenerationResponse { GeneralLeadGenerationModel = LeadGenerationMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralLeadGenerationResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralLeadGenerationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralLeadGenerationMaster/GetLeadGeneration")]
        [HttpGet]
        [Produces(typeof(GeneralLeadGenerationResponse))]
        public virtual IActionResult GetLeadGeneration(long generalLeadGenerationMasterId)
        {
            try
            {
                GeneralLeadGenerationModel generalLeadGenerationMasterModel = _generalLeadGenerationMasterService.GetLeadGeneration(generalLeadGenerationMasterId);
                return IsNotNull(generalLeadGenerationMasterModel) ? CreateOKResponse(new GeneralLeadGenerationResponse { GeneralLeadGenerationModel = generalLeadGenerationMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralLeadGenerationResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralLeadGenerationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralLeadGenerationMaster/UpdateLeadGeneration")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralLeadGenerationResponse))]
        public virtual IActionResult UpdateLeadGeneration([FromBody] GeneralLeadGenerationModel model)
        {
            try
            {
                bool isUpdated = _generalLeadGenerationMasterService.UpdateLeadGeneration(model);
                return isUpdated ? CreateOKResponse(new GeneralLeadGenerationResponse { GeneralLeadGenerationModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralLeadGenerationResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralLeadGenerationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralLeadGenerationMaster/DeleteLeadGeneration")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteLeadGeneration([FromBody] ParameterModel LeadGenerationIds)
        {
            try
            {
                bool deleted = _generalLeadGenerationMasterService.DeleteLeadGeneration(LeadGenerationIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.LeadGenerationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
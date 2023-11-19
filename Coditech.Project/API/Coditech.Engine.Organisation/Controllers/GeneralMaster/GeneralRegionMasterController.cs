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
    public class GeneralRegionMasterController : BaseController
    {
        private readonly IGeneralRegionMasterService _generalRegionMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralRegionMasterController(ICoditechLogging coditechLogging, IGeneralRegionMasterService generalRegionMasterService)
        {
            _generalRegionMasterService = generalRegionMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralRegionMaster/GetRegionList")]
        [Produces(typeof(GeneralRegionListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetRegionList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralRegionListModel list = _generalRegionMasterService.GetRegionList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralRegionListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralRegionListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralRegionListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralRegionMaster/CreateRegion")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralRegionResponse))]
        public IActionResult CreateRegion([FromBody] GeneralRegionModel model)
        {
            try
            {
                GeneralRegionModel regionMaster = _generalRegionMasterService.CreateRegion(model);
                return IsNotNull(regionMaster) ? CreateCreatedResponse(new GeneralRegionResponse { GeneralRegionModel = regionMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralRegionResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralRegionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralRegionMaster/GetRegion")]
        [HttpGet]
        [Produces(typeof(GeneralRegionResponse))]
        public IActionResult GetRegion(short generalRegionMasterId)
        {
            try
            {
                GeneralRegionModel generalRegionModel = _generalRegionMasterService.GetRegion(generalRegionMasterId);
                return IsNotNull(generalRegionModel) ? CreateOKResponse(new GeneralRegionResponse { GeneralRegionModel = generalRegionModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralRegionResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralRegionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralRegionMaster/UpdateRegion")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralRegionResponse))]
        public IActionResult UpdateRegion([FromBody] GeneralRegionModel model)
        {
            try
            {
                bool isUpdated = _generalRegionMasterService.UpdateRegion(model);
                return isUpdated ? CreateOKResponse(new GeneralRegionResponse { GeneralRegionModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralRegionResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralRegionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralRegionMaster/DeleteRegion")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteRegion([FromBody] ParameterModel regionIds)
        {
            try
            {
                bool deleted = _generalRegionMasterService.DeleteRegion(regionIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
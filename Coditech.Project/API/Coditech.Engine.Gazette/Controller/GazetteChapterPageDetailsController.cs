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
    public class GazetteChaptersPageDetailController : BaseController
    {
        private readonly IGazetteChaptersPageDetailService _gazetteChaptersPageDetailService;
        protected readonly ICoditechLogging _coditechLogging;
        public GazetteChaptersPageDetailController(ICoditechLogging coditechLogging, IGazetteChaptersPageDetailService gazetteChaptersPageDetailService)
        {
            _gazetteChaptersPageDetailService = gazetteChaptersPageDetailService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GazetteChapterPageDetails/GetGazetteChaptersPageDetailList")]
        [Produces(typeof(GazetteChaptersPageDetailListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetGazetteChaptersPageDetailList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GazetteChaptersPageDetailListModel list = _gazetteChaptersPageDetailService.GetGazetteChaptersPageDetailList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GazetteChaptersPageDetailListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GazetteChaptersPageDetailListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GazetteChaptersPageDetailListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GazetteChapterPageDetails/CreateGazetteChaptersPageDetail")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GazetteChaptersPageDetailResponse))]
        public virtual IActionResult CreateGazetteChaptersPageDetail([FromBody] GazetteChaptersPageDetailModel model)
        {
            try
            {
                GazetteChaptersPageDetailModel gazetteChaptersPageDetail = _gazetteChaptersPageDetailService.CreateGazetteChaptersPageDetail(model);
                return IsNotNull(gazetteChaptersPageDetail) ? CreateCreatedResponse(new GazetteChaptersPageDetailResponse { GazetteChaptersPageDetailModel = gazetteChaptersPageDetail }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GazetteChaptersPageDetailResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GazetteChaptersPageDetailResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GazetteChapterPageDetails/GetGazetteChaptersPageDetail")]
        [HttpGet]
        [Produces(typeof(GazetteChaptersPageDetailResponse))]
        public virtual IActionResult GetGazetteChaptersPageDetail(int gazetteChaptersPageDetailId)
        {
            try
            {
                GazetteChaptersPageDetailModel gazetteChaptersPageDetailModel = _gazetteChaptersPageDetailService.GetGazetteChaptersPageDetail(gazetteChaptersPageDetailId);
                return IsNotNull(gazetteChaptersPageDetailModel) ? CreateOKResponse(new GazetteChaptersPageDetailResponse { GazetteChaptersPageDetailModel = gazetteChaptersPageDetailModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GazetteChaptersPageDetailResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GazetteChaptersPageDetailResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GazetteChapterPageDetails/UpdateGazetteChaptersPageDetail")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GazetteChaptersPageDetailResponse))]
        public virtual IActionResult UpdateGazetteChaptersPageDetail([FromBody] GazetteChaptersPageDetailModel model)
        {
            try
            {
                bool isUpdated = _gazetteChaptersPageDetailService.UpdateGazetteChaptersPageDetail(model);
                return isUpdated ? CreateOKResponse(new GazetteChaptersPageDetailResponse { GazetteChaptersPageDetailModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GazetteChaptersPageDetailResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GazetteChaptersPageDetailResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GazetteChapterPageDetails/DeleteGazetteChaptersPageDetail")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteGazetteChaptersPageDetail([FromBody] ParameterModel gazetteChaptersPageDetailIds)
        {
            try
            {
                bool deleted = _gazetteChaptersPageDetailService.DeleteGazetteChaptersPageDetail(gazetteChaptersPageDetailIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        
    }
}
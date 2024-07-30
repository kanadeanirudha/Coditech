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
    public class CoditechApplicationSettingController : BaseController
    {
        private readonly ICoditechApplicationSettingService _coditechApplicationSettingService;
        protected readonly ICoditechLogging _coditechLogging;
        public CoditechApplicationSettingController(ICoditechLogging coditechLogging, ICoditechApplicationSettingService coditechApplicationSettingService)
        {
            _coditechApplicationSettingService = coditechApplicationSettingService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/CoditechApplicationSetting/GetCoditechApplicationSettingList")]
        [Produces(typeof(CoditechApplicationSettingListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetCoditechApplicationSettingList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                CoditechApplicationSettingListModel list = _coditechApplicationSettingService.GetCoditechApplicationSettingList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<CoditechApplicationSettingListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new CoditechApplicationSettingListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new CoditechApplicationSettingListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/CoditechApplicationSetting/CreateCoditechApplicationSetting")]
        [HttpPost, ValidateModel]
        [Produces(typeof(CoditechApplicationSettingResponse))]
        public virtual IActionResult CreateCoditechApplicationSetting([FromBody] CoditechApplicationSettingModel model)
        {
            try
            {
                CoditechApplicationSettingModel coditechApplicationSetting = _coditechApplicationSettingService.CreateCoditechApplicationSetting(model);
                return IsNotNull(coditechApplicationSetting) ? CreateCreatedResponse(new CoditechApplicationSettingResponse { CoditechApplicationSettingModel = coditechApplicationSetting }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new CoditechApplicationSettingResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new CoditechApplicationSettingResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/CoditechApplicationSetting/GetCoditechApplicationSetting")]
        [HttpGet]
        [Produces(typeof(CoditechApplicationSettingResponse))]
        public virtual IActionResult GetCoditechApplicationSetting(short coditechApplicationSettingId)
        {
            try
            {
                CoditechApplicationSettingModel coditechApplicationSettingModel = _coditechApplicationSettingService.GetCoditechApplicationSetting(coditechApplicationSettingId);
                return IsNotNull(coditechApplicationSettingModel) ? CreateOKResponse(new CoditechApplicationSettingResponse { CoditechApplicationSettingModel = coditechApplicationSettingModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new CoditechApplicationSettingResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new CoditechApplicationSettingResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/CoditechApplicationSetting/UpdateCoditechApplicationSetting")]
        [HttpPut, ValidateModel]
        [Produces(typeof(CoditechApplicationSettingResponse))]
        public virtual IActionResult UpdateCoditechApplicationSetting([FromBody] CoditechApplicationSettingModel model)
        {
            try
            {
                bool isUpdated = _coditechApplicationSettingService.UpdateCoditechApplicationSetting(model);
                return isUpdated ? CreateOKResponse(new CoditechApplicationSettingResponse { CoditechApplicationSettingModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new CoditechApplicationSettingResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new CoditechApplicationSettingResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/CoditechApplicationSetting/DeleteCoditechApplicationSetting")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteCoditechApplicationSetting([FromBody] ParameterModel coditechApplicationSettingIds)
        {
            try
            {
                bool deleted = _coditechApplicationSettingService.DeleteCoditechApplicationSetting(coditechApplicationSettingIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
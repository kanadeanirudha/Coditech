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
    public class GeneralSystemGlobleSettingController : BaseController
    {
        private readonly IGeneralSystemGlobleSettingService _generalSystemGlobleSettingService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralSystemGlobleSettingController(ICoditechLogging coditechLogging, IGeneralSystemGlobleSettingService generalSystemGlobleSettingService)
        {
            _generalSystemGlobleSettingService = generalSystemGlobleSettingService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralSystemGlobleSetting/GetSystemGlobleSettingList")]
        [Produces(typeof(GeneralSystemGlobleSettingListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetSystemGlobleSettingList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralSystemGlobleSettingListModel list = _generalSystemGlobleSettingService.GetSystemGlobleSettingList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralSystemGlobleSettingListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralSystemGlobleSettingListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralSystemGlobleSettingListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralSystemGlobleSetting/CreateSystemGlobleSetting")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralSystemGlobleSettingResponse))]
        public virtual IActionResult CreateSystemGlobleSetting([FromBody] GeneralSystemGlobleSettingModel model)
        {
            try
            {
                GeneralSystemGlobleSettingModel systemGlobleSettingMaster = _generalSystemGlobleSettingService.CreateSystemGlobleSetting(model);
                return IsNotNull(systemGlobleSettingMaster) ? CreateCreatedResponse(new GeneralSystemGlobleSettingResponse { GeneralSystemGlobleSettingModel = systemGlobleSettingMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralSystemGlobleSettingResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralSystemGlobleSettingResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralSystemGlobleSetting/GetSystemGlobleSetting")]
        [HttpGet]
        [Produces(typeof(GeneralSystemGlobleSettingResponse))]
        public virtual IActionResult GetSystemGlobleSetting(short generalSystemGlobleSettingId)
        {
            try
            {
                GeneralSystemGlobleSettingModel generalSystemGlobleSettingModel = _generalSystemGlobleSettingService.GetSystemGlobleSetting(generalSystemGlobleSettingId);
                return IsNotNull(generalSystemGlobleSettingModel) ? CreateOKResponse(new GeneralSystemGlobleSettingResponse { GeneralSystemGlobleSettingModel = generalSystemGlobleSettingModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralSystemGlobleSettingResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralSystemGlobleSettingResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralSystemGlobleSetting/UpdateSystemGlobleSetting")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralSystemGlobleSettingResponse))]
        public virtual IActionResult UpdateSystemGlobleSetting([FromBody] GeneralSystemGlobleSettingModel model)
        {
            try
            {
                bool isUpdated = _generalSystemGlobleSettingService.UpdateSystemGlobleSetting(model);
                return isUpdated ? CreateOKResponse(new GeneralSystemGlobleSettingResponse { GeneralSystemGlobleSettingModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralSystemGlobleSettingResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralSystemGlobleSettingResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralSystemGlobleSetting/DeleteSystemGlobleSetting")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteSystemGlobleSetting([FromBody] ParameterModel generalSystemGlobleSettingIds)
        {
            try
            {
                bool deleted = _generalSystemGlobleSettingService.DeleteSystemGlobleSetting(generalSystemGlobleSettingIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
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
    public class MediaSettingMasterController : BaseController
    {
        private readonly IMediaSettingMasterService _mediaSettingMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public MediaSettingMasterController(ICoditechLogging coditechLogging, IMediaSettingMasterService mediaSettingMasterService)
        {
            _mediaSettingMasterService = mediaSettingMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/MediaSettingMaster/GetMediaSettingMasterList")]
        [Produces(typeof(MediaSettingMasterListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetMediaSettingMasterList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                MediaSettingMasterListModel list = _mediaSettingMasterService.GetMediaSettingMasterList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<MediaSettingMasterListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaSettingMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new MediaSettingMasterListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaSettingMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new MediaSettingMasterListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/MediaSettingMaster/GetMediaSettingMaster")]
        [HttpGet]
        [Produces(typeof(MediaSettingMasterResponse))]
        public virtual IActionResult GetMediaSettingMaster(byte mediaTypeMasterId)
        {
            try
            {
                MediaSettingMasterModel mediaSettingMasterModel = _mediaSettingMasterService.GetMediaSettingMaster(mediaTypeMasterId);
                return IsNotNull(mediaSettingMasterModel) ? CreateOKResponse(new MediaSettingMasterResponse { MediaSettingMasterModel = mediaSettingMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaSettingMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new MediaSettingMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaSettingMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new MediaSettingMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/MediaSettingMaster/UpdateMediaSettingMaster")]
        [HttpPut, ValidateModel]
        [Produces(typeof(MediaSettingMasterResponse))]
        public virtual IActionResult UpdateMediaSettingMaster([FromBody] MediaSettingMasterModel model)
        {
            try
            {
                bool isUpdated = _mediaSettingMasterService.UpdateMediaSettingMaster(model);
                return isUpdated ? CreateOKResponse(new MediaSettingMasterResponse { MediaSettingMasterModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaSettingMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new MediaSettingMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaSettingMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new MediaSettingMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/MediaSettingMaster/DeleteMediaSettingMaster")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteMediaSettingMaster([FromBody] ParameterModel mediasettingmasterIds)
        {
            try
            {
                bool deleted = _mediaSettingMasterService.DeleteMediaSettingMaster(mediasettingmasterIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaSettingMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaSettingMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
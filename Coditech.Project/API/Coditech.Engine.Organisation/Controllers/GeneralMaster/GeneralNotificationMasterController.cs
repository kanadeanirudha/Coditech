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
    public class GeneralNotificationMasterController : BaseController
    {
        private readonly IGeneralNotificationMasterService _generalNotificationService;
        private readonly ICoditechLogging _coditechLogging;
        protected readonly ICoditechLogging coditechLogging;
        public GeneralNotificationMasterController(ICoditechLogging coditechLogging, IGeneralNotificationMasterService generalNotificationMasterService)
        {
            _generalNotificationService = generalNotificationMasterService;
            _coditechLogging = coditechLogging;
        }
        [HttpGet]
        [Route("/GeneralNotificationMaster/GetNotificationList")]
        [Produces(typeof(GeneralNotificationListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetNotificationList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int PageIndex, int pazeSize)
        {
            try
            {
                GeneralNotificationListModel list = _generalNotificationService.GetNotificationList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), PageIndex, pazeSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralNotificationListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralNotificationListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralNotificationListResponse { HasError = true, ErrorMessage = ex.Message });
            }

        }

        [Route("/GeneralNotificationMaster/CreateNotification")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralNotificationListResponse))]

        public virtual IActionResult CreateNotification([FromBody] GeneralNotificationModel model)
        {
            try
            {
                GeneralNotificationModel NotificationMaster = _generalNotificationService.CreateNotification(model);
                return IsNotNull(NotificationMaster) ? CreateCreatedResponse(new GeneralNotificationResponse { GeneralNotificationModel = NotificationMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralNotificationResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralNotificationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/GeneralNotificationMaster/GetNotification")]
        [HttpGet]
        [Produces(typeof(GeneralNotificationResponse))]
        public virtual IActionResult GetNotification(long GeneralNotificationId)
        {

            try
            {
                GeneralNotificationModel generalNotificationModel = _generalNotificationService.GetNotification(GeneralNotificationId);
                return IsNotNull(generalNotificationModel) ? CreateOKResponse(new GeneralNotificationResponse { GeneralNotificationModel = generalNotificationModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralNotificationResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralNotificationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
         [Route("/GeneralNotificationMaster/UpdateNotification")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralNotificationResponse))]
        public virtual IActionResult UpdateNotification([FromBody] GeneralNotificationModel model)
        {
            try
            {
                bool isUpdated = _generalNotificationService.UpdateNotification(model);
                return isUpdated ? CreateOKResponse(new GeneralNotificationResponse { GeneralNotificationModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralNotificationResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralNotificationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/GeneralNotificationMaster/DeleteNotification")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteNotification([FromBody] ParameterModel NotificationIds)
        {
            try
            {
                bool deleted = _generalNotificationService.DeleteNotification(NotificationIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Error);
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("/GeneralNotificationMaster/GetActiveNotificationList")]
        [Produces(typeof(GeneralNotificationListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetActiveNotificationList()
        {
            try
            {
                GeneralNotificationListModel list = _generalNotificationService.GetActiveNotificationList();
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralNotificationListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralNotificationListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.NotificationMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralNotificationListResponse { HasError = true, ErrorMessage = ex.Message });
            }

        }
    }

}
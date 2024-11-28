using Coditech.API.Organisation.Service.Interface.Organisation;
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
    public class TaskApprovalSettingController : BaseController
    {
        private readonly ITaskApprovalSettingService _taskApprovalSettingService;
        protected readonly ICoditechLogging _coditechLogging;
        public TaskApprovalSettingController(ICoditechLogging coditechLogging, ITaskApprovalSettingService taskApprovalSettingService)
        {
            _taskApprovalSettingService = taskApprovalSettingService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/TaskApprovalSetting/GetTaskApprovalSettingList")]
        [Produces(typeof(TaskApprovalSettingListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetTaskApprovalSettingList(string selectedCentreCode,ExpandCollection expand, FilterCollection filter, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                TaskApprovalSettingListModel list = _taskApprovalSettingService.GetTaskApprovalSettingList(selectedCentreCode,filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<TaskApprovalSettingListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskApprovalSetting.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TaskApprovalSettingListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskApprovalSetting.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TaskApprovalSettingListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }


        [Route("/TaskApprovalSetting/GetTaskApprovalSetting")]
        [HttpGet]
        [Produces(typeof(TaskApprovalSettingResponse))]
        public virtual IActionResult GetTaskApprovalSetting(short taskMasterId, string centreCode)
        {
            try
            {
                TaskApprovalSettingModel taskApprovalSettingModel = _taskApprovalSettingService.GetTaskApprovalSetting(taskMasterId, centreCode);
                return IsNotNull(taskApprovalSettingModel) ? CreateOKResponse(new TaskApprovalSettingResponse { TaskApprovalSettingModel = taskApprovalSettingModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskApprovalSetting.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TaskApprovalSettingResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaskApprovalSetting.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TaskApprovalSettingResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }       
    }
}
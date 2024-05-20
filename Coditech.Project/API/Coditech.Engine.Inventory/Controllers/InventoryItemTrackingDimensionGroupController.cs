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
    public class InventoryItemTrackingDimensionGroupController : BaseController
    {
        private readonly IInventoryItemTrackingDimensionGroupService _inventoryItemTrackingDimensionGroupService;
        protected readonly ICoditechLogging _coditechLogging;
        public InventoryItemTrackingDimensionGroupController(ICoditechLogging coditechLogging, IInventoryItemTrackingDimensionGroupService inventoryItemTrackingDimensionGroupService)
        {
            _inventoryItemTrackingDimensionGroupService = inventoryItemTrackingDimensionGroupService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/InventoryItemTrackingDimensionGroup/GetInventoryItemTrackingDimensionGroupList")]
        [Produces(typeof(InventoryItemTrackingDimensionGroupListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetInventoryItemTrackingDimensionGroupList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                InventoryItemTrackingDimensionGroupListModel list = _inventoryItemTrackingDimensionGroupService.GetInventoryItemTrackingDimensionGroupList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<InventoryItemTrackingDimensionGroupListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemTrackingDimensionGroupListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemTrackingDimensionGroupListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemTrackingDimensionGroup/CreateInventoryItemTrackingDimensionGroup")]
        [HttpPost, ValidateModel]
        [Produces(typeof(InventoryItemTrackingDimensionGroupResponse))]
        public virtual IActionResult CreateInventoryItemTrackingDimensionGroup([FromBody] InventoryItemTrackingDimensionGroupModel model)
        {
            try
            {
                InventoryItemTrackingDimensionGroupModel inventoryItemTrackingDimensionGroup = _inventoryItemTrackingDimensionGroupService.CreateInventoryItemTrackingDimensionGroup(model);
                return IsNotNull(inventoryItemTrackingDimensionGroup) ? CreateCreatedResponse(new InventoryItemTrackingDimensionGroupResponse { InventoryItemTrackingDimensionGroupModel = inventoryItemTrackingDimensionGroup }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryItemTrackingDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemTrackingDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemTrackingDimensionGroup/GetInventoryItemTrackingDimensionGroup")]
        [HttpGet]
        [Produces(typeof(InventoryItemTrackingDimensionGroupResponse))]
        public virtual IActionResult GetInventoryItemTrackingDimensionGroup(int inventoryItemTrackingDimensionGroupId)
        {
            try
            {
                InventoryItemTrackingDimensionGroupModel inventoryItemTrackingDimensionGroupModel = _inventoryItemTrackingDimensionGroupService.GetInventoryItemTrackingDimensionGroup(inventoryItemTrackingDimensionGroupId);
                return IsNotNull(inventoryItemTrackingDimensionGroupModel) ? CreateOKResponse(new InventoryItemTrackingDimensionGroupResponse { InventoryItemTrackingDimensionGroupModel = inventoryItemTrackingDimensionGroupModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryItemTrackingDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemTrackingDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemTrackingDimensionGroup/UpdateInventoryItemTrackingDimensionGroup")]
        [HttpPut, ValidateModel]
        [Produces(typeof(InventoryItemTrackingDimensionGroupResponse))]
        public virtual IActionResult UpdateInventoryItemTrackingDimensionGroup([FromBody] InventoryItemTrackingDimensionGroupModel model)
        {
            try
            {
                bool isUpdated = _inventoryItemTrackingDimensionGroupService.UpdateInventoryItemTrackingDimensionGroup(model);
                return isUpdated ? CreateOKResponse(new InventoryItemTrackingDimensionGroupResponse { InventoryItemTrackingDimensionGroupModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryItemTrackingDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemTrackingDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemTrackingDimensionGroup/DeleteInventoryItemTrackingDimensionGroup")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteInventoryItemTrackingDimensionGroup([FromBody] ParameterModel inventoryItemTrackingDimensionGroupIds)
        {
            try
            {
                bool deleted = _inventoryItemTrackingDimensionGroupService.DeleteInventoryItemTrackingDimensionGroup(inventoryItemTrackingDimensionGroupIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
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
    public class InventoryProductDimensionGroupController : BaseController
    {
        private readonly IInventoryProductDimensionGroupService _inventoryProductDimensionGroupService;
        protected readonly ICoditechLogging _coditechLogging;
        public InventoryProductDimensionGroupController(ICoditechLogging coditechLogging, IInventoryProductDimensionGroupService inventoryProductDimensionGroupService)
        {
            _inventoryProductDimensionGroupService = inventoryProductDimensionGroupService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/InventoryProductDimensionGroup/GetInventoryProductDimensionGroupList")]
        [Produces(typeof(InventoryProductDimensionGroupListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetInventoryProductDimensionGroupList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                InventoryProductDimensionGroupListModel list = _inventoryProductDimensionGroupService.GetInventoryProductDimensionGroupList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<InventoryProductDimensionGroupListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryProductDimensionGroupListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryProductDimensionGroupListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryProductDimensionGroup/CreateInventoryProductDimensionGroup")]
        [HttpPost, ValidateModel]
        [Produces(typeof(InventoryProductDimensionGroupResponse))]
        public virtual IActionResult CreateInventoryProductDimensionGroup([FromBody] InventoryProductDimensionGroupModel model)
        {
            try
            {
                InventoryProductDimensionGroupModel inventoryProductDimensionGroup = _inventoryProductDimensionGroupService.CreateInventoryProductDimensionGroup(model);
                return IsNotNull(inventoryProductDimensionGroup) ? CreateCreatedResponse(new InventoryProductDimensionGroupResponse { InventoryProductDimensionGroupModel = inventoryProductDimensionGroup }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryProductDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryProductDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryProductDimensionGroup/GetInventoryProductDimensionGroup")]
        [HttpGet]
        [Produces(typeof(InventoryProductDimensionGroupResponse))]
        public virtual IActionResult GetInventoryProductDimensionGroup(int inventoryProductDimensionGroupId)
        {
            try
            {
                InventoryProductDimensionGroupModel inventoryProductDimensionGroupModel = _inventoryProductDimensionGroupService.GetInventoryProductDimensionGroup(inventoryProductDimensionGroupId);
                return IsNotNull(inventoryProductDimensionGroupModel) ? CreateOKResponse(new InventoryProductDimensionGroupResponse { InventoryProductDimensionGroupModel = inventoryProductDimensionGroupModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryProductDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryProductDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryProductDimensionGroup/UpdateInventoryProductDimensionGroup")]
        [HttpPut, ValidateModel]
        [Produces(typeof(InventoryProductDimensionGroupResponse))]
        public virtual IActionResult UpdateInventoryProductDimensionGroup([FromBody] InventoryProductDimensionGroupModel model)
        {
            try
            {
                bool isUpdated = _inventoryProductDimensionGroupService.UpdateInventoryProductDimensionGroup(model);
                return isUpdated ? CreateOKResponse(new InventoryProductDimensionGroupResponse { InventoryProductDimensionGroupModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryProductDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryProductDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryProductDimensionGroup/DeleteInventoryProductDimensionGroup")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteInventoryProductDimensionGroup([FromBody] ParameterModel inventoryProductDimensionGroupIds)
        {
            try
            {
                bool deleted = _inventoryProductDimensionGroupService.DeleteInventoryProductDimensionGroup(inventoryProductDimensionGroupIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
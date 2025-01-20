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
    public class InventoryStorageDimensionGroupController : BaseController
    {
        private readonly IInventoryStorageDimensionGroupService _inventoryStorageDimensionGroupService;
        protected readonly ICoditechLogging _coditechLogging;
        public InventoryStorageDimensionGroupController(ICoditechLogging coditechLogging, IInventoryStorageDimensionGroupService inventoryStorageDimensionGroupService)
        {
            _inventoryStorageDimensionGroupService = inventoryStorageDimensionGroupService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/InventoryStorageDimensionGroup/GetInventoryStorageDimensionGroupList")]
        [Produces(typeof(InventoryStorageDimensionGroupListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetInventoryStorageDimensionGroupList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                InventoryStorageDimensionGroupListModel list = _inventoryStorageDimensionGroupService.GetInventoryStorageDimensionGroupList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<InventoryStorageDimensionGroupListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryStorageDimensionGroupListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryStorageDimensionGroupListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryStorageDimensionGroup/CreateInventoryStorageDimensionGroup")]
        [HttpPost, ValidateModel]
        [Produces(typeof(InventoryStorageDimensionGroupResponse))]
        public virtual IActionResult CreateInventoryStorageDimensionGroup([FromBody] InventoryStorageDimensionGroupModel model)
        {
            try
            {
                InventoryStorageDimensionGroupModel inventoryStorageDimensionGroup = _inventoryStorageDimensionGroupService.CreateInventoryStorageDimensionGroup(model);
                return IsNotNull(inventoryStorageDimensionGroup) ? CreateCreatedResponse(new InventoryStorageDimensionGroupResponse { InventoryStorageDimensionGroupModel = inventoryStorageDimensionGroup }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryStorageDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryStorageDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryStorageDimensionGroup/GetInventoryStorageDimensionGroup")]
        [HttpGet]
        [Produces(typeof(InventoryStorageDimensionGroupResponse))]
        public virtual IActionResult GetInventoryStorageDimensionGroup(int inventoryStorageDimensionGroupId)
        {
            try
            {
                InventoryStorageDimensionGroupModel inventoryStorageDimensionGroupModel = _inventoryStorageDimensionGroupService.GetInventoryStorageDimensionGroup(inventoryStorageDimensionGroupId);
                return IsNotNull(inventoryStorageDimensionGroupModel) ? CreateOKResponse(new InventoryStorageDimensionGroupResponse { InventoryStorageDimensionGroupModel = inventoryStorageDimensionGroupModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryStorageDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryStorageDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryStorageDimensionGroup/UpdateInventoryStorageDimensionGroup")]
        [HttpPut, ValidateModel]
        [Produces(typeof(InventoryStorageDimensionGroupResponse))]
        public virtual IActionResult UpdateInventoryStorageDimensionGroup([FromBody] InventoryStorageDimensionGroupModel model)
        {
            try
            {
                bool isUpdated = _inventoryStorageDimensionGroupService.UpdateInventoryStorageDimensionGroup(model);
                return isUpdated ? CreateOKResponse(new InventoryStorageDimensionGroupResponse { InventoryStorageDimensionGroupModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryStorageDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryStorageDimensionGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryStorageDimensionGroup/DeleteInventoryStorageDimensionGroup")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteInventoryStorageDimensionGroup([FromBody] ParameterModel inventoryStorageDimensionGroupIds)
        {
            try
            {
                bool deleted = _inventoryStorageDimensionGroupService.DeleteInventoryStorageDimensionGroup(inventoryStorageDimensionGroupIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryStorageDimensionGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
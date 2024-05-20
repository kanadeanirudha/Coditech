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
    public class InventoryItemGroupController : BaseController
    {
        private readonly IInventoryItemGroupService _inventoryItemGroupService;
        protected readonly ICoditechLogging _coditechLogging;
        public InventoryItemGroupController(ICoditechLogging coditechLogging, IInventoryItemGroupService inventoryItemGroupService)
        {
            _inventoryItemGroupService = inventoryItemGroupService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/InventoryItemGroup/GetInventoryItemGroupList")]
        [Produces(typeof(InventoryItemGroupListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetInventoryItemGroupList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                InventoryItemGroupListModel list = _inventoryItemGroupService.GetInventoryItemGroupList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<InventoryItemGroupListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemGroupListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemGroupListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemGroup/CreateInventoryItemGroup")]
        [HttpPost, ValidateModel]
        [Produces(typeof(InventoryItemGroupResponse))]
        public virtual IActionResult CreateInventoryItemGroup([FromBody] InventoryItemGroupModel model)
        {
            try
            {
                InventoryItemGroupModel InventoryItemGroup = _inventoryItemGroupService.CreateInventoryItemGroup(model);
                return IsNotNull(InventoryItemGroup) ? CreateCreatedResponse(new InventoryItemGroupResponse { InventoryItemGroupModel = InventoryItemGroup }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryItemGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemGroup/GetInventoryItemGroup")]
        [HttpGet]
        [Produces(typeof(InventoryItemGroupResponse))]
        public virtual IActionResult GetInventoryItemGroup(short inventoryItemGroupId)
        {
            try
            {
                InventoryItemGroupModel InventoryItemGroupModel = _inventoryItemGroupService.GetInventoryItemGroup(inventoryItemGroupId);
                return IsNotNull(InventoryItemGroupModel) ? CreateOKResponse(new InventoryItemGroupResponse { InventoryItemGroupModel = InventoryItemGroupModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryItemGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemGroup/UpdateInventoryItemGroup")]
        [HttpPut, ValidateModel]
        [Produces(typeof(InventoryItemGroupResponse))]
        public virtual IActionResult UpdateInventoryItemGroup([FromBody] InventoryItemGroupModel model)
        {
            try
            {
                bool isUpdated = _inventoryItemGroupService.UpdateInventoryItemGroup(model);
                return isUpdated ? CreateOKResponse(new InventoryItemGroupResponse { InventoryItemGroupModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryItemGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemGroup/DeleteInventoryItemGroup")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteInventoryItemGroup([FromBody] ParameterModel inventoryItemGroupIds)
        {
            try
            {
                bool deleted = _inventoryItemGroupService.DeleteInventoryItemGroup(inventoryItemGroupIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
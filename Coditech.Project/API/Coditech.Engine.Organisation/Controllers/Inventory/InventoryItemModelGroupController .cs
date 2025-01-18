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
    public class InventoryItemModelGroupController : BaseController
    {
        private readonly IInventoryItemModelGroupService _inventoryItemModelGroupService;
        protected readonly ICoditechLogging _coditechLogging;
        public InventoryItemModelGroupController(ICoditechLogging coditechLogging, IInventoryItemModelGroupService inventoryItemModelGroupService)
        {
            _inventoryItemModelGroupService = inventoryItemModelGroupService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/InventoryItemModelGroup/GetInventoryItemModelGroupList")]
        [Produces(typeof(InventoryItemModelGroupListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetInventoryItemModelGroupList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                InventoryItemModelGroupListModel list = _inventoryItemModelGroupService.GetInventoryItemModelGroupList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<InventoryItemModelGroupListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemModelGroupListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemModelGroupListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemModelGroup/CreateInventoryItemModelGroup")]
        [HttpPost, ValidateModel]
        [Produces(typeof(InventoryItemModelGroupResponse))]
        public virtual IActionResult CreateInventoryItemModelGroup([FromBody] InventoryItemModelGroupModel model)
        {
            try
            {
                InventoryItemModelGroupModel InventoryItemModelGroup = _inventoryItemModelGroupService.CreateInventoryItemModelGroup(model);
                return IsNotNull(InventoryItemModelGroup) ? CreateCreatedResponse(new InventoryItemModelGroupResponse { InventoryItemModelGroupModel = InventoryItemModelGroup }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryItemModelGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemModelGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemModelGroup/GetInventoryItemModelGroup")]
        [HttpGet]
        [Produces(typeof(InventoryItemModelGroupResponse))]
        public virtual IActionResult GetInventoryItemModelGroup(short inventoryItemModelGroupId)
        {
            try
            {
                InventoryItemModelGroupModel inventoryItemModelGroupModel = _inventoryItemModelGroupService.GetInventoryItemModelGroup(inventoryItemModelGroupId);
                return IsNotNull(inventoryItemModelGroupModel) ? CreateOKResponse(new InventoryItemModelGroupResponse { InventoryItemModelGroupModel = inventoryItemModelGroupModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryItemModelGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemModelGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemModelGroup/UpdateInventoryItemModelGroup")]
        [HttpPut, ValidateModel]
        [Produces(typeof(InventoryItemModelGroupResponse))]
        public virtual IActionResult UpdateInventoryItemModelGroup([FromBody] InventoryItemModelGroupModel model)
        {
            try
            {
                bool isUpdated = _inventoryItemModelGroupService.UpdateInventoryItemModelGroup(model);
                return isUpdated ? CreateOKResponse(new InventoryItemModelGroupResponse { InventoryItemModelGroupModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryItemModelGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemModelGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemModelGroup/DeleteInventoryItemModelGroup")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteInventoryItemModelGroup([FromBody] ParameterModel inventoryItemModelGroupIds)
        {
            try
            {
                bool deleted = _inventoryItemModelGroupService.DeleteInventoryItemModelGroup(inventoryItemModelGroupIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemModelGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
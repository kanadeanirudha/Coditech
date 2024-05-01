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
    public class InventoryItemTrackingDimensionController : BaseController
    {
        private readonly IInventoryItemTrackingDimensionService _inventoryItemTrackingDimensionService;
        protected readonly ICoditechLogging _coditechLogging;
        public InventoryItemTrackingDimensionController(ICoditechLogging coditechLogging, IInventoryItemTrackingDimensionService inventoryItemTrackingDimensionService)
        {
            _inventoryItemTrackingDimensionService = inventoryItemTrackingDimensionService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/InventoryItemTrackingDimension/GetInventoryItemTrackingDimensionList")]
        [Produces(typeof(InventoryItemTrackingDimensionListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetInventoryItemTrackingDimensionList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                InventoryItemTrackingDimensionListModel list = _inventoryItemTrackingDimensionService.GetInventoryItemTrackingDimensionList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<InventoryItemTrackingDimensionListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemTrackingDimensionListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemTrackingDimensionListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemTrackingDimension/CreateInventoryItemTrackingDimension")]
        [HttpPost, ValidateModel]
        [Produces(typeof(InventoryItemTrackingDimensionResponse))]
        public virtual IActionResult CreateInventoryItemTrackingDimension([FromBody] InventoryItemTrackingDimensionModel model)
        {
            try
            {
                InventoryItemTrackingDimensionModel InventoryItemTrackingDimension = _inventoryItemTrackingDimensionService.CreateInventoryItemTrackingDimension(model);
                return IsNotNull(InventoryItemTrackingDimension) ? CreateCreatedResponse(new InventoryItemTrackingDimensionResponse { InventoryItemTrackingDimensionModel = InventoryItemTrackingDimension }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryItemTrackingDimensionResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemTrackingDimensionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemTrackingDimension/GetInventoryItemTrackingDimension")]
        [HttpGet]
        [Produces(typeof(InventoryItemTrackingDimensionResponse))]
        public virtual IActionResult GetInventoryItemTrackingDimension(short inventoryItemTrackingDimensionId)
        {
            try
            {
                InventoryItemTrackingDimensionModel InventoryItemTrackingDimensionModel = _inventoryItemTrackingDimensionService.GetInventoryItemTrackingDimension(inventoryItemTrackingDimensionId);
                return IsNotNull(InventoryItemTrackingDimensionModel) ? CreateOKResponse(new InventoryItemTrackingDimensionResponse { InventoryItemTrackingDimensionModel = InventoryItemTrackingDimensionModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryItemTrackingDimensionResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemTrackingDimensionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemTrackingDimension/UpdateInventoryItemTrackingDimension")]
        [HttpPut, ValidateModel]
        [Produces(typeof(InventoryItemTrackingDimensionResponse))]
        public virtual IActionResult UpdateInventoryItemTrackingDimension([FromBody] InventoryItemTrackingDimensionModel model)
        {
            try
            {
                bool isUpdated = _inventoryItemTrackingDimensionService.UpdateInventoryItemTrackingDimension(model);
                return isUpdated ? CreateOKResponse(new InventoryItemTrackingDimensionResponse { InventoryItemTrackingDimensionModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryItemTrackingDimensionResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemTrackingDimensionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemTrackingDimension/DeleteInventoryItemTrackingDimension")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteInventoryItemTrackingDimension([FromBody] ParameterModel inventoryItemTrackingDimensionIds)
        {
            try
            {
                bool deleted = _inventoryItemTrackingDimensionService.DeleteInventoryItemTrackingDimension(inventoryItemTrackingDimensionIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemTrackingDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
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
    public class InventoryItemStorageDimensionController : BaseController
    {
        private readonly IInventoryItemStorageDimensionService _inventoryItemStorageDimensionService;
        protected readonly ICoditechLogging _coditechLogging;
        public InventoryItemStorageDimensionController(ICoditechLogging coditechLogging, IInventoryItemStorageDimensionService inventoryItemStorageDimensionService)
        {
            _inventoryItemStorageDimensionService = inventoryItemStorageDimensionService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/InventoryItemStorageDimension/GetInventoryItemStorageDimensionList")]
        [Produces(typeof(InventoryItemStorageDimensionListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetInventoryItemStorageDimensionList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                InventoryItemStorageDimensionListModel list = _inventoryItemStorageDimensionService.GetInventoryItemStorageDimensionList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<InventoryItemStorageDimensionListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemStorageDimensionListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemStorageDimensionListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemStorageDimension/CreateInventoryItemStorageDimension")]
        [HttpPost, ValidateModel]
        [Produces(typeof(InventoryItemStorageDimensionResponse))]
        public virtual IActionResult CreateInventoryItemStorageDimension([FromBody] InventoryItemStorageDimensionModel model)
        {
            try
            {
                InventoryItemStorageDimensionModel InventoryItemStorageDimension = _inventoryItemStorageDimensionService.CreateInventoryItemStorageDimension(model);
                return IsNotNull(InventoryItemStorageDimension) ? CreateCreatedResponse(new InventoryItemStorageDimensionResponse { InventoryItemStorageDimensionModel = InventoryItemStorageDimension }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryItemStorageDimensionResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemStorageDimensionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemStorageDimension/GetInventoryItemStorageDimension")]
        [HttpGet]
        [Produces(typeof(InventoryItemStorageDimensionResponse))]
        public virtual IActionResult GetInventoryItemStorageDimension(short inventoryItemStorageDimensionId)
        {
            try
            {
                InventoryItemStorageDimensionModel InventoryItemStorageDimensionModel = _inventoryItemStorageDimensionService.GetInventoryItemStorageDimension(inventoryItemStorageDimensionId);
                return IsNotNull(InventoryItemStorageDimensionModel) ? CreateOKResponse(new InventoryItemStorageDimensionResponse { InventoryItemStorageDimensionModel = InventoryItemStorageDimensionModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryItemStorageDimensionResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemStorageDimensionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemStorageDimension/UpdateInventoryItemStorageDimension")]
        [HttpPut, ValidateModel]
        [Produces(typeof(InventoryItemStorageDimensionResponse))]
        public virtual IActionResult UpdateInventoryItemStorageDimension([FromBody] InventoryItemStorageDimensionModel model)
        {
            try
            {
                bool isUpdated = _inventoryItemStorageDimensionService.UpdateInventoryItemStorageDimension(model);
                return isUpdated ? CreateOKResponse(new InventoryItemStorageDimensionResponse { InventoryItemStorageDimensionModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryItemStorageDimensionResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryItemStorageDimensionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryItemStorageDimension/DeleteInventoryItemStorageDimension")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteInventoryItemStorageDimension([FromBody] ParameterModel inventoryItemStorageDimensionIds)
        {
            try
            {
                bool deleted = _inventoryItemStorageDimensionService.DeleteInventoryItemStorageDimension(inventoryItemStorageDimensionIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryItemStorageDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
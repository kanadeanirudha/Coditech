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
    public class InventoryProductDimensionController : BaseController
    {
        private readonly IInventoryProductDimensionService _inventoryProductDimensionService;
        protected readonly ICoditechLogging _coditechLogging;
        public InventoryProductDimensionController(ICoditechLogging coditechLogging, IInventoryProductDimensionService inventoryProductDimensionService)
        {
            _inventoryProductDimensionService = inventoryProductDimensionService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/InventoryProductDimension/GetInventoryProductDimensionList")]
        [Produces(typeof(InventoryProductDimensionListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetInventoryProductDimensionList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                InventoryProductDimensionListModel list = _inventoryProductDimensionService.GetInventoryProductDimensionList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<InventoryProductDimensionListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryProductDimensionListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryProductDimensionListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryProductDimension/CreateInventoryProductDimension")]
        [HttpPost, ValidateModel]
        [Produces(typeof(InventoryProductDimensionResponse))]
        public virtual IActionResult CreateInventoryProductDimension([FromBody] InventoryProductDimensionModel model)
        {
            try
            {
                InventoryProductDimensionModel InventoryProductDimension = _inventoryProductDimensionService.CreateInventoryProductDimension(model);
                return IsNotNull(InventoryProductDimension) ? CreateCreatedResponse(new InventoryProductDimensionResponse { InventoryProductDimensionModel = InventoryProductDimension }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryProductDimensionResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryProductDimensionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryProductDimension/GetInventoryProductDimension")]
        [HttpGet]
        [Produces(typeof(InventoryProductDimensionResponse))]
        public virtual IActionResult GetInventoryProductDimension(short inventoryProductDimensionId)
        {
            try
            {
                InventoryProductDimensionModel InventoryProductDimensionModel = _inventoryProductDimensionService.GetInventoryProductDimension(inventoryProductDimensionId);
                return IsNotNull(InventoryProductDimensionModel) ? CreateOKResponse(new InventoryProductDimensionResponse { InventoryProductDimensionModel = InventoryProductDimensionModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryProductDimensionResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryProductDimensionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryProductDimension/UpdateInventoryProductDimension")]
        [HttpPut, ValidateModel]
        [Produces(typeof(InventoryProductDimensionResponse))]
        public virtual IActionResult UpdateInventoryProductDimension([FromBody] InventoryProductDimensionModel model)
        {
            try
            {
                bool isUpdated = _inventoryProductDimensionService.UpdateInventoryProductDimension(model);
                return isUpdated ? CreateOKResponse(new InventoryProductDimensionResponse { InventoryProductDimensionModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryProductDimensionResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryProductDimensionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryProductDimension/DeleteInventoryProductDimension")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteInventoryProductDimension([FromBody] ParameterModel inventoryProductDimensionIds)
        {
            try
            {
                bool deleted = _inventoryProductDimensionService.DeleteInventoryProductDimension(inventoryProductDimensionIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryProductDimension.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
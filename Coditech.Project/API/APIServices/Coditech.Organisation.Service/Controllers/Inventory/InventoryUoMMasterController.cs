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
    public class InventoryUoMMasterController : BaseController
    {
        private readonly IInventoryUoMMasterService _inventoryUoMMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public InventoryUoMMasterController(ICoditechLogging coditechLogging, IInventoryUoMMasterService inventoryUoMMasterService)
        {
            _inventoryUoMMasterService = inventoryUoMMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/InventoryUoMMaster/GetInventoryUoMMasterList")]
        [Produces(typeof(InventoryUoMMasterListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetInventoryUoMMasterList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                InventoryUoMMasterListModel list = _inventoryUoMMasterService.GetInventoryUoMMasterList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<InventoryUoMMasterListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryUoMMasterListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryUoMMasterListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryUoMMaster/CreateInventoryUoMMaster")]
        [HttpPost, ValidateModel]
        [Produces(typeof(InventoryUoMMasterResponse))]
        public virtual IActionResult CreateInventoryUoMMaster([FromBody] InventoryUoMMasterModel model)
        {
            try
            {
                InventoryUoMMasterModel InventoryUoMMaster = _inventoryUoMMasterService.CreateInventoryUoMMaster(model);
                return IsNotNull(InventoryUoMMaster) ? CreateCreatedResponse(new InventoryUoMMasterResponse { InventoryUoMMasterModel = InventoryUoMMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryUoMMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryUoMMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryUoMMaster/GetInventoryUoMMaster")]
        [HttpGet]
        [Produces(typeof(InventoryUoMMasterResponse))]
        public virtual IActionResult GetInventoryUoMMaster(short inventoryUoMMasterId)
        {
            try
            {
                InventoryUoMMasterModel InventoryUoMMasterModel = _inventoryUoMMasterService.GetInventoryUoMMaster(inventoryUoMMasterId);
                return IsNotNull(InventoryUoMMasterModel) ? CreateOKResponse(new InventoryUoMMasterResponse { InventoryUoMMasterModel = InventoryUoMMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryUoMMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryUoMMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryUoMMaster/UpdateInventoryUoMMaster")]
        [HttpPut, ValidateModel]
        [Produces(typeof(InventoryUoMMasterResponse))]
        public virtual IActionResult UpdateInventoryUoMMaster([FromBody] InventoryUoMMasterModel model)
        {
            try
            {
                bool isUpdated = _inventoryUoMMasterService.UpdateInventoryUoMMaster(model);
                return isUpdated ? CreateOKResponse(new InventoryUoMMasterResponse { InventoryUoMMasterModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryUoMMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryUoMMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryUoMMaster/DeleteInventoryUoMMaster")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteInventoryUoMMaster([FromBody] ParameterModel inventoryUoMMasterIds)
        {
            try
            {
                bool deleted = _inventoryUoMMasterService.DeleteInventoryUoMMaster(inventoryUoMMasterIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryUoMMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
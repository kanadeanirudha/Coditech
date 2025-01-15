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
    public class InventoryCategoryController : BaseController
    {
        private readonly IInventoryCategoryService _inventoryCategoryService;
        protected readonly ICoditechLogging _coditechLogging;
        public InventoryCategoryController(ICoditechLogging coditechLogging, IInventoryCategoryService inventoryCategoryService)
        {
            _inventoryCategoryService = inventoryCategoryService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/InventoryCategory/GetInventoryCategoryList")]
        [Produces(typeof(InventoryCategoryListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetInventoryCategoryList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                InventoryCategoryListModel list = _inventoryCategoryService.GetInventoryCategoryList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<InventoryCategoryListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryCategoryListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryCategoryListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryCategory/CreateInventoryCategory")]
        [HttpPost, ValidateModel]
        [Produces(typeof(InventoryCategoryResponse))]
        public virtual IActionResult CreateInventoryCategory([FromBody] InventoryCategoryModel model)
        {
            try
            {
                InventoryCategoryModel InventoryCategory = _inventoryCategoryService.CreateInventoryCategory(model);
                return IsNotNull(InventoryCategory) ? CreateCreatedResponse(new InventoryCategoryResponse { InventoryCategoryModel = InventoryCategory }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryCategoryResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryCategoryResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryCategory/GetInventoryCategory")]
        [HttpGet]
        [Produces(typeof(InventoryCategoryResponse))]
        public virtual IActionResult GetInventoryCategory(short inventoryCategoryId)
        {
            try
            {
                InventoryCategoryModel InventoryCategoryModel = _inventoryCategoryService.GetInventoryCategory(inventoryCategoryId);
                return IsNotNull(InventoryCategoryModel) ? CreateOKResponse(new InventoryCategoryResponse { InventoryCategoryModel = InventoryCategoryModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryCategoryResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryCategoryResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryCategory/UpdateInventoryCategory")]
        [HttpPut, ValidateModel]
        [Produces(typeof(InventoryCategoryResponse))]
        public virtual IActionResult UpdateInventoryCategory([FromBody] InventoryCategoryModel model)
        {
            try
            {
                bool isUpdated = _inventoryCategoryService.UpdateInventoryCategory(model);
                return isUpdated ? CreateOKResponse(new InventoryCategoryResponse { InventoryCategoryModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryCategoryResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryCategoryResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryCategory/DeleteInventoryCategory")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteInventoryCategory([FromBody] ParameterModel inventoryCategoryIds)
        {
            try
            {
                bool deleted = _inventoryCategoryService.DeleteInventoryCategory(inventoryCategoryIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategory.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
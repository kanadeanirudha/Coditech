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
    public class InventoryCategoryTypeController : BaseController
    {
        private readonly IInventoryCategoryTypeService _inventoryCategoryTypeService;
        protected readonly ICoditechLogging _coditechLogging;
        public InventoryCategoryTypeController(ICoditechLogging coditechLogging, IInventoryCategoryTypeService inventoryCategoryTypeService)
        {
            _inventoryCategoryTypeService = inventoryCategoryTypeService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/InventoryCategoryType/GetInventoryCategoryTypeList")]
        [Produces(typeof(InventoryCategoryTypeListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetInventoryCategoryTypeList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                InventoryCategoryTypeListModel list = _inventoryCategoryTypeService.GetInventoryCategoryTypeList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<InventoryCategoryTypeListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryCategoryTypeListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryCategoryTypeListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/InventoryCategoryType/CreateInventoryCategoryType")]
        [HttpPost, ValidateModel]
        [Produces(typeof(InventoryCategoryTypeResponse))]
        public virtual IActionResult CreateInventoryCategoryType([FromBody] InventoryCategoryTypeModel model)
        {
            try
            {
                InventoryCategoryTypeModel inventoryCategoryType = _inventoryCategoryTypeService.CreateInventoryCategoryType(model);
                return IsNotNull(inventoryCategoryType) ? CreateCreatedResponse(new InventoryCategoryTypeResponse { InventoryCategoryTypeModel = inventoryCategoryType }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryCategoryTypeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryCategoryTypeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryCategoryType/GetInventoryCategoryType")]
        [HttpGet]
        [Produces(typeof(InventoryCategoryTypeResponse))]
        public virtual IActionResult GetInventoryCategoryType(byte inventoryCategoryTypeMasterId)
        {
            try
            {
                InventoryCategoryTypeModel inventoryCategoryTypeModel = _inventoryCategoryTypeService.GetInventoryCategoryType(inventoryCategoryTypeMasterId);
                return IsNotNull(inventoryCategoryTypeModel) ? CreateOKResponse(new InventoryCategoryTypeResponse { InventoryCategoryTypeModel = inventoryCategoryTypeModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryCategoryTypeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryCategoryTypeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryCategoryType/UpdateInventoryCategoryType")]
        [HttpPut, ValidateModel]
        [Produces(typeof(InventoryCategoryTypeResponse))]
        public virtual IActionResult UpdateInventoryCategoryType([FromBody] InventoryCategoryTypeModel model)
        {
            try
            {
                bool isUpdated = _inventoryCategoryTypeService.UpdateInventoryCategoryType(model);
                return isUpdated ? CreateOKResponse(new InventoryCategoryTypeResponse { InventoryCategoryTypeModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new InventoryCategoryTypeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new InventoryCategoryTypeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/InventoryCategoryType/DeleteInventoryCategoryType")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteInventoryCategoryType([FromBody] ParameterModel inventoryCategoryTypeMasterIds)
        {
            try
            {
                bool deleted = _inventoryCategoryTypeService.DeleteInventoryCategoryType(inventoryCategoryTypeMasterIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.InventoryCategoryType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
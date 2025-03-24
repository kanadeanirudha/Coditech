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
    }
}
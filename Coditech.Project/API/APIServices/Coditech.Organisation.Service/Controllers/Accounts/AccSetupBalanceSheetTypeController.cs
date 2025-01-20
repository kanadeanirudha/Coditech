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
    public class AccSetupBalanceSheetTypeController : BaseController
    {
        private readonly IAccSetupBalanceSheetTypeService _accSetupBalanceSheetTypeService;
        protected readonly ICoditechLogging _coditechLogging;
        public AccSetupBalanceSheetTypeController(ICoditechLogging coditechLogging, IAccSetupBalanceSheetTypeService accSetupBalanceSheetTypeService)
        {
            _accSetupBalanceSheetTypeService = accSetupBalanceSheetTypeService;
            _coditechLogging = coditechLogging;
        }
        [HttpGet]
        [Route("/AccSetupBalanceSheetType/AccSetupBalanceSheetTypeList")]
        [Produces(typeof(AccSetupBalanceSheetTypeListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetAccSetupBalanceSheetTypeList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                AccSetupBalanceSheetTypeListModel list = _accSetupBalanceSheetTypeService.GetAccSetupBalanceSheetTypeList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<AccSetupBalanceSheetTypeListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheetType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupBalanceSheetTypeListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheetType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupBalanceSheetTypeListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}

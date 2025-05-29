using System.Diagnostics;
using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Microsoft.AspNetCore.Mvc;
namespace Coditech.API.Controllers
{
    public class AccountReportController : BaseController
    {
        private readonly IAccountReportService _accountReportService;
        protected readonly ICoditechLogging _coditechLogging;
        public AccountReportController(ICoditechLogging coditechLogging, IAccountReportService accountReportService)
        {
            _accountReportService = accountReportService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/AccountReport/GetBalanceSheetReportList")]
        [Produces(typeof(AccountBalanceSheetReportListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetBalanceSheetReportList(string selectedCentreCode, string selectedParameter1, string selectedParameter2, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                AccountBalanceSheetReportListModel list = _accountReportService.GetBalanceSheetReportList(selectedCentreCode, selectedParameter1, selectedParameter2, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<AccountBalanceSheetReportListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, "Account GL", TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccountBalanceSheetReportListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, "Account GL", TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccountBalanceSheetReportListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
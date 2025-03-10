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
    public class AccSetupChartOfAccountTemplateController : BaseController
    {
        private readonly IAccSetupChartOfAccountTemplateService _accSetupChartOfAccountTemplateService;
        protected readonly ICoditechLogging _coditechLogging;
        public AccSetupChartOfAccountTemplateController(ICoditechLogging coditechLogging, IAccSetupChartOfAccountTemplateService accSetupChartOfAccountTemplateService)
        {
            _accSetupChartOfAccountTemplateService = accSetupChartOfAccountTemplateService;
            _coditechLogging = coditechLogging;
        }
        [HttpGet]
        [Route("/AccSetupChartOfAccountTemplate/AccSetupChartOfAccountTemplateList")]
        [Produces(typeof(AccSetupChartOfAccountTemplateListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetAccSetupChartOfAccountTemplateList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                AccSetupChartOfAccountTemplateListModel list = _accSetupChartOfAccountTemplateService.GetAccSetupChartOfAccountTemplateList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<AccSetupChartOfAccountTemplateListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupChartOfAccountTemplate.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupChartOfAccountTemplateListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupChartOfAccountTemplate.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupChartOfAccountTemplateListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}

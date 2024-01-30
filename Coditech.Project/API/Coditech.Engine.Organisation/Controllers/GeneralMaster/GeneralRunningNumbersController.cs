using Coditech.API.Organisation.Service.Interface.Organisation;
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

namespace Coditech.API.Controllers
{
    public class GeneralRunningNumbersController : BaseController
    {
        private readonly IGeneralRunningNumbersService _generalRunningNumbersService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralRunningNumbersController(ICoditechLogging coditechLogging, IGeneralRunningNumbersService generalRunningNumbersService)
        {
            _generalRunningNumbersService = generalRunningNumbersService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralRunningNumbers/GetRunningNumbersList")]
        [Produces(typeof(GeneralRunningNumbersListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetRunningNumbersList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralRunningNumbersListModel list = _generalRunningNumbersService.GetRunningNumbersList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralRunningNumbersListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralRunningNumbersListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralRunningNumbersListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

    }
}

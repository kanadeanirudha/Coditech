using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
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

        [Route("/GeneralRunningNumbers/CreateRunningNumbers")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralRunningNumbersResponse))]
        public virtual IActionResult CreateRunningNumbers([FromBody] GeneralRunningNumbersModel model)
        {
            try
            {
                GeneralRunningNumbersModel generalRunningNumbers = _generalRunningNumbersService.CreateRunningNumbers(model);
                return HelperUtility.IsNotNull(generalRunningNumbers) ? CreateCreatedResponse(new GeneralRunningNumbersResponse { GeneralRunningNumbersModel = generalRunningNumbers }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralRunningNumbersResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralRunningNumbersResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralRunningNumbers/GetRunningNumbers")]
        [HttpGet]
        [Produces(typeof(GeneralRunningNumbersResponse))]
        public virtual IActionResult GetRunningNumbers(long generalRunningNumberId)
        {
            try
            {
                GeneralRunningNumbersModel generalRunningNumbersModel = _generalRunningNumbersService.GetRunningNumbers(generalRunningNumberId);
                return HelperUtility.IsNotNull(generalRunningNumbersModel) ? CreateOKResponse(new GeneralRunningNumbersResponse { GeneralRunningNumbersModel = generalRunningNumbersModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralRunningNumbersResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralRunningNumbersResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralRunningNumbers/UpdateRunningNumbers")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralRunningNumbersResponse))]
        public virtual IActionResult UpdateRunningNumbers([FromBody] GeneralRunningNumbersModel model)
        {
            try
            {
                bool isUpdated = _generalRunningNumbersService.UpdateRunningNumbers(model);
                return isUpdated ? CreateOKResponse(new GeneralRunningNumbersResponse { GeneralRunningNumbersModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralRunningNumbersResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralRunningNumbersResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralRunningNumbers/DeleteRunningNumbers")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteRunningNumbers([FromBody] ParameterModel generalRunningNumberIds)
        {
            try
            {
                bool deleted = _generalRunningNumbersService.DeleteRunningNumbers(generalRunningNumberIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.RunningNumbers.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}

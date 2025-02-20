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
    public class GeneralCurrencyMasterController : BaseController
    {
        private readonly IGeneralCurrencyMasterService _generalCurrencyMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralCurrencyMasterController(ICoditechLogging coditechLogging, IGeneralCurrencyMasterService generalCurrencyMasterService)
        {
            _generalCurrencyMasterService = generalCurrencyMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralCurrencyMaster/GetCurrencyList")]
        [Produces(typeof(GeneralCurrencyMasterListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetCurrencyList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralCurrencyMasterListModel list = _generalCurrencyMasterService.GetCurrencyList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralCurrencyMasterListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCurrencyMasterListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCurrencyMasterListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralCurrencyMaster/CreateCurrency")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralCurrencyMasterResponse))]
        public virtual IActionResult CreateCurrency([FromBody] GeneralCurrencyMasterModel model)
        {
            try
            {
                GeneralCurrencyMasterModel currencyMaster = _generalCurrencyMasterService.CreateCurrency(model);
                return IsNotNull(currencyMaster) ? CreateCreatedResponse(new GeneralCurrencyMasterResponse { GeneralCurrencyMasterModel = currencyMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralCurrencyMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCurrencyMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralCurrencyMaster/GetCurrency")]
        [HttpGet]
        [Produces(typeof(GeneralCurrencyMasterResponse))]
        public virtual IActionResult GetCurrency(short generalCurrencyMasterId)
        {
            try
            {
                GeneralCurrencyMasterModel generalCurrencyMasterModel = _generalCurrencyMasterService.GetCurrency(generalCurrencyMasterId);
                return IsNotNull(generalCurrencyMasterModel) ? CreateOKResponse(new GeneralCurrencyMasterResponse { GeneralCurrencyMasterModel = generalCurrencyMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralCurrencyMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCurrencyMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralCurrencyMaster/UpdateCurrency")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralCurrencyMasterResponse))]
        public virtual IActionResult UpdateCurrency([FromBody] GeneralCurrencyMasterModel model)
        {
            try
            {
                bool isUpdated = _generalCurrencyMasterService.UpdateCurrency(model);
                return isUpdated ? CreateOKResponse(new GeneralCurrencyMasterResponse { GeneralCurrencyMasterModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralCurrencyMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCurrencyMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralCurrencyMaster/DeleteCurrency")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteCurrency([FromBody] ParameterModel generalCurrencyMasterIds)
        {
            try
            {
                bool deleted = _generalCurrencyMasterService.DeleteCurrency(generalCurrencyMasterIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralCurrencyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
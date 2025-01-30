using Coditech.API.Data;
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
    public class AccSetupGLBankController : BaseController
    {
        private readonly IAccSetupGLBankService _accSetupGLBankService;
        protected readonly ICoditechLogging _coditechLogging;

        public AccSetupGLBankController(ICoditechLogging coditechLogging, IAccSetupGLBankService accSetupGLBankService)
        {
            _accSetupGLBankService = accSetupGLBankService;
            _coditechLogging = coditechLogging;
        }
        [HttpGet]
        [Route("/AccSetupGLBank/GetAccSetupGLBankList")]
        [Produces(typeof(AccSetupGLBankListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetAccSetupGLBankList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                AccSetupGLBankListModel list = _accSetupGLBankService.GetAccSetupGLBankList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<AccSetupGLBankResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupGLBankListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupGLBankListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/AccSetupGLBank/CreateAccSetupGLBank")]
        [HttpPost, ValidateModel]
        [Produces(typeof(AccSetupGLBankResponse))]
        public virtual IActionResult CreateAccSetupGLBank([FromBody] AccSetupGLBankModel model)
        {
            try
            {
                AccSetupGLBankModel AccSetupGLBank = _accSetupGLBankService.CreateAccSetupGLBank(model);
                return IsNotNull(AccSetupGLBank) ? CreateCreatedResponse(new AccSetupGLBankResponse { AccSetupGLBankModel = AccSetupGLBank }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupGLBankResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupGLBankResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccSetupGLBank/GetAccSetupGLBank")]
        [HttpGet]
        [Produces(typeof(AccSetupGLBankResponse))]
        public virtual IActionResult GetAccSetupGLBank(int accSetupGLBankId)
        {
            try
            {
                AccSetupGLBankModel accSetupGLBankModel = _accSetupGLBankService.GetAccSetupGLBank(accSetupGLBankId);
                return IsNotNull(accSetupGLBankModel) ? CreateOKResponse(new AccSetupGLBankResponse { AccSetupGLBankModel = accSetupGLBankModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupGLBankResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupGLBankResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccSetupGLBank/UpdateAccSetupGLBank")]
        [HttpPut, ValidateModel]
        [Produces(typeof(AccSetupGLBankResponse))]
        public virtual IActionResult UpdateAccSetupGLBank([FromBody] AccSetupGLBankModel model)
        {
            try
            {
                bool isUpdated = _accSetupGLBankService.UpdateAccSetupGLBank(model);
                return isUpdated ? CreateOKResponse(new AccSetupGLBankResponse { AccSetupGLBankModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupGLBankResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupGLBankResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/AccSetupGLBank/DeleteAccSetupGLBank")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteAccSetupGLBank([FromBody] ParameterModel accSetupGLBankIds)
        {
            try
            {
                bool deleted = _accSetupGLBankService.DeleteAccSetupGLBank(accSetupGLBankIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLBank.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [HttpGet]
        [Route("/AccSetupGLBank/GetAccSetupBalanceSheet")]
        [Produces(typeof(AccSetupBalanceSheetListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetAccSetupBalanceSheet(string selectedCentreCode, byte accSetupBalanceSheetTypeId)
        {
            try
            {
                AccSetupBalanceSheetListModel list = _accSetupGLBankService.GetAccSetupBalanceSheet(selectedCentreCode, accSetupBalanceSheetTypeId);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<AccSetupBalanceSheetListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupBalanceSheetListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupBalanceSheetListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}

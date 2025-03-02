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
    public class AccGLTransactionController : BaseController
    {
        private readonly IAccGLTransactionService _accGLTransactionService;
        protected readonly ICoditechLogging _coditechLogging;
        public AccGLTransactionController(ICoditechLogging coditechLogging, IAccGLTransactionService accGLTransactionService)
        {
            _accGLTransactionService = accGLTransactionService;
            _coditechLogging = coditechLogging;
        }

      
        [HttpGet]
        [Route("/AccGLTransaction/GetGLTransaction")]
        [Produces(typeof(AccGLTransactionListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetGLTransactionList( string selectedCentreCode ,int accGLTransactionId, short generalFinancialYearId, short accSetupTransactionTypeId,byte accSetupBalanceSheetTypeId, ExpandCollection expand, FilterCollection filter, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                AccGLTransactionListModel list = _accGLTransactionService.AccGLTransactionList(selectedCentreCode,accGLTransactionId, generalFinancialYearId, accSetupTransactionTypeId, accSetupBalanceSheetTypeId, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<AccGLTransactionListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccGLTransactionListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccGLTransactionListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
       
        [Route("/AccGLTransaction/CreateGLTransaction")]
        [HttpPost, ValidateModel]
        [Produces(typeof(AccGLTransactionResponse))]
        public virtual IActionResult CreateGLTransaction([FromBody] AccGLTransactionModel model)
        {
            try
            {
                
                AccGLTransactionModel designation = _accGLTransactionService.CreateGLTransaction(model);
                return IsNotNull(designation) ? CreateCreatedResponse(new AccGLTransactionResponse { AccGLTransactionModel = designation }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccGLTransactionResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccGLTransactionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccGLTransaction/GetGLTransaction")]
        [HttpGet]
        [Produces(typeof(AccGLTransactionResponse))]
        public virtual IActionResult GetGLTransaction(long accGLTransactionId)
        {
            try
            {
                AccGLTransactionModel accGLTransactionModel = _accGLTransactionService.GetGLTransaction(accGLTransactionId);
                return IsNotNull(accGLTransactionModel) ? CreateOKResponse(new AccGLTransactionResponse { AccGLTransactionModel = accGLTransactionModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccGLTransactionResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccGLTransactionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccGLTransaction/UpdateGLTransaction")]
        [HttpPut, ValidateModel]
        [Produces(typeof(AccGLTransactionResponse))]
        public virtual IActionResult UpdateGLTransaction([FromBody] AccGLTransactionModel model)
        {
            try
            {
                bool isUpdated = _accGLTransactionService.UpdateGLTransaction(model);
                return isUpdated ? CreateOKResponse(new AccGLTransactionResponse { AccGLTransactionModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccGLTransactionResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccGLTransactionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        //[Route("/AccGLTransaction/DeleteBalanceSheet")]
        //[HttpPost, ValidateModel]
        //[Produces(typeof(TrueFalseResponse))]
        //public virtual IActionResult DeleteBalanceSheet([FromBody] ParameterModel balanceSheetIds)
        //{
        //    try
        //    {
        //        bool deleted = _accGLTransactionService.DeleteBalanceSheet(balanceSheetIds);
        //        return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
        //    }
        //    catch (CoditechException ex)
        //    {
        //        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Warning);
        //        return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
        //    }
        //    catch (Exception ex)
        //    {
        //        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Error);
        //        return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
        //    }
        //}
    }
}

using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [Route("/AccGLTransaction/CreateGLTransaction")]
        [HttpPost, ValidateModel]
        [Produces(typeof(AccGLTransactionResponse))]
        public virtual IActionResult CreateGLTransaction([FromBody] AccGLTransactionModel model)
        {
            try
            {

                bool accGLTransactionModel = _accGLTransactionService.CreateGLTransaction(model);
                return CreateOKResponse(new AccGLTransactionResponse { AccGLTransactionModel = model }); // CreateInternalServerErrorResponse();
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

        [Route("/AccGLTransaction/GetAccSetupGLAccountList")]
        [HttpGet]
        [Produces(typeof(AccGLTransactionResponse))]
        public virtual IActionResult GetAccSetupGLAccountList(string searchKeyword, int accSetupGLId, string userType, string transactionTypeCode, int balanceSheet)
        {
            try
            {
                List<AccGLTransactionModel> accGLTransactionList = _accGLTransactionService.GetAccSetupGLAccountList(searchKeyword, accSetupGLId, userType, transactionTypeCode, balanceSheet);

                return accGLTransactionList != null && accGLTransactionList.Any()
                    ? CreateOKResponse(new AccGLTransactionListResponse { AccGLTransactionList = accGLTransactionList })
                    : CreateNoContentResponse();
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccGLTransactionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/AccGLTransaction/GetPersons")]
        [HttpGet]
        [Produces(typeof(AccGLTransactionResponse))]
        public virtual IActionResult GetPersons(string searchKeyword, int userTypeId, int balaceSheet)
        {
            try
            {
                List<AccGLTransactionModel> accGLTransactionList = _accGLTransactionService.GetPersons(searchKeyword, userTypeId, balaceSheet);

                return accGLTransactionList != null && accGLTransactionList.Any()
                    ? CreateOKResponse(new AccGLTransactionListResponse { AccGLTransactionList = accGLTransactionList })
                    : CreateNoContentResponse();
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLTransaction.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccGLTransactionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }


    }
}

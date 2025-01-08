using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Logger;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Controllers
{
    public class PaymentGatewayDetailsController : BaseController
    {
        private readonly IPaymentGatewayDetailsService _paymentGatewayDetailsService;
        protected readonly ICoditechLogging _coditechLogging;
        public PaymentGatewayDetailsController(ICoditechLogging coditechLogging, IPaymentGatewayDetailsService paymentGatewayDetailsService)
        {
            _paymentGatewayDetailsService = paymentGatewayDetailsService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/PaymentGatewayDetails/GetPaymentGatewayDetailsList")]
        [Produces(typeof(PaymentGatewayDetailsListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetPaymentGatewayDetailsList(string selectedCentreCode, byte paymentGatewayId)
        {
            try
            {
                PaymentGatewayDetailsListModel list = _paymentGatewayDetailsService.GetPaymentGatewayDetailsList(selectedCentreCode, paymentGatewayId);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<PaymentGatewayDetailsListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new PaymentGatewayDetailsListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new PaymentGatewayDetailsListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/PaymentGatewayDetails/CreatePaymentGatewayDetails")]
        [HttpPost, ValidateModel]
        [Produces(typeof(PaymentGatewayDetailsResponse))]
        public virtual IActionResult CreatePaymentGatewayDetails([FromBody] PaymentGatewayDetailsModel model)
        {
            try
            {
                PaymentGatewayDetailsModel paymentGatewayDetails = _paymentGatewayDetailsService.CreatePaymentGatewayDetails(model);
                return IsNotNull(paymentGatewayDetails) ? CreateCreatedResponse(new PaymentGatewayDetailsResponse { PaymentGatewayDetailsModel = paymentGatewayDetails }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new PaymentGatewayDetailsResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new PaymentGatewayDetailsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/PaymentGatewayDetails/GetPaymentGatewayDetails")]
        [HttpGet]
        [Produces(typeof(PaymentGatewayDetailsResponse))]
        public virtual IActionResult GetPaymentGatewayDetails(int paymentGatewayDetailId)
        {
            try
            {
                PaymentGatewayDetailsModel paymentGatewayDetailsModel = _paymentGatewayDetailsService.GetPaymentGatewayDetails(paymentGatewayDetailId);
                return IsNotNull(paymentGatewayDetailsModel) ? CreateOKResponse(new PaymentGatewayDetailsResponse { PaymentGatewayDetailsModel = paymentGatewayDetailsModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new PaymentGatewayDetailsResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new PaymentGatewayDetailsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/PaymentGatewayDetails/UpdatePaymentGatewayDetails")]
        [HttpPut, ValidateModel]
        [Produces(typeof(PaymentGatewayDetailsResponse))]
        public virtual IActionResult UpdatePaymentGatewayDetails([FromBody] PaymentGatewayDetailsModel model)
        {
            try
            {
                bool isUpdated = _paymentGatewayDetailsService.UpdatePaymentGatewayDetails(model);
                return isUpdated ? CreateOKResponse(new PaymentGatewayDetailsResponse { PaymentGatewayDetailsModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new PaymentGatewayDetailsResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new PaymentGatewayDetailsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/PaymentGatewayDetails/DeletePaymentGatewayDetails")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeletePaymentGatewayDetails([FromBody] ParameterModel paymentGatewayDetailId)
        {
            try
            {
                bool deleted = _paymentGatewayDetailsService.DeletePaymentGatewayDetails(paymentGatewayDetailId);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

    }
}

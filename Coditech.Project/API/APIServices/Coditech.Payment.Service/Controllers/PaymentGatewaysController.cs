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
    public class PaymentGatewaysController : BaseController
    {
        private readonly IPaymentGatewaysService _paymentGatewaysService;
        protected readonly ICoditechLogging _coditechLogging;

        public PaymentGatewaysController(ICoditechLogging coditechLogging, IPaymentGatewaysService paymentGatewaysService)
        {
            _paymentGatewaysService = paymentGatewaysService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/PaymentGateways/GetPaymentGatewaysList")]
        [Produces(typeof(PaymentGatewaysListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetPaymentGatewaysList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                PaymentGatewaysListModel list = _paymentGatewaysService.GetPaymentGatewaysList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<PaymentGatewaysListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new PaymentGatewaysListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new PaymentGatewaysListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/PaymentGateways/CreatePaymentGateways")]
        [HttpPost, ValidateModel]
        [Produces(typeof(PaymentGatewaysResponse))]
        public virtual IActionResult CreatePaymentGateways([FromBody] PaymentGatewaysModel model)
        {
            try
            {
                PaymentGatewaysModel paymentGateways = _paymentGatewaysService.CreatePaymentGateways(model);
                return IsNotNull(paymentGateways) ? CreateCreatedResponse(new PaymentGatewaysResponse { PaymentGatewaysModel = paymentGateways }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new PaymentGatewaysResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new PaymentGatewaysResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/PaymentGateways/GetPaymentGateways")]
        [HttpGet]
        [Produces(typeof(PaymentGatewaysResponse))]
        public virtual IActionResult GetPaymentGateways(byte paymentGatewaysId)
        {
            try
            {
                PaymentGatewaysModel paymentGatewaysModel = _paymentGatewaysService.GetPaymentGateways(paymentGatewaysId);
                return IsNotNull(paymentGatewaysModel) ? CreateOKResponse(new PaymentGatewaysResponse { PaymentGatewaysModel = paymentGatewaysModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new PaymentGatewaysResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new PaymentGatewaysResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/PaymentGateways/UpdatePaymentGateways")]
        [HttpPut, ValidateModel]
        [Produces(typeof(PaymentGatewaysResponse))]
        public virtual IActionResult UpdatePaymentGateways([FromBody] PaymentGatewaysModel model)
        {
            try
            {
                bool isUpdated = _paymentGatewaysService.UpdatePaymentGateways(model);
                return isUpdated ? CreateOKResponse(new PaymentGatewaysResponse { PaymentGatewaysModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new PaymentGatewaysResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new PaymentGatewaysResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/PaymentGateways/DeletePaymentGateways")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeletePaymentGateways([FromBody] ParameterModel paymentGatewaysIdIds)
        {
            try
            {
                bool deleted = _paymentGatewaysService.DeletePaymentGateways(paymentGatewaysIdIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

    }
}

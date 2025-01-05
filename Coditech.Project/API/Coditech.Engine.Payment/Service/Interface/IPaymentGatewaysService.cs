using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;
namespace Coditech.API.Service
{
    public interface IPaymentGatewaysService
    {
        PaymentGatewaysListModel GetPaymentGatewaysList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        PaymentGatewaysModel CreatePaymentGateways(PaymentGatewaysModel model);
        PaymentGatewaysModel GetPaymentGateways(byte paymentGatewayId);
        bool UpdatePaymentGateways(PaymentGatewaysModel model);
        bool DeletePaymentGateways(ParameterModel parameterModel);
    }
}

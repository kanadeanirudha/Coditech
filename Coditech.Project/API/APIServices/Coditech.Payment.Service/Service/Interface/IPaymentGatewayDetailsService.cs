using Coditech.Common.API.Model;
namespace Coditech.API.Service
{
    public interface IPaymentGatewayDetailsService
    {
        PaymentGatewayDetailsListModel GetPaymentGatewayDetailsList(string selectedCentreCode, byte paymentGatewayId);
        PaymentGatewayDetailsModel CreatePaymentGatewayDetails(PaymentGatewayDetailsModel model);
        PaymentGatewayDetailsModel GetPaymentGatewayDetails(int PaymentGatewayDetailId);
        bool UpdatePaymentGatewayDetails(PaymentGatewayDetailsModel model);
        bool DeletePaymentGatewayDetails(ParameterModel parameterModel);
    }
}

using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
namespace Coditech.API.Endpoint
{
    public class PaymentGatewayDetailsEndpoint : BaseEndpoint
    {
        public string ListAsync(string selectedCentreCode, byte paymentGatewayId)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechPaymentApiRootUri}/PaymentGatewayDetails/GetPaymentGatewayDetailsList?selectedCentreCode={selectedCentreCode}&paymentGatewayId={paymentGatewayId}{BuildEndpointQueryString(true)}";
            return endpoint;
        }
        public string CreatePaymentGatewayDetailsAsync() =>
            $"{CoditechAdminSettings.CoditechPaymentApiRootUri}/PaymentGatewayDetails/CreatePaymentGatewayDetails";
        public string GetPaymentGatewayDetailsAsync(int paymentGatewayDetailId) =>
            $"{CoditechAdminSettings.CoditechPaymentApiRootUri}/PaymentGatewayDetails/GetPaymentGatewayDetails?paymentGatewayDetailId={paymentGatewayDetailId}";

        public string UpdatePaymentGatewayDetailsAsync() =>
               $"{CoditechAdminSettings.CoditechPaymentApiRootUri}/PaymentGatewayDetails/UpdatePaymentGatewayDetails";

        public string DeletePaymentGatewayDetailsAsync() =>
                  $"{CoditechAdminSettings.CoditechPaymentApiRootUri}/PaymentGatewayDetails/DeletePaymentGatewayDetails";

    }
}

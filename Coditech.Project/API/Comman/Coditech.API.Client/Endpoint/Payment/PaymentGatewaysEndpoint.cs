using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;
namespace Coditech.API.Endpoint
{
    public class PaymentGatewaysEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechPaymentApiRootUri}/PaymentGateways/GetPaymentGatewaysList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreatePaymentGatewaysAsync() =>
            $"{CoditechAdminSettings.CoditechPaymentApiRootUri}/PaymentGateways/CreatePaymentGateways";
        public string GetPaymentGatewaysAsync(short paymentGatewaysId) =>
            $"{CoditechAdminSettings.CoditechPaymentApiRootUri}/PaymentGateways/GetPaymentGateways?paymentGatewaysId={paymentGatewaysId}";

        public string UpdatePaymentGatewaysAsync() =>
               $"{CoditechAdminSettings.CoditechPaymentApiRootUri}/PaymentGateways/UpdatePaymentGateways";

        public string DeletePaymentGatewaysAsync() =>
                  $"{CoditechAdminSettings.CoditechPaymentApiRootUri}/PaymentGateways/DeletePaymentGateways";
    }
}

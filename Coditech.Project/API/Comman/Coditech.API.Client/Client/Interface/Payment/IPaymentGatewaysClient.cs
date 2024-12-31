using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
namespace Coditech.API.Client
{
    public interface IPaymentGatewaysClient : IBaseClient
    {
        /// <summary>
        /// Get list of Payment Gateways .
        /// </summary>
        /// <returns>PaymentGatewaysListResponse</returns>
        PaymentGatewaysListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create PaymentGateways.
        /// </summary>
        /// <param name="PaymentGatewaysModel">PaymentGatewaysModel.</param>
        /// <returns>Returns PaymentGatewaysResponse.</returns>
        PaymentGatewaysResponse CreatePaymentGateways(PaymentGatewaysModel body);

        /// <summary>
        /// Get PaymentGateways by paymentGatewaysId.
        /// </summary>
        /// <param name="paymentGatewaysId">paymentGatewaysId</param>
        /// <returns>Returns PaymentGatewaysResponse.</returns>
        PaymentGatewaysResponse GetPaymentGateways(byte paymentGatewaysId);

        /// <summary>
        /// Update PaymentGateways.
        /// </summary>
        /// <param name="PaymentGatewaysModel">PaymentGatewaysModel.</param>
        /// <returns>Returns updated PaymentGatewaysResponse</returns>
        PaymentGatewaysResponse UpdatePaymentGateways(PaymentGatewaysModel body);

        /// <summary>
        /// Delete PaymentGateways.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeletePaymentGateways(ParameterModel body);



    }
}

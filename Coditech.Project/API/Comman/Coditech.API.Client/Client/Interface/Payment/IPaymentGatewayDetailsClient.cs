using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
namespace Coditech.API.Client

{
    public interface IPaymentGatewayDetailsClient : IBaseClient
    {
        /// <summary>
        /// Get list of PaymentGatewayDetails.
        /// </summary>
        /// <returns>PaymentGatewayDetailsListResponse</returns>
        PaymentGatewayDetailsListResponse List(string selectedCentreCode, byte paymentGatewayId);
        /// <summary>
        /// Create PaymentGatewayDetails.
        /// </summary>
        /// <param name="PaymentGatewayDetailsModel">PaymentGatewayDetailsModel.</param>
        /// <returns>Returns PaymentGatewayDetailsResponse.</returns>
        PaymentGatewayDetailsResponse CreatePaymentGatewayDetails(PaymentGatewayDetailsModel body);

        /// <summary>
        /// Get PaymentGatewayDetails by paymentGatewayDetailsId.
        /// </summary>
        /// <param name="paymentGatewayDetailsId">paymentGatewayDetailsId</param>
        /// <returns>Returns PaymentGatewayDetailsIdResponse.</returns>
        PaymentGatewayDetailsResponse GetPaymentGatewayDetails(int paymentGatewayDetailId);

        /// <summary>
        /// Update PaymentGatewayDetails.
        /// </summary>
        /// <param name="PaymentGatewayDetailsModel">PaymentGatewayDetailsModel.</param>
        /// <returns>Returns updated PaymentGatewayDetailsResponse</returns>
        PaymentGatewayDetailsResponse UpdatePaymentGatewayDetails(PaymentGatewayDetailsModel model);

        /// <summary>
        /// Delete PaymentGatewayDetails.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeletePaymentGatewayDetails(ParameterModel body);

    }
}

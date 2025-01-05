using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;
namespace Coditech.Admin.Agents
{
    public interface IPaymentGatewaysAgent
    {
        /// <summary>
        /// Get list of PaymentGateways
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>PaymentGatewaysListViewModel</returns>
        PaymentGatewaysListViewModel GetPaymentGatewaysList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create PaymentGateways.
        /// </summary>
        /// <param name="paymentGatewaysViewModel">PaymentGateways View Model.</param>
        /// <returns>Returns created model.</returns>
        PaymentGatewaysViewModel CreatePaymentGateways(PaymentGatewaysViewModel paymentGatewaysViewModel);


        /// <summary>
        /// Get PaymentGateways by paymentGatewaysId.
        /// </summary>
        /// <param name="paymentGatewaysId">paymentGatewaysId</param>
        /// <returns>Returns GeneralCountryViewModel.</returns>
        PaymentGatewaysViewModel GetPaymentGateways(byte paymentGatewaysId);

        /// <summary>
        /// Update PaymentGateways.
        /// </summary>
        /// <param name="paymentGatewaysViewModel">paymentGatewaysViewModel.</param>
        /// <returns>Returns updated paymentGatewaysViewModel</returns>
        PaymentGatewaysViewModel UpdatePaymentGateways(PaymentGatewaysViewModel paymentGatewaysViewModel);

        /// <summary>
        /// Delete PaymentGateways.
        /// </summary>
        /// <param name="paymentGatewaysId">paymentGatewaysId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeletePaymentGateways(string paymentGatewaysId, out string errorMessage);
        PaymentGatewaysListResponse GetPaymentGatewaysList();
    }
}

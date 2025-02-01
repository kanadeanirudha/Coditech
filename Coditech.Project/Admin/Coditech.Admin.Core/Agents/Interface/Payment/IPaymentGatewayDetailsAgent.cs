using Coditech.Admin.ViewModel;
namespace Coditech.Admin.Agents
{
    public interface IPaymentGatewayDetailsAgent   
    {
        /// <summary>
        /// Get list of PaymentGatewayDetails
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>PaymentGatewayDetailsListViewModel</returns>
        PaymentGatewayDetailsListViewModel GetPaymentGatewayDetailsList(DataTableViewModel dataTable, byte paymentGatewayId);

    /// <summary>
    /// Create PaymentGatewayDetails.
    /// </summary>
    /// <param name="PrivacySettingViewModel"> PaymentGatewayDetails  View Model.</param>
    /// <returns>Returns created model.</returns>
    PaymentGatewayDetailsViewModel CreatePaymentGatewayDetails(PaymentGatewayDetailsViewModel paymentGatewayDetailsViewModel);

        /// <summary>
        /// Get PaymentGatewayDetails by PaymentGatewayDetailsId.
        /// </summary>
        /// <param name="paymentGatewayDetailsId">paymentGatewayDetailsId</param>
        /// <returns>Returns PaymentGatewayDetailsViewModel.</returns>
        PaymentGatewayDetailsViewModel GetPaymentGatewayDetails(int paymentGatewayDetailId);

        /// <summary>
        /// Update paymentGatewayDetailsId.
        /// </summary>
        /// <param name="paymentGatewayDetailsViewModel">paymentGatewayDetailsViewModel.</param>
        /// <returns>Returns updated PaymentGatewayDetailsViewModel</returns>
        PaymentGatewayDetailsViewModel UpdatePaymentGatewayDetails(PaymentGatewayDetailsViewModel paymentGatewayDetailsViewModel);

        /// <summary>
        /// Delete paymentGatewayDetails.
        /// </summary>
        /// <param name="paymentGatewayDetailsId">paymentGatewayDetailsId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeletePaymentGatewayDetails(string paymentGatewayDetailsIdId, out string errorMessage);
    }
}

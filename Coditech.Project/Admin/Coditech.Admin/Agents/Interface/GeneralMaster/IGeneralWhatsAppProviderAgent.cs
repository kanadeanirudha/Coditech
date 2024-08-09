using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents

{
    public interface IGeneralWhatsAppProviderAgent
    {

        /// <summary>
        /// Get list of General WhatsAppProvider.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralWhatsAppProviderListViewModel</returns>
        GeneralWhatsAppProviderListViewModel GetWhatsAppProviderList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create WhatsAppProvider.
        /// </summary>
        /// <param name="generalWhatsAppProviderViewModel">General WhatsAppProvider View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralWhatsAppProviderViewModel CreateWhatsAppProvider(GeneralWhatsAppProviderViewModel generalWhatsAppProviderViewModel);
        /// <summary>
        /// Get SmsProvider by generalSmsProviderId.
        /// </summary>
        /// <param name="generalSmsProviderId">generalSmsProviderId</param>
        /// <returns>Returns GeneralSmsProviderViewModel.</returns>
   
        GeneralWhatsAppProviderViewModel GetWhatsAppProvider(short generalWhatsAppProviderId);

        /// <summary>
        /// Update WhatsAppProvider.
        /// </summary>
        /// <param name="generalWhatsAppProviderViewModel">generalWhatsAppProviderViewModel.</param>
        /// <returns>Returns updated GeneralWhatsAppProviderViewModel</returns>
        GeneralWhatsAppProviderViewModel UpdateWhatsAppProvider(GeneralWhatsAppProviderViewModel generalWhatsAppProviderViewModel);

        /// <summary>
        /// Delete WhatsAppProvider.
        /// </summary>
        /// <param name="generalWhatsAppProviderId">generalWhatsAppProviderId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteWhatsAppProvider(string generalWhatsAppProviderId, out string errorMessage);
        GeneralWhatsAppProviderListResponse GetWhatsAppProviderList();

    }
}

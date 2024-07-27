using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface  IGeneralSmsProviderAgent
    {
        /// <summary>
        /// Get list of General SmsProvider.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralSmsProviderListViewModel</returns>
        GeneralSmsProviderListViewModel GetSmsProviderList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create SmsProvider.
        /// </summary>
        /// <param name="generalSmsProviderViewModel">General SmsProvider View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralSmsProviderViewModel CreateSmsProvider(GeneralSmsProviderViewModel generalSmsProviderViewModel);

        /// <summary>
        /// Get SmsProvider by generalSmsProviderId.
        /// </summary>
        /// <param name="generalSmsProviderId">generalSmsProviderId</param>
        /// <returns>Returns GeneralSmsProviderViewModel.</returns>
        GeneralSmsProviderViewModel GetSmsProvider(short generalSmsProviderId);

        /// <summary>
        /// Update SmsProvider.
        /// </summary>
        /// <param name="generalSmsProviderViewModel">generalSmsProviderViewModel.</param>
        /// <returns>Returns updated GeneralSmsProviderViewModel</returns>
        GeneralSmsProviderViewModel UpdateSmsProvider(GeneralSmsProviderViewModel generalSmsProviderViewModel);

        /// <summary>
        /// Delete SmsProvider.
        /// </summary>
        /// <param name="generalSmsProviderId">generalSmsProviderId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteSmsProvider(string generalSmsProviderId, out string errorMessage);
        GeneralSmsProviderListResponse GetSmsProviderList();
    }
}

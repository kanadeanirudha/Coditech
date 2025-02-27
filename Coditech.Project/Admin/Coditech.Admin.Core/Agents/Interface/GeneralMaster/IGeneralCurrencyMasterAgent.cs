using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralCurrencyMasterAgent
    {
        /// <summary>
        /// Get list of General Currency Master.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralCurrencyMasterListViewModel</returns>
        GeneralCurrencyMasterListViewModel GetCurrencyList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create City.
        /// </summary>
        /// <param name="generalCurrencyMasterViewModel">General Currency View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralCurrencyMasterViewModel CreateCurrency(GeneralCurrencyMasterViewModel generalCurrencyMasterViewModel);

        /// <summary>
        /// Get Currency by currencyId.
        /// </summary>
        /// <param name="currencyId"> currencyId</param>
        /// <returns>Returns GeneralCurrencyMasterViewModel.</returns>
        GeneralCurrencyMasterViewModel GetCurrency(short generalCurrencyMasterId);

        /// <summary>
        /// Update Currency.
        /// </summary>
        /// <param name="generalCurrencyMasterViewModel">generalCurrencyMasterViewModel.</param>
        /// <returns>Returns updated GeneralCurrencyMasterViewModel</returns>
        GeneralCurrencyMasterViewModel UpdateCurrency(GeneralCurrencyMasterViewModel generalCurrencyMasterViewModel);

        /// <summary>
        /// Delete Currency.
        /// </summary>
        /// <param name="currencyId">currencyId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteCurrency(string currencyId, out string errorMessage);
    }
}

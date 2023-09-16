using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGeneralCountryAgent
    {
        /// <summary>
        /// Get list of General Country.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralCountryListViewModel</returns>
        GeneralCountryListViewModel GetCountryList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Country.
        /// </summary>
        /// <param name="generalCountryViewModel">General Country View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralCountryViewModel CreateCountry(GeneralCountryViewModel generalCountryViewModel);

        /// <summary>
        /// Get Country by generalCountryId.
        /// </summary>
        /// <param name="generalCountryId">generalCountryId</param>
        /// <returns>Returns GeneralCountryViewModel.</returns>
        GeneralCountryViewModel GetCountry(short generalCountryId);

        /// <summary>
        /// Update Country.
        /// </summary>
        /// <param name="generalCountryViewModel">generalCountryViewModel.</param>
        /// <returns>Returns updated GeneralCountryViewModel</returns>
        GeneralCountryViewModel UpdateCountry(GeneralCountryViewModel generalCountryViewModel);

        /// <summary>
        /// Delete Country.
        /// </summary>
        /// <param name="generalCountryId">generalCountryId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteCountry(string generalCountryId, out string errorMessage);
    }
}

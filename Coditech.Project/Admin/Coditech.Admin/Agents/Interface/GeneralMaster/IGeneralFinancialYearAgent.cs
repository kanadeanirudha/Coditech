using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralFinancialYearAgent
    {
        /// <summary>
        /// Get list of General FinancialYear.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralFinancialYearListViewModel</returns>
        GeneralFinancialYearListViewModel GetFinancialYearList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create FinancialYear.
        /// </summary>
        /// <param name="generalFinancialYearViewModel">General FinancialYear View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralFinancialYearViewModel CreateFinancialYear(GeneralFinancialYearViewModel generalFinancialYearViewModel);

        /// <summary>
        /// Get FinancialYear by generalFinancialYearId.
        /// </summary>
        /// <param name="generalFinancialYearId">generalFinancialYearId</param>
        /// <returns>Returns GeneralFinancialYearViewModel.</returns>
        GeneralFinancialYearViewModel GetFinancialYear(short generalFinancialYearId);

        /// <summary>
        /// Update FinancialYear.
        /// </summary>
        /// <param name="generalFinancialYearViewModel">generalFinancialYearViewModel.</param>
        /// <returns>Returns updated GeneralFinancialYearViewModel</returns>
        GeneralFinancialYearViewModel UpdateFinancialYear(GeneralFinancialYearViewModel generalFinancialYearViewModel);

        /// <summary>
        /// Delete FinancialYear.
        /// </summary>
        /// <param name="generalFinancialYearId">generalFinancialYearId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteFinancialYear(string generalFinancialYearId, out string errorMessage);
        GeneralFinancialYearListResponse GetFinancialYearList();
    }
}

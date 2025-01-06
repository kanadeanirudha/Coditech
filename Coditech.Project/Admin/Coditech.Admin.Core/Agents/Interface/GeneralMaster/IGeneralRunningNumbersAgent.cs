using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGeneralRunningNumbersAgent
    {
        /// <summary>
        /// Get list of General Running Numbers.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralRunningNumbersListViewModel</returns>
        GeneralRunningNumbersListViewModel GetRunningNumbersList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create GeneralRunningNumbers.
        /// </summary>
        /// <param name="generalRunningNumbersViewModel">General Running Numbers View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralRunningNumbersViewModel CreateRunningNumbers(GeneralRunningNumbersViewModel generalRunningNumbersViewModel);

        /// <summary>
        /// Get GeneralRunningNumbers by generalRunningNumberId.
        /// </summary>
        /// <param name="generalRunningNumberId">generalRunningNumberId</param>
        /// <returns>Returns GeneralRunningNumbersViewModel.</returns>
        GeneralRunningNumbersViewModel GetRunningNumbers(long generalRunningNumberId);

        /// <summary>
        /// Update GeneralRunningNumbers.
        /// </summary>
        /// <param name="generalRunningNumbersViewModel">generalRunningNumbersViewModel.</param>
        /// <returns>Returns updated GeneralRunningNumbersViewModel</returns>
        GeneralRunningNumbersViewModel UpdateRunningNumbers(GeneralRunningNumbersViewModel generalRunningNumbersViewModel);

        /// <summary>
        /// Delete GeneralRunningNumbers.
        /// </summary>
        /// <param name="generalRunningNumberId">generalRunningNumberId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteRunningNumbers(string generalRunningNumberId, out string errorMessage);
    }
}

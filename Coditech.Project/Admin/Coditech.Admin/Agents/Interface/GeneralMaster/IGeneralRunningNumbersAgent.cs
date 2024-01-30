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
    }
}

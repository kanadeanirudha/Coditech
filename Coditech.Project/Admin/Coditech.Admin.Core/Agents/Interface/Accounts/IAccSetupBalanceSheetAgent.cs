using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IAccSetupBalanceSheetAgent
    {
        /// <summary>
        /// Get list of Balance Sheet.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>AccSetupBalanceSheetListViewModel</returns>
        AccSetupBalanceSheetListViewModel GetBalanceSheetList(DataTableViewModel dataTableModel, byte accSetupBalanceSheetTypeId);

        /// <summary>
        /// Create Designation.
        /// </summary>
        /// <param name="accSetupBalanceSheetViewModel">Balance Sheet View Model.</param>
        /// <returns>Returns created model.</returns>
        AccSetupBalanceSheetViewModel CreateBalanceSheet(AccSetupBalanceSheetViewModel accSetupBalanceSheetViewModel);

        /// <summary>
        /// Get Designation by balanceSheetId.
        /// </summary>
        /// <param name="balanceSheetId">balanceSheetId</param>
        /// <returns>Returns AccSetupBalanceSheetViewModel.</returns>
        AccSetupBalanceSheetViewModel GetBalanceSheet(int balanceSheetId);

        /// <summary>
        /// Update Designation.
        /// </summary>
        /// <param name="accSetupBalanceSheetViewModel">accSetupBalanceSheetViewModel.</param>
        /// <returns>Returns updated AccSetupBalanceSheetViewModel</returns>
        AccSetupBalanceSheetViewModel UpdateBalanceSheet(AccSetupBalanceSheetViewModel accSetupBalanceSheetViewModel);

        /// <summary>
        /// Delete Designation.
        /// </summary>
        /// <param name="balanceSheetId">balanceSheetId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteBalanceSheet(string balanceSheetId, out string errorMessage);
    }
}

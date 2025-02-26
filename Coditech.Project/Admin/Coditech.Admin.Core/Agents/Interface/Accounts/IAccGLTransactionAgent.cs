using Coditech.Admin.ViewModel;
using Coditech.API.Data;

namespace Coditech.Admin.Agents
{
    public interface IAccGLTransactionAgent
    {
        /// <summary>
        /// Get list of Balance Sheet.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>AccGLTransactionListViewModel</returns>
        AccGLTransactionListViewModel GetGLTransactionList(DataTableViewModel dataTableModel,string selectedCentreCode, int accSetupBalanceSheetId, short generalFinancialYearId, short accSetupTransactionTypeId, byte accSetupBalanceSheetTypeId);

        /// <summary>
        /// Create Designation.
        /// </summary>
        /// <param name="accGLTransactionViewModel">Balance Sheet View Model.</param>
        /// <returns>Returns created model.</returns>short
        AccGLTransactionViewModel CreateGLTransaction(AccGLTransactionViewModel accGLTransactionViewModel);

        /// <summary>
        /// Get Designation by accGLTransactionId.
        /// </summary>
        /// <param name="accGLTransactionId">accGLTransactionId</param>
        /// <returns>Returns AccGLTransactionViewModel.</returns>
        AccGLTransactionViewModel GetGLTransaction(long accGLTransactionId);

        /// <summary>
        /// Update Designation.
        /// </summary>
        /// <param name="accGLTransactionViewModel">accGLTransactionViewModel.</param>
        /// <returns>Returns updated AccGLTransactionsViewModel</returns>
        AccGLTransactionViewModel UpdateGLTransaction(AccGLTransactionViewModel accGLTransactionViewModel);

        /// <summary>
        ///// Delete Designation.
        ///// </summary>
        ///// <param name="accGLTransactionId">accGLTransactionId.</param>
        ///// <returns>Returns true if deleted successfully else return false.</returns>
        //bool DeleteBalanceSheet(string accGLTransactionId, out string errorMessage);
    }
}

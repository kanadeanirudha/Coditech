using Coditech.Admin.ViewModel;
namespace Coditech.Admin.Agents
{
    public interface IAccSetupTransactionTypeAgent
    {
        /// <summary>
        /// Get list of AccSetupTransactionType.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>AccSetupTransactionTypeListViewModel</returns>
        AccSetupTransactionTypeListViewModel GetTransactionTypeList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create AccSetupTransactionType.
        /// </summary>
        /// <param name="accSetupTransactionTypeViewModel"> AccSetupTransactionType View Model.</param>
        /// <returns>Returns created model.</returns>
        AccSetupTransactionTypeViewModel CreateTransactionType(AccSetupTransactionTypeViewModel accSetupTransactionTypeViewModel);

        /// <summary>
        /// Get AccSetupTransactionType by accSetupTransactionType.
        /// </summary>
        /// <param name="accSetupTransactionType">accSetupTransactionType</param>
        /// <returns>Returns AccSetupTransactionTypeViewModel.</returns>
        AccSetupTransactionTypeViewModel GetTransactionType(short accSetupTransactionTypeId);

        /// <summary>
        /// Update AccSetupTransactionType.
        /// </summary>
        /// <param name="accSetupTransactionTypeViewModel">AccSetupTransactionTypeViewModel.</param>
        /// <returns>Returns updated AccSetupTransactionTypeViewModel</returns>
        AccSetupTransactionTypeViewModel UpdateTransactionType(AccSetupTransactionTypeViewModel accSetupTransactionTypeViewModel);

        /// <summary>
        /// Delete AccSetupTransactionType.
        /// </summary>
        /// <param name="accSetupTransactionTypeId">AccSetupTransactionTypeId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteTransactionType(string accSetupTransactionTypeId, out string errorMessage);

    }
}

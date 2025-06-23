using Coditech.Admin.ViewModel;
using Coditech.API.Data;
using Coditech.Common.API.Model;

namespace Coditech.Admin.Agents
{
    public interface IAccGLTransactionAgent
    {

        /// <summary>
        /// Create Designation.
        /// </summary>
        /// <param name="accGLTransactionViewModel">Balance Sheet View Model.</param>
        /// <returns>Returns created model.</returns>short
        AccGLTransactionViewModel CreateGLTransaction(AccGLTransactionViewModel accGLTransactionViewModel);


        /// <summary>
        /// Update Designation.
        /// </summary>
        /// <param name="accGLTransactionViewModel">accGLTransactionViewModel.</param>
        /// <returns>Returns updated AccGLTransactionsViewModel</returns>
        List<AccGLTransactionViewModel> GetAccSetupGLAccountList(string searchKeyword, int accSetupGLId, string userType, string transactionTypeCode, int balanceSheet);
        GeneralFinancialYearModel GetCurrentFinancialYear();

        /// <summary>
        ///GetPersonsByUserType
        /// </summary>
        /// <param name="accGLTransactionViewModel">accGLTransactionViewModel.</param>
        /// <returns>Returns updated AccGLTransactionsViewModel</returns>
        List<AccGLTransactionViewModel> GetPersons(string searchKeyword, int userTypeId, int balanceSheet);

    }
}

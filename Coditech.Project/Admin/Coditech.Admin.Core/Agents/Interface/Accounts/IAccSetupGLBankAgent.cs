using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;
namespace Coditech.Admin.Agents
{
    public interface IAccSetupGLBankAgent
    {

        /// <summary>
        /// Get list of AccSetupGLBank.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>AccSetupGLBankListViewModel</returns>
        AccSetupGLBankListViewModel GetAccSetupGLBankList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create AccSetupGLBank.
        /// </summary>
        /// <param name="accSetupGLBankViewModel">AccSetupGLBank View Model.</param>
        /// <returns>Returns created model.</returns>
        AccSetupGLBankViewModel CreateAccSetupGLBank(AccSetupGLBankViewModel accSetupGLBankViewModel);

        /// <summary>
        /// Get AccSetupGLBank by accSetupGLBankId.
        /// </summary>
        /// <param name="accSetupGLBankId">accSetupGLBankId</param>
        /// <returns>Returns AccSetupGLBankViewModel.</returns>
        AccSetupGLBankViewModel GetAccSetupGLBank(int accSetupGLBankId);

        /// <summary>
        /// Update AccSetupGLBank.
        /// </summary>
        /// <param name="accSetupGLBankViewModel">accSetupGLBankViewModel.</param>
        /// <returns>Returns updated AccSetupGLBankViewModel</returns>
        AccSetupGLBankViewModel UpdateAccSetupGLBank(AccSetupGLBankViewModel accSetupGLBankViewModel);

        /// <summary>
        /// Delete AccSetupGLBank.
        /// </summary>
        /// <param name="accSetupGLBankId">accSetupGLBankId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteAccSetupGLBank(string accSetupGLBankId, out string errorMessage);


    }
}

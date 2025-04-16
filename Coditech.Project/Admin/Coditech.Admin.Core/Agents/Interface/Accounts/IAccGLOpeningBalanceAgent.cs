using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;

namespace Coditech.Admin.Agents
{
    public interface IAccGLOpeningBalanceAgent
    {

        /// <summary>
        /// Get Designation by accGLTransactionId.
        /// </summary>
        /// <param name="accGLTransactionId">accGLTransactionId</param>
        /// <returns>Returns AccGLTransactionViewModel.</returns>
        ACCGLOpeningBalanceListViewModel GetNonControlHeadType(short accSetupCategoryId);
        GeneralFinancialYearModel GetCurrentFinancialYear();

        /// <summary>
        /// Update Designation.
        /// </summary>
        /// <param name="accGLOpeningBalanceViewListModel">accGLOpeningBalanceListViewModel.</param>
        /// <returns>Returns updated ACCGLOpeningBalanceListViewModel</returns>
        ACCGLOpeningBalanceListViewModel UpdateNonControlHeadType(ACCGLOpeningBalanceListViewModel accGLOpeningBalanceListViewModel);
    }
}

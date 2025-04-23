using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;

namespace Coditech.Admin.Agents
{
    public interface IAccGLOpeningBalanceAgent
    {

        /// <summary>
        /// Get ACCGLOpeningBalance
        /// </summary>
        /// <param name="accSetupCategoryId">accSetupCategoryId</param>
        /// <returns>Returns ACCGLOpeningBalanceListViewModel.</returns>
        ACCGLOpeningBalanceListViewModel GetNonControlHeadType(short accSetupCategoryId);
        GeneralFinancialYearModel GetCurrentFinancialYear();

        /// <summary>
        /// Update ACCGLOpeningBalance.
        /// </summary>
        /// <param name="accGLOpeningBalanceViewListModel">accGLOpeningBalanceListViewModel.</param>
        /// <returns>Returns updated ACCGLOpeningBalanceListViewModel</returns>
        ACCGLOpeningBalanceListViewModel UpdateNonControlHeadType(ACCGLOpeningBalanceListViewModel accGLOpeningBalanceListViewModel);
        ACCGLOpeningBalanceViewModel GetControlHeadType(short accSetupCategoryId);
        AccGLIndividualOpeningBalanceViewModel GetIndividualOpeningBalance(short userTypeId,short generalFinancialYearId,int accSetupGLId);
        AccGLIndividualOpeningBalanceViewModel UpdateIndividualOpeningBalance(AccGLIndividualOpeningBalanceViewModel accGLIndividualOpeningBalanceViewModel);
    }
}

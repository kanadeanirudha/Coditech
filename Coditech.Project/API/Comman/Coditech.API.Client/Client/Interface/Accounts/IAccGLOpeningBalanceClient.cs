using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;

namespace Coditech.API.Client
{
    public interface IAccGLOpeningBalanceClient : IBaseClient
    {
        /// <summary>
        /// GetNonControlHeadType
        /// </summary>
        /// <param name="accGLTransactionId">designationId</param>
        /// <returns>Returns AccGLTransactionResponse.</returns>
        ACCGLOpeningBalanceListResponse GetNonControlHeadType(int accSetupBalanceSheetId, short accSetupCategoryId, byte controlNonControl);

        /// <summary>
        /// Update ACCGLOpeningBalance.
        /// </summary>
        /// <param name="ACCGLOpeningBalanceModel">ACCGLOpeningBalanceModel.</param>
        /// <returns>Returns updated ACCGLOpeningBalanceResponse</returns>
        ACCGLOpeningBalanceResponse UpdateNonControlHeadType(ACCGLOpeningBalanceModel body);
    }
}

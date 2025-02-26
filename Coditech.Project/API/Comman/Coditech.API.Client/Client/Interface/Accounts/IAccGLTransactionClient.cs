using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IAccGLTransactionClient : IBaseClient
    {
        /// <summary>
        /// Get list of General Designation.
        /// </summary>
        /// <returns>AccGLTransactionListResponse</returns>
        AccGLTransactionListResponse List(string selectedCentreCode, int accSetupBalanceSheetId, short generalFinancialYearId, short accSetupTransactionTypeId ,byte accSetupBalanceSheetTypeId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Designation.
        /// </summary>
        /// <param name="AccGLTransactionModel">AccGLTransactionModel.</param>
        /// <returns>Returns AccGLTransactionResponse.</returns>
        AccGLTransactionResponse CreateGLTransaction(AccGLTransactionModel body);

        /// <summary>
        /// Get Designation by designationId.
        /// </summary>
        /// <param name="accGLTransactionId">designationId</param>
        /// <returns>Returns AccGLTransactionResponse.</returns>
        AccGLTransactionResponse GetGLTransaction(long accGLTransactionId);

        /// <summary>
        /// Update Designation.
        /// </summary>
        /// <param name="AccGLTransactionModel">AccGLTransactionModel.</param>
        /// <returns>Returns updated AccGLTransactionResponse</returns>
        AccGLTransactionResponse UpdateGLTransaction(AccGLTransactionModel body);

        ///// <summary>
        ///// Delete Designation.
        ///// </summary>
        ///// <param name="ParameterModel">ParameterModel.</param>
        ///// <returns>Returns true if deleted successfully else return false.</returns>
        //TrueFalseResponse DeleteBalanceSheet(ParameterModel body);
        ////AccGLTransactionListResponse List(string selectedCentreCode, , object value, FilterCollection filters, SortCollection sortlist, int pageIndex, int pageSize);
    }
}

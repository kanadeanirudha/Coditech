using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;

namespace Coditech.API.Client
{
    public interface IAccGLTransactionClient : IBaseClient
    {

        /// <summary>
        /// Create Designation.
        /// </summary>
        /// <param name="AccGLTransactionModel">AccGLTransactionModel.</param>
        /// <returns>Returns AccGLTransactionResponse.</returns>
        AccGLTransactionResponse CreateGLTransaction(AccGLTransactionModel body);

        /// <param name="searchKeyword">Search keyword</param>
        /// <param name="accSetupGLId">Account ID</param>
        /// <param name="userType">Person Type</param>
        /// <param name= "transactionTypeCode" > Transaction Type Code</param>
        // <returns>List of AccSetupGLAccountResponse</returns>
        AccGLTransactionListResponse GetAccSetupGLAccountList(string searchKeyword, int accSetupGLId, string userType, string transactionTypeCode);

        /// <param name="searchKeyword">Search keyword</param>
        /// <param name="userTypeId">User Type</param>
        /// <param name= "transactionTypeCode" > Transaction Type Code</param>
        // <returns>List of AccSetupGLAccountResponse</returns>
        AccGLTransactionListResponse GetPersons(string searchKeyword, int userTypeId, int balanceSheet);

        ///// <summary>
        ///// Delete Designation.
        ///// </summary>
        ///// <param name="ParameterModel">ParameterModel.</param>
        ///// <returns>Returns true if deleted successfully else return false.</returns>
        //TrueFalseResponse DeleteBalanceSheet(ParameterModel body);
        ////AccGLTransactionListResponse List(string selectedCentreCode, , object value, FilterCollection filters, SortCollection sortlist, int pageIndex, int pageSize);
    }
}

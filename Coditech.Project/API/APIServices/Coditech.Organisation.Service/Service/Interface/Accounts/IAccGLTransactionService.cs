using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IAccGLTransactionService
    {
        AccGLTransactionListModel AccGLTransactionList(string selectedCentreCode, int accSetupBalanceSheetId, short generalFinancialYearId, short accSetupTransactionTypeId, byte accSetupBalanceSheetTypeId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        AccGLTransactionModel CreateGLTransaction(AccGLTransactionModel model);
        AccGLTransactionModel GetGLTransaction(long AccGLTransactionId);

        List<AccGLTransactionModel> GetAccSetupGLAccountList(string searchKeyword, int accSetupGLId, string userType, string transactionTypeCode);
        bool UpdateGLTransaction(AccGLTransactionModel model);
        //bool DeleteBalanceSheet(ParameterModel parameterModel);
    }
}


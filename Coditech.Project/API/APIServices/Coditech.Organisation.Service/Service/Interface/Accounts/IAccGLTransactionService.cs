using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public interface IAccGLTransactionService
    {
        //AccGLTransactionListModel AccGLTransactionList(string selectedCentreCode, int accSetupBalanceSheetId, short generalFinancialYearId, short accSetupTransactionTypeId, byte accSetupBalanceSheetTypeId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        bool CreateGLTransaction(AccGLTransactionModel model);
        List<AccGLTransactionModel> GetAccSetupGLAccountList(string searchKeyword, int accSetupGLId, string userType, string transactionTypeCode, int balanceSheet);
        List<AccGLTransactionModel> GetPersons(string searchKeyword, int userTypeId, int balaceSheet);
        //bool DeleteBalanceSheet(ParameterModel parameterModel);
    }
}


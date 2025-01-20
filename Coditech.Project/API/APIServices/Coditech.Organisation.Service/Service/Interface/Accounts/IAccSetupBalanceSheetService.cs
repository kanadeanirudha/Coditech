using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IAccSetupBalanceSheetService
    {
        AccSetupBalanceSheetListModel GetBalanceSheetList(string selectedCentreCode, byte accSetupBalanceSheetId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        AccSetupBalanceSheetModel CreateBalanceSheet(AccSetupBalanceSheetModel model);
        AccSetupBalanceSheetModel GetBalanceSheet(int AccSetupBalanceSheetId);
        bool UpdateBalanceSheet(AccSetupBalanceSheetModel model);
        bool DeleteBalanceSheet(ParameterModel parameterModel);
    }
}


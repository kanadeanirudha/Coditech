using System.Collections.Specialized;
using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Service
{
    public interface IAccountReportService
    {
        AccountBalanceSheetReportListModel GetBalanceSheetReportList(string selectedCentreCode, string selectedParameter1, string selectedParameter2, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
    }
}

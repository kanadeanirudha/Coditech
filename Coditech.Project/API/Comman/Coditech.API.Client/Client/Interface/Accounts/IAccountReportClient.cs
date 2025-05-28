using Coditech.Common.API.Model.Response;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IAccountReportClient : IBaseClient
    {
        /// <summary>
        /// Get Account BalanceSheet Report List
        /// </summary>
        /// <returns>AccountBalanceSheetReportListResponse</returns>
        AccountBalanceSheetReportListResponse GetBalanceSheetReportList(string selectedCentreCode, string selectedParameter1, string selectedParameter2, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);
    }
}

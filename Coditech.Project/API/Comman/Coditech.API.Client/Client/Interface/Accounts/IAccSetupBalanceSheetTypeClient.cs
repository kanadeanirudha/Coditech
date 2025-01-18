using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
namespace Coditech.API.Client
{
    public interface IAccSetupBalanceSheetTypeClient : IBaseClient
    {
        /// <summary>
        /// Get list of Balance Sheet.
        /// </summary>
        /// <returns>PaymentGatewaysListResponse</returns>
        AccSetupBalanceSheetTypeListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);  
    }
}

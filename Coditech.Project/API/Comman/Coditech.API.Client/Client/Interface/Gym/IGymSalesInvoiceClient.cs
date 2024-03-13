using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGymSalesInvoiceClient : IBaseClient
    {
        /// <summary>
        /// Get Gym Service Sales Invoice List
        /// </summary>
        /// <returns>GymMemberSalesInvoiceListResponse</returns>
        GymMemberSalesInvoiceListResponse GymMemberServiceSalesInvoiceList(string selectedCentreCode, DateTime? toDate, DateTime? fromDate, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);
    }
}

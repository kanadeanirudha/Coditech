using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface ITaskApprovalSettingClient : IBaseClient
    {
        /// <summary>
        /// Get list of Task Approval Setting.
        /// </summary>
        /// <returns>TaskApprovalSettingListResponse</returns>
        TaskApprovalSettingListResponse List(string SelectedCentreCode, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);
            
    }
}

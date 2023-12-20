using Coditech.Common.API.Model.Response;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGymMemberDetailsClient : IBaseClient
    {
        /// <summary>
        /// Get list of Gym Member.
        /// </summary>
        /// <returns>GymMemberDetailsListResponse</returns>
        GymMemberDetailsListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);
    }
}

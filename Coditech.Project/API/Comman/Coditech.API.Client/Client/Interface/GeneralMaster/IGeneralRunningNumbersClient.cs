using Coditech.Common.API.Model.Response;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralRunningNumbersClient : IBaseClient
    {
        /// <summary>
        /// Get list of GeneralRunningNumbers.
        /// </summary>
        /// <returns>GeneralRunningNumbersListResponse</returns>
        GeneralRunningNumbersListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);
    }
}

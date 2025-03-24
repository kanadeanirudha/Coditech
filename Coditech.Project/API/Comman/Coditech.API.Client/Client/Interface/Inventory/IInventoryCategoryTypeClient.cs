using Coditech.Common.API.Model.Response;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IInventoryCategoryTypeClient : IBaseClient
    {
        /// <summary>
        /// Get list of Inventory Category Type.
        /// </summary>
        /// <returns>InventoryCategoryTypeListResponse</returns>
        InventoryCategoryTypeListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);
    }
}

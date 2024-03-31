using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IInventoryCategoryClient : IBaseClient
    {
        /// <summary>
        /// Get list of Inventory Category.
        /// </summary>
        /// <returns>InventoryCategoryListResponse</returns>
        InventoryCategoryListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create InventoryCategory.
        /// </summary>
        /// <param name="InventoryCategoryModel">InventoryCategoryModel.</param>
        /// <returns>Returns InventoryCategoryResponse.</returns>
        InventoryCategoryResponse CreateInventoryCategory(InventoryCategoryModel body);

        /// <summary>
        /// GetInventoryCategory by InventoryCategoryId.
        /// </summary>
        /// <param name="InventoryCategoryId">InventoryCategoryId</param>
        /// <returns>Returns InventoryCategoryResponse.</returns>
        InventoryCategoryResponse GetInventoryCategory(short InventoryCategoryId);

        /// <summary>
        /// UpdateInventoryCategory.
        /// </summary>
        /// <param name="InventoryCategoryModel">InventoryCategoryModel.</param>
        /// <returns>Returns updated InventoryCategoryResponse</returns>
        InventoryCategoryResponse UpdateInventoryCategory(InventoryCategoryModel body);

        /// <summary>
        /// DeleteInventoryCategory.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteInventoryCategory(ParameterModel body);
    }
}

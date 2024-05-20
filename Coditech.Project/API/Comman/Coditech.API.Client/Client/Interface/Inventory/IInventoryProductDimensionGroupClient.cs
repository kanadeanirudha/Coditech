using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IInventoryProductDimensionGroupClient : IBaseClient
    {
        /// <summary>
        /// Get list of General InventoryProductDimensionGroup.
        /// </summary>
        /// <returns>InventoryProductDimensionGroupListResponse</returns>
        InventoryProductDimensionGroupListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create InventoryProductDimensionGroup.
        /// </summary>
        /// <param name="InventoryProductDimensionGroupModel">InventoryProductDimensionGroupModel.</param>
        /// <returns>Returns InventoryProductDimensionGroupResponse.</returns>
        InventoryProductDimensionGroupResponse CreateInventoryProductDimensionGroup(InventoryProductDimensionGroupModel body);

        /// <summary>
        /// Get InventoryProductDimensionGroup by inventoryProductDimensionGroupId.
        /// </summary>
        /// <param name="inventoryProductDimensionGroupId">inventoryProductDimensionGroupId</param>
        /// <returns>Returns InventoryProductDimensionGroupResponse.</returns>
        InventoryProductDimensionGroupResponse GetInventoryProductDimensionGroup(int inventoryProductDimensionGroupId);

        /// <summary>
        /// Update InventoryProductDimensionGroup.
        /// </summary>
        /// <param name="InventoryProductDimensionGroupModel">InventoryProductDimensionGroupModel.</param>
        /// <returns>Returns updated InventoryProductDimensionGroupResponse</returns>
        InventoryProductDimensionGroupResponse UpdateInventoryProductDimensionGroup(InventoryProductDimensionGroupModel body);

        /// <summary>
        /// Delete InventoryProductDimensionGroup.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteInventoryProductDimensionGroup(ParameterModel body);
    }
}

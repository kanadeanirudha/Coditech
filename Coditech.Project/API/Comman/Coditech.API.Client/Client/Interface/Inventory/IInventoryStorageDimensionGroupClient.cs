using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IInventoryStorageDimensionGroupClient : IBaseClient
    {
        /// <summary>
        /// Get list of General InventoryStorageDimensionGroup.
        /// </summary>
        /// <returns>InventoryStorageDimensionGroupListResponse</returns>
        InventoryStorageDimensionGroupListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create InventoryStorageDimensionGroup.
        /// </summary>
        /// <param name="InventoryStorageDimensionGroupModel">InventoryStorageDimensionGroupModel.</param>
        /// <returns>Returns InventoryStorageDimensionGroupResponse.</returns>
        InventoryStorageDimensionGroupResponse CreateInventoryStorageDimensionGroup(InventoryStorageDimensionGroupModel body);

        /// <summary>
        /// Get InventoryStorageDimensionGroup by inventoryStorageDimensionGroupId.
        /// </summary>
        /// <param name="inventoryStorageDimensionGroupId">inventoryStorageDimensionGroupId</param>
        /// <returns>Returns InventoryStorageDimensionGroupResponse.</returns>
        InventoryStorageDimensionGroupResponse GetInventoryStorageDimensionGroup(int inventoryStorageDimensionGroupId);

        /// <summary>
        /// Update InventoryStorageDimensionGroup.
        /// </summary>
        /// <param name="InventoryStorageDimensionGroupModel">InventoryStorageDimensionGroupModel.</param>
        /// <returns>Returns updated InventoryStorageDimensionGroupResponse</returns>
        InventoryStorageDimensionGroupResponse UpdateInventoryStorageDimensionGroup(InventoryStorageDimensionGroupModel body);

        /// <summary>
        /// Delete InventoryStorageDimensionGroup.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteInventoryStorageDimensionGroup(ParameterModel body);
    }
}

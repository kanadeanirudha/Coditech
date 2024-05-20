using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IInventoryItemTrackingDimensionGroupClient : IBaseClient
    {
        /// <summary>
        /// Get list of General InventoryItemTrackingDimensionGroup.
        /// </summary>
        /// <returns>InventoryItemTrackingDimensionGroupListResponse</returns>
        InventoryItemTrackingDimensionGroupListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create InventoryItemTrackingDimensionGroup.
        /// </summary>
        /// <param name="InventoryItemTrackingDimensionGroupModel">InventoryItemTrackingDimensionGroupModel.</param>
        /// <returns>Returns InventoryItemTrackingDimensionGroupResponse.</returns>
        InventoryItemTrackingDimensionGroupResponse CreateInventoryItemTrackingDimensionGroup(InventoryItemTrackingDimensionGroupModel body);

        /// <summary>
        /// Get InventoryItemTrackingDimensionGroup by inventoryItemTrackingDimensionGroupId.
        /// </summary>
        /// <param name="inventoryItemTrackingDimensionGroupId">inventoryItemTrackingDimensionGroupId</param>
        /// <returns>Returns InventoryItemTrackingDimensionGroupResponse.</returns>
        InventoryItemTrackingDimensionGroupResponse GetInventoryItemTrackingDimensionGroup(int inventoryItemTrackingDimensionGroupId);

        /// <summary>
        /// Update InventoryItemTrackingDimensionGroup.
        /// </summary>
        /// <param name="InventoryItemTrackingDimensionGroupModel">InventoryItemTrackingDimensionGroupModel.</param>
        /// <returns>Returns updated InventoryItemTrackingDimensionGroupResponse</returns>
        InventoryItemTrackingDimensionGroupResponse UpdateInventoryItemTrackingDimensionGroup(InventoryItemTrackingDimensionGroupModel body);

        /// <summary>
        /// Delete InventoryItemTrackingDimensionGroup.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteInventoryItemTrackingDimensionGroup(ParameterModel body);
    }
}

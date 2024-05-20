using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IInventoryItemModelGroupClient : IBaseClient
    {
        /// <summary>
        /// Get list of inventory Item Model Group.
        /// </summary>
        /// <returns>InventoryItemModelGroupListResponse</returns>
        InventoryItemModelGroupListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create InventoryItemModelGroup.
        /// </summary>
        /// <param name="InventoryItemModelGroupModel">InventoryItemModelGroupModel.</param>
        /// <returns>Returns InventoryItemModelGroupResponse.</returns>
        InventoryItemModelGroupResponse CreateInventoryItemModelGroup(InventoryItemModelGroupModel body);

        /// <summary>
        /// Get InventoryItemModelGroup by inventoryItemModelGroupId.
        /// </summary>
        /// <param name="inventoryItemModelGroupId">inventoryItemModelGroupId</param>
        /// <returns>Returns InventoryItemModelGroupResponse.</returns>
        InventoryItemModelGroupResponse GetInventoryItemModelGroup(short inventoryItemModelGroupId);

        /// <summary>
        /// Update InventoryItemModelGroup.
        /// </summary>
        /// <param name="InventoryItemModelGroupModel">InventoryItemModelGroupModel.</param>
        /// <returns>Returns updated InventoryItemModelGroupResponse</returns>
        InventoryItemModelGroupResponse UpdateInventoryItemModelGroup(InventoryItemModelGroupModel body);

        /// <summary>
        /// Delete InventoryItemModelGroup.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteInventoryItemModelGroup(ParameterModel body);
    }
}

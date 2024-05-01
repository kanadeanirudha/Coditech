using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IInventoryItemGroupClient : IBaseClient
    {
        /// <summary>
        /// Get list of InventoryItemGroup.
        /// </summary>
        /// <returns>InventoryItemGroupListResponse</returns>
        InventoryItemGroupListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create InventoryItemGroup.
        /// </summary>
        /// <param name="InventoryItemGroupModel">InventoryItemGroupModel.</param>
        /// <returns>Returns InventoryItemGroupResponse.</returns>
        InventoryItemGroupResponse CreateInventoryItemGroup(InventoryItemGroupModel body);

        /// <summary>
        /// Get InventoryItemGroup by InventoryItemGroupId.
        /// </summary>
        /// <param name="InventoryItemGroupId">InventoryItemGroupId</param>
        /// <returns>Returns InventoryItemGroupResponse.</returns>
        InventoryItemGroupResponse GetInventoryItemGroup(short InventoryItemGroupId);

        /// <summary>
        /// Update InventoryItemGroup.
        /// </summary>
        /// <param name="InventoryItemGroupModel">InventoryItemGroupModel.</param>
        /// <returns>Returns updated InventoryItemGroupResponse</returns>
        InventoryItemGroupResponse UpdateInventoryItemGroup(InventoryItemGroupModel body);

        /// <summary>
        /// Delete InventoryItemGroup.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteInventoryItemGroup(ParameterModel body);
    }
}

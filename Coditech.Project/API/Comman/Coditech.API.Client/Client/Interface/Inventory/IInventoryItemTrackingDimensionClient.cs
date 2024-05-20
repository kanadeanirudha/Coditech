using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IInventoryItemTrackingDimensionClient : IBaseClient
    {
        /// <summary>
        /// Get list of Inventory Category.
        /// </summary>
        /// <returns>InventoryItemTrackingDimensionListResponse</returns>
        InventoryItemTrackingDimensionListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create InventoryItemTrackingDimension.
        /// </summary>
        /// <param name="InventoryItemTrackingDimensionModel">InventoryItemTrackingDimensionModel.</param>
        /// <returns>Returns InventoryItemTrackingDimensionResponse.</returns>
        InventoryItemTrackingDimensionResponse CreateInventoryItemTrackingDimension(InventoryItemTrackingDimensionModel body);

        /// <summary>
        /// GetInventoryItemTrackingDimension by InventoryItemTrackingDimensionId.
        /// </summary>
        /// <param name="InventoryItemTrackingDimensionId">InventoryItemTrackingDimensionId</param>
        /// <returns>Returns InventoryItemTrackingDimensionResponse.</returns>
        InventoryItemTrackingDimensionResponse GetInventoryItemTrackingDimension(short InventoryItemTrackingDimensionId);

        /// <summary>
        /// UpdateInventoryItemTrackingDimension.
        /// </summary>
        /// <param name="InventoryItemTrackingDimensionModel">InventoryItemTrackingDimensionModel.</param>
        /// <returns>Returns updated InventoryItemTrackingDimensionResponse</returns>
        InventoryItemTrackingDimensionResponse UpdateInventoryItemTrackingDimension(InventoryItemTrackingDimensionModel body);

        /// <summary>
        /// DeleteInventoryItemTrackingDimension.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteInventoryItemTrackingDimension(ParameterModel body);
    }
}

using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IInventoryItemStorageDimensionClient : IBaseClient
    {
        /// <summary>
        /// Get list of Inventory Category.
        /// </summary>
        /// <returns>InventoryItemStorageDimensionListResponse</returns>
        InventoryItemStorageDimensionListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create InventoryItemStorageDimension.
        /// </summary>
        /// <param name="InventoryItemStorageDimensionModel">InventoryItemStorageDimensionModel.</param>
        /// <returns>Returns InventoryItemStorageDimensionResponse.</returns>
        InventoryItemStorageDimensionResponse CreateInventoryItemStorageDimension(InventoryItemStorageDimensionModel body);

        /// <summary>
        /// GetInventoryItemStorageDimension by InventoryItemStorageDimensionId.
        /// </summary>
        /// <param name="InventoryItemStorageDimensionId">InventoryItemStorageDimensionId</param>
        /// <returns>Returns InventoryItemStorageDimensionResponse.</returns>
        InventoryItemStorageDimensionResponse GetInventoryItemStorageDimension(short InventoryItemStorageDimensionId);

        /// <summary>
        /// UpdateInventoryItemStorageDimension.
        /// </summary>
        /// <param name="InventoryItemStorageDimensionModel">InventoryItemStorageDimensionModel.</param>
        /// <returns>Returns updated InventoryItemStorageDimensionResponse</returns>
        InventoryItemStorageDimensionResponse UpdateInventoryItemStorageDimension(InventoryItemStorageDimensionModel body);

        /// <summary>
        /// DeleteInventoryItemStorageDimension.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteInventoryItemStorageDimension(ParameterModel body);
    }
}

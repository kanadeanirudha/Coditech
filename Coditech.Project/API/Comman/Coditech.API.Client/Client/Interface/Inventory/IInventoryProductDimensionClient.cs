using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IInventoryProductDimensionClient : IBaseClient
    {
        /// <summary>
        /// Get list of Inventory Category.
        /// </summary>
        /// <returns>InventoryProductDimensionListResponse</returns>
        InventoryProductDimensionListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create InventoryProductDimension.
        /// </summary>
        /// <param name="InventoryProductDimensionModel">InventoryProductDimensionModel.</param>
        /// <returns>Returns InventoryProductDimensionResponse.</returns>
        InventoryProductDimensionResponse CreateInventoryProductDimension(InventoryProductDimensionModel body);

        /// <summary>
        /// GetInventoryProductDimension by InventoryProductDimensionId.
        /// </summary>
        /// <param name="InventoryProductDimensionId">InventoryProductDimensionId</param>
        /// <returns>Returns InventoryProductDimensionResponse.</returns>
        InventoryProductDimensionResponse GetInventoryProductDimension(short InventoryProductDimensionId);

        /// <summary>
        /// UpdateInventoryProductDimension.
        /// </summary>
        /// <param name="InventoryProductDimensionModel">InventoryProductDimensionModel.</param>
        /// <returns>Returns updated InventoryProductDimensionResponse</returns>
        InventoryProductDimensionResponse UpdateInventoryProductDimension(InventoryProductDimensionModel body);

        /// <summary>
        /// DeleteInventoryProductDimension.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteInventoryProductDimension(ParameterModel body);
    }
}

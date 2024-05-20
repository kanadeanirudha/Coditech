using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IInventoryUoMMasterClient : IBaseClient
    {
        /// <summary>
        /// Get list of Inventory Category.
        /// </summary>
        /// <returns>InventoryUoMMasterListResponse</returns>
        InventoryUoMMasterListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create InventoryUoMMaster.
        /// </summary>
        /// <param name="InventoryUoMMasterModel">InventoryUoMMasterModel.</param>
        /// <returns>Returns InventoryUoMMasterResponse.</returns>
        InventoryUoMMasterResponse CreateInventoryUoMMaster(InventoryUoMMasterModel body);

        /// <summary>
        /// GetInventoryUoMMaster by InventoryUoMMasterId.
        /// </summary>
        /// <param name="InventoryUoMMasterId">InventoryUoMMasterId</param>
        /// <returns>Returns InventoryUoMMasterResponse.</returns>
        InventoryUoMMasterResponse GetInventoryUoMMaster(short InventoryUoMMasterId);

        /// <summary>
        /// UpdateInventoryUoMMaster.
        /// </summary>
        /// <param name="InventoryUoMMasterModel">InventoryUoMMasterModel.</param>
        /// <returns>Returns updated InventoryUoMMasterResponse</returns>
        InventoryUoMMasterResponse UpdateInventoryUoMMaster(InventoryUoMMasterModel body);

        /// <summary>
        /// DeleteInventoryUoMMaster.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteInventoryUoMMaster(ParameterModel body);
    }
}

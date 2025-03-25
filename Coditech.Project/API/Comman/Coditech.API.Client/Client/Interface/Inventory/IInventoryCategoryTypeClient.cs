using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IInventoryCategoryTypeClient : IBaseClient
    {
        /// <summary>
        /// Get list of Inventory Category Type.
        /// </summary>
        /// <returns>InventoryCategoryTypeListResponse</returns>
        InventoryCategoryTypeListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);
        /// <summary>
        /// Create InventoryCategoryType.
        /// </summary>
        /// <param name="InventoryCategoryTypeModel">InventoryCategoryTypeModel.</param>
        /// <returns>Returns InventoryCategoryTypeResponse.</returns>
        InventoryCategoryTypeResponse CreateInventoryCategoryType(InventoryCategoryTypeModel body);

        /// <summary>
        /// Get InventoryCategoryType by InventoryCategoryTypeMasterId.
        /// </summary>
        /// <param name="inventoryCategoryTypeMasterId">InventoryCategoryTypeMasterId</param>
        /// <returns>Returns InventoryCategoryTypeResponse.</returns>
        InventoryCategoryTypeResponse GetInventoryCategoryType(byte inventoryCategoryTypeMasterId);

        /// <summary>
        /// Update InventoryCategoryType.
        /// </summary>
        /// <param name="InventoryCategoryTypeModel">InventoryCategoryTypeModel.</param>
        /// <returns>Returns updated InventoryCategoryTypeResponse</returns>
        InventoryCategoryTypeResponse UpdateInventoryCategoryType(InventoryCategoryTypeModel body);

        /// <summary>
        /// Delete InventoryCategoryType.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteInventoryCategoryType(ParameterModel body);
    }
}

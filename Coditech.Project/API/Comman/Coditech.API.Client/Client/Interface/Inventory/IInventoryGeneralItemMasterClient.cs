using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses.Inventory.InventoryGeneralItemMaster;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IInventoryGeneralItemMasterClient : IBaseClient
    {
        /// <summary>
        /// Get list of Inventory General Item Master.
        /// </summary>
        /// <returns>InventoryGeneralItemMasterListResponse</returns>
        InventoryGeneralItemMasterListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Inventory General Item Master.
        /// </summary>
        /// <param name="InventoryGeneralItemMasterModel">InventoryGeneralItemMasterModel.</param>
        /// <returns>Returns InventoryGeneralItemMasterResponse.</returns>
        InventoryGeneralItemMasterResponse CreateInventoryGeneralItemMaster(InventoryGeneralItemMasterModel body);

        /// <summary>
        /// Get Inventory General Item Master by inventoryGeneralItemMasterId.
        /// </summary>
        /// <param name="inventoryGeneralItemMasterId">inventoryGeneralItemMasterId</param>
        /// <returns>Returns InventoryGeneralItemMasterResponse.</returns>
        InventoryGeneralItemMasterResponse GetInventoryGeneralItemMaster(int inventoryGeneralItemMasterId);

        /// <summary>
        /// Update Inventory General Item Master.
        /// </summary>
        /// <param name="InventoryGeneralItemMasterModel">InventoryGeneralItemMasterModel.</param>
        /// <returns>Returns updated InventoryGeneralItemMasterResponse</returns>
        InventoryGeneralItemMasterResponse UpdateInventoryGeneralItemMaster(InventoryGeneralItemMasterModel body);

        /// <summary>
        /// Delete Inventory General Item Master.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteInventoryGeneralItemMaster(ParameterModel body);

        /// <summary>
        /// Get list of Inventory General Services.
        /// </summary>
        /// <returns>InventoryGeneralItemMasterListResponse</returns>
        InventoryGeneralItemMasterListResponse GetGeneralServicesList(string searchText);
    }
}

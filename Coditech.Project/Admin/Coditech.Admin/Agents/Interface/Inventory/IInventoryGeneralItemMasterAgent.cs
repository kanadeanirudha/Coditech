using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Responses.Inventory.InventoryGeneralItemMaster;

namespace Coditech.Admin.Agents
{
    public interface IInventoryGeneralItemMasterAgent
    {
        /// <summary>
        /// Get list of General InventoryGeneralItemMaster.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>InventoryGeneralItemMasterListViewModel</returns>
        InventoryGeneralItemMasterListViewModel GetInventoryGeneralItemMasterList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create InventoryGeneralItemMaster.
        /// </summary>
        /// <param name="inventoryGeneralItemMasterViewModel">General InventoryGeneralItemMaster View Model.</param>
        /// <returns>Returns created model.</returns>
        InventoryGeneralItemMasterViewModel CreateInventoryGeneralItemMaster(InventoryGeneralItemMasterViewModel inventoryGeneralItemMasterViewModel);

        /// <summary>
        /// Get InventoryGeneralItemMaster by inventoryGeneralItemMasterId.
        /// </summary>
        /// <param name="inventoryGeneralItemMasterId">inventoryGeneralItemMasterId</param>
        /// <returns>Returns InventoryGeneralItemMasterViewModel.</returns>
        InventoryGeneralItemMasterViewModel GetInventoryGeneralItemMaster(int inventoryGeneralItemMasterId);

        /// <summary>
        /// Update InventoryGeneralItemMaster.
        /// </summary>
        /// <param name="inventoryGeneralItemMasterViewModel">inventoryGeneralItemMasterViewModel.</param>
        /// <returns>Returns updated InventoryGeneralItemMasterViewModel</returns>
        InventoryGeneralItemMasterViewModel UpdateInventoryGeneralItemMaster(InventoryGeneralItemMasterViewModel inventoryGeneralItemMasterViewModel);

        /// <summary>
        /// Delete InventoryGeneralItemMaster.
        /// </summary>
        /// <param name="inventoryGeneralItemMasterId">inventoryGeneralItemMasterId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteInventoryGeneralItemMaster(string inventoryGeneralItemMasterId, out string errorMessage);
        InventoryGeneralItemMasterListResponse GetInventoryGeneralItemMasterList();
    }
}

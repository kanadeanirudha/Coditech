using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IInventoryItemGroupAgent
    {
        /// <summary>
        /// Get list of InventoryItemGroup.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>InventoryItemGroupListViewModel</returns>
        InventoryItemGroupListViewModel GetInventoryItemGroupList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create InventoryItemGroup.
        /// </summary>
        /// <param name="inventoryItemGroupViewModel">Inventory Item Group View Model.</param>
        /// <returns>Returns created model.</returns>
        InventoryItemGroupViewModel CreateInventoryItemGroup(InventoryItemGroupViewModel inventoryItemGroupViewModel);

        /// <summary>
        /// Get InventoryItemGroup by inventoryItemGroupId.
        /// </summary>
        /// <param name="inventoryItemGroupId">inventoryItemGroupId</param>
        /// <returns>Returns InventoryItemGroupViewModel.</returns>
        InventoryItemGroupViewModel GetInventoryItemGroup(short inventoryItemGroupId);

        /// <summary>
        /// Update Inventory Item Group.
        /// </summary>
        /// <param name="inventoryItemGroupViewModel">inventoryItemGroupViewModel.</param>
        /// <returns>Returns updated InventoryItemGroupViewModel</returns>
        InventoryItemGroupViewModel UpdateInventoryItemGroup(InventoryItemGroupViewModel inventoryItemGroupViewModel);

        /// <summary>
        /// Delete InventoryItemGroup.
        /// </summary>
        /// <param name="inventoryItemGroupId">inventoryItemGroupId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteInventoryItemGroup(string inventoryItemGroupId, out string errorMessage);
        InventoryItemGroupListResponse GetInventoryItemGroupList();
    }
}

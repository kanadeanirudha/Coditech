using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IInventoryItemModelGroupAgent
    {
        /// <summary>
        /// Get list of Inventory Item Model Group.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>InventoryItemModelGroupListViewModel</returns>
        InventoryItemModelGroupListViewModel GetInventoryItemModelGroupList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create InventoryItemModelGroup.
        /// </summary>
        /// <param name="inventoryItemModelGroupViewModel">Inventory Item Model Group View Model.</param>
        /// <returns>Returns created model.</returns>
        InventoryItemModelGroupViewModel CreateInventoryItemModelGroup(InventoryItemModelGroupViewModel inventoryItemModelGroupViewModel);

        /// <summary>
        /// Get InventoryItemModelGroup by inventoryItemModelGroupId.
        /// </summary>
        /// <param name="inventoryItemModelGroupId">inventoryItemModelGroupId</param>
        /// <returns>Returns InventoryItemModelGroupViewModel.</returns>
        InventoryItemModelGroupViewModel GetInventoryItemModelGroup(short InventoryItemModelGroupId);

        /// <summary>
        /// Update InventoryItemModelGroup.
        /// </summary>
        /// <param name="inventoryItemModelGroupViewModel">inventoryItemModelGroupViewModel.</param>
        /// <returns>Returns updated InventoryItemModelGroupViewModel</returns>
        InventoryItemModelGroupViewModel UpdateInventoryItemModelGroup(InventoryItemModelGroupViewModel inventoryItemModelGroupViewModel);

        /// <summary>
        /// Delete InventoryItemModelGroup.
        /// </summary>
        /// <param name="inventoryItemModelGroupId">inventoryItemModelGroupId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteInventoryItemModelGroup(string inventoryItemModelGroupId, out string errorMessage);
        InventoryItemModelGroupListResponse GetInventoryItemModelGroupList();
    }
}

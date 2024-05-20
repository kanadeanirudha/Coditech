using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IInventoryStorageDimensionGroupAgent
    {
        /// <summary>
        /// Get list of InventoryStorageDimensionGroup.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>InventoryStorageDimensionGroupListViewModel</returns>
        InventoryStorageDimensionGroupListViewModel GetInventoryStorageDimensionGroupList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create InventoryStorageDimensionGroup.
        /// </summary>
        /// <param name="inventoryStorageDimensionGroupViewModel">InventoryStorageDimensionGroup View Model.</param>
        /// <returns>Returns created model.</returns>
        InventoryStorageDimensionGroupViewModel CreateInventoryStorageDimensionGroup(InventoryStorageDimensionGroupViewModel inventoryStorageDimensionGroupViewModel);

        /// <summary>
        /// Get InventoryStorageDimensionGroup by inventoryStorageDimensionGroupId.
        /// </summary>
        /// <param name="inventoryStorageDimensionGroupId">inventoryStorageDimensionGroupId</param>
        /// <returns>Returns InventoryStorageDimensionGroupViewModel.</returns>
        InventoryStorageDimensionGroupViewModel GetInventoryStorageDimensionGroup(int inventoryStorageDimensionGroupId);

        /// <summary>
        /// Update InventoryStorageDimensionGroup.
        /// </summary>
        /// <param name="inventoryStorageDimensionGroupViewModel">inventoryStorageDimensionGroupViewModel.</param>
        /// <returns>Returns updated InventoryStorageDimensionGroupViewModel</returns>
        InventoryStorageDimensionGroupViewModel UpdateInventoryStorageDimensionGroup(InventoryStorageDimensionGroupViewModel inventoryStorageDimensionGroupViewModel);

        /// <summary>
        /// Delete InventoryStorageDimensionGroup.
        /// </summary>
        /// <param name="inventoryStorageDimensionGroupId">inventoryStorageDimensionGroupId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteInventoryStorageDimensionGroup(string inventoryStorageDimensionGroupId, out string errorMessage);
        InventoryStorageDimensionGroupListResponse GetInventoryStorageDimensionGroupList();
    }
}

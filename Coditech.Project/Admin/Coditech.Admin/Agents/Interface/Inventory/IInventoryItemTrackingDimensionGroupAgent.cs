using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IInventoryItemTrackingDimensionGroupAgent
    {
        /// <summary>
        /// Get list of InventoryItemTrackingDimensionGroup.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>InventoryItemTrackingDimensionGroupListViewModel</returns>
        InventoryItemTrackingDimensionGroupListViewModel GetInventoryItemTrackingDimensionGroupList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create InventoryItemTrackingDimensionGroup.
        /// </summary>
        /// <param name="inventoryItemTrackingDimensionGroupViewModel">InventoryItemTrackingDimensionGroup View Model.</param>
        /// <returns>Returns created model.</returns>
        InventoryItemTrackingDimensionGroupViewModel CreateInventoryItemTrackingDimensionGroup(InventoryItemTrackingDimensionGroupViewModel inventoryItemTrackingDimensionGroupViewModel);

        /// <summary>
        /// Get InventoryItemTrackingDimensionGroup by inventoryItemTrackingDimensionGroupId.
        /// </summary>
        /// <param name="inventoryItemTrackingDimensionGroupId">inventoryItemTrackingDimensionGroupId</param>
        /// <returns>Returns InventoryItemTrackingDimensionGroupViewModel.</returns>
        InventoryItemTrackingDimensionGroupViewModel GetInventoryItemTrackingDimensionGroup(int inventoryItemTrackingDimensionGroupId);

        /// <summary>
        /// Update InventoryItemTrackingDimensionGroup.
        /// </summary>
        /// <param name="inventoryItemTrackingDimensionGroupViewModel">inventoryItemTrackingDimensionGroupViewModel.</param>
        /// <returns>Returns updated InventoryItemTrackingDimensionGroupViewModel</returns>
        InventoryItemTrackingDimensionGroupViewModel UpdateInventoryItemTrackingDimensionGroup(InventoryItemTrackingDimensionGroupViewModel inventoryItemTrackingDimensionGroupViewModel);

        /// <summary>
        /// Delete InventoryItemTrackingDimensionGroup.
        /// </summary>
        /// <param name="inventoryItemTrackingDimensionGroupId">inventoryItemTrackingDimensionGroupId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteInventoryItemTrackingDimensionGroup(string inventoryItemTrackingDimensionGroupId, out string errorMessage);
        InventoryItemTrackingDimensionGroupListResponse GetInventoryItemTrackingDimensionGroupList();
    }
}

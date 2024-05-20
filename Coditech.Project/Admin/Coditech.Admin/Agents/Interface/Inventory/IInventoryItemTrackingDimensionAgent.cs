using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IInventoryItemTrackingDimensionAgent
    {
        /// <summary>
        /// Get list of General InventoryItemTrackingDimension.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>InventoryItemTrackingDimensionListViewModel</returns>
        InventoryItemTrackingDimensionListViewModel GetInventoryItemTrackingDimensionList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create InventoryItemTrackingDimension.
        /// </summary>
        /// <param name="inventoryItemTrackingDimensionViewModel"> InventoryItemTrackingDimension View Model.</param>
        /// <returns>Returns created model.</returns>
        InventoryItemTrackingDimensionViewModel CreateInventoryItemTrackingDimension(InventoryItemTrackingDimensionViewModel inventoryItemTrackingDimensionViewModel);

        /// <summary>
        /// Get InventoryItemTrackingDimension by InventoryItemTrackingDimensionId.
        /// </summary>
        /// <param name="inventoryItemTrackingDimensionId">inventoryItemTrackingDimensionId</param>
        /// <returns>Returns InventoryItemTrackingDimensionViewModel.</returns>
        InventoryItemTrackingDimensionViewModel GetInventoryItemTrackingDimension(short inventoryItemTrackingDimensionId);

        /// <summary>
        /// Update InventoryItemTrackingDimension.
        /// </summary>
        /// <param name="inventoryItemTrackingDimensionViewModel">inventoryItemTrackingDimensionViewModel.</param>
        /// <returns>Returns updated InventoryItemTrackingDimensionViewModel</returns>
        InventoryItemTrackingDimensionViewModel UpdateInventoryItemTrackingDimension(InventoryItemTrackingDimensionViewModel inventoryItemTrackingDimensionViewModel);

        /// <summary>
        /// Delete InventoryItemTrackingDimension.
        /// </summary>
        /// <param name="inventoryItemTrackingDimensionId">inventoryItemTrackingDimensionId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteInventoryItemTrackingDimension(string inventoryItemTrackingDimensionId, out string errorMessage);
        InventoryItemTrackingDimensionListResponse GetInventoryItemTrackingDimensionList();
    }
}

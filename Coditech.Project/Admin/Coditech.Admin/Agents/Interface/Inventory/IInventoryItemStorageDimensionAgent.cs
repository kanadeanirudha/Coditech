using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IInventoryItemStorageDimensionAgent
    {
        /// <summary>
        /// Get list of General InventoryItemStorageDimension.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>InventoryItemStorageDimensionListViewModel</returns>
        InventoryItemStorageDimensionListViewModel GetInventoryItemStorageDimensionList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create InventoryItemStorageDimension.
        /// </summary>
        /// <param name="inventoryItemStorageDimensionViewModel"> InventoryItemStorageDimension View Model.</param>
        /// <returns>Returns created model.</returns>
        InventoryItemStorageDimensionViewModel CreateInventoryItemStorageDimension(InventoryItemStorageDimensionViewModel inventoryItemStorageDimensionViewModel);

        /// <summary>
        /// Get InventoryItemStorageDimension by InventoryItemStorageDimensionId.
        /// </summary>
        /// <param name="inventoryItemStorageDimensionId">inventoryItemStorageDimensionId</param>
        /// <returns>Returns InventoryItemStorageDimensionViewModel.</returns>
        InventoryItemStorageDimensionViewModel GetInventoryItemStorageDimension(short inventoryItemStorageDimensionId);

        /// <summary>
        /// Update InventoryItemStorageDimension.
        /// </summary>
        /// <param name="inventoryItemStorageDimensionViewModel">inventoryItemStorageDimensionViewModel.</param>
        /// <returns>Returns updated InventoryItemStorageDimensionViewModel</returns>
        InventoryItemStorageDimensionViewModel UpdateInventoryItemStorageDimension(InventoryItemStorageDimensionViewModel inventoryItemStorageDimensionViewModel);

        /// <summary>
        /// Delete InventoryItemStorageDimension.
        /// </summary>
        /// <param name="inventoryItemStorageDimensionId">inventoryItemStorageDimensionId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteInventoryItemStorageDimension(string inventoryItemStorageDimensionId, out string errorMessage);
        InventoryItemStorageDimensionListResponse GetInventoryItemStorageDimensionList();
    }
}

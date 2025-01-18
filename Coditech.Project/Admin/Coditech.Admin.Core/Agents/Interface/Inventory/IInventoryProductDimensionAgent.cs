using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IInventoryProductDimensionAgent
    {
        /// <summary>
        /// Get list of General InventoryProductDimension.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>InventoryProductDimensionListViewModel</returns>
        InventoryProductDimensionListViewModel GetInventoryProductDimensionList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create InventoryProductDimension.
        /// </summary>
        /// <param name="inventoryProductDimensionViewModel"> InventoryProductDimension View Model.</param>
        /// <returns>Returns created model.</returns>
        InventoryProductDimensionViewModel CreateInventoryProductDimension(InventoryProductDimensionViewModel inventoryProductDimensionViewModel);

        /// <summary>
        /// Get InventoryProductDimension by InventoryProductDimensionId.
        /// </summary>
        /// <param name="inventoryProductDimensionId">inventoryProductDimensionId</param>
        /// <returns>Returns InventoryProductDimensionViewModel.</returns>
        InventoryProductDimensionViewModel GetInventoryProductDimension(short inventoryProductDimensionId);

        /// <summary>
        /// Update InventoryProductDimension.
        /// </summary>
        /// <param name="inventoryProductDimensionViewModel">inventoryProductDimensionViewModel.</param>
        /// <returns>Returns updated InventoryProductDimensionViewModel</returns>
        InventoryProductDimensionViewModel UpdateInventoryProductDimension(InventoryProductDimensionViewModel inventoryProductDimensionViewModel);

        /// <summary>
        /// Delete InventoryProductDimension.
        /// </summary>
        /// <param name="inventoryProductDimensionId">inventoryProductDimensionId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteInventoryProductDimension(string inventoryProductDimensionId, out string errorMessage);
        InventoryProductDimensionListResponse GetInventoryProductDimensionList();
    }
}

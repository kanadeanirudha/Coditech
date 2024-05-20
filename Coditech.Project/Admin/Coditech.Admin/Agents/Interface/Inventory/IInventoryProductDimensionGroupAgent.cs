using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IInventoryProductDimensionGroupAgent
    {
        /// <summary>
        /// Get list of InventoryProductDimensionGroup.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>InventoryProductDimensionGroupListViewModel</returns>
        InventoryProductDimensionGroupListViewModel GetInventoryProductDimensionGroupList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create InventoryProductDimensionGroup.
        /// </summary>
        /// <param name="inventoryProductDimensionGroupViewModel">InventoryProductDimensionGroup View Model.</param>
        /// <returns>Returns created model.</returns>
        InventoryProductDimensionGroupViewModel CreateInventoryProductDimensionGroup(InventoryProductDimensionGroupViewModel inventoryProductDimensionGroupViewModel);

        /// <summary>
        /// Get InventoryProductDimensionGroup by inventoryProductDimensionGroupId.
        /// </summary>
        /// <param name="inventoryProductDimensionGroupId">inventoryProductDimensionGroupId</param>
        /// <returns>Returns InventoryProductDimensionGroupViewModel.</returns>
        InventoryProductDimensionGroupViewModel GetInventoryProductDimensionGroup(int inventoryProductDimensionGroupId);

        /// <summary>
        /// Update InventoryProductDimensionGroup.
        /// </summary>
        /// <param name="inventoryProductDimensionGroupViewModel">inventoryProductDimensionGroupViewModel.</param>
        /// <returns>Returns updated InventoryProductDimensionGroupViewModel</returns>
        InventoryProductDimensionGroupViewModel UpdateInventoryProductDimensionGroup(InventoryProductDimensionGroupViewModel inventoryProductDimensionGroupViewModel);

        /// <summary>
        /// Delete InventoryProductDimensionGroup.
        /// </summary>
        /// <param name="inventoryProductDimensionGroupId">inventoryProductDimensionGroupId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteInventoryProductDimensionGroup(string inventoryProductDimensionGroupId, out string errorMessage);
        InventoryProductDimensionGroupListResponse GetInventoryProductDimensionGroupList();
    }
}

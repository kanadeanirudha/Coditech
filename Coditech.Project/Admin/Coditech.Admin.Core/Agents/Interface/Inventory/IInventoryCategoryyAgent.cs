using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IInventoryCategoryAgent
    {
        /// <summary>
        /// Get list of General InventoryCategory.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>InventoryCategoryListViewModel</returns>
        InventoryCategoryListViewModel GetInventoryCategoryList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create InventoryCategory.
        /// </summary>
        /// <param name="inventoryCategoryViewModel"> InventoryCategory View Model.</param>
        /// <returns>Returns created model.</returns>
        InventoryCategoryViewModel CreateInventoryCategory(InventoryCategoryViewModel inventoryCategoryViewModel);

        /// <summary>
        /// Get InventoryCategory by InventoryCategoryId.
        /// </summary>
        /// <param name="inventoryCategoryId">inventoryCategoryId</param>
        /// <returns>Returns InventoryCategoryViewModel.</returns>
        InventoryCategoryViewModel GetInventoryCategory(short inventoryCategoryId);

        /// <summary>
        /// Update InventoryCategory.
        /// </summary>
        /// <param name="inventoryCategoryViewModel">inventoryCategoryViewModel.</param>
        /// <returns>Returns updated InventoryCategoryViewModel</returns>
        InventoryCategoryViewModel UpdateInventoryCategory(InventoryCategoryViewModel inventoryCategoryViewModel);

        /// <summary>
        /// Delete InventoryCategory.
        /// </summary>
        /// <param name="inventoryCategoryId">inventoryCategoryId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteInventoryCategory(string inventoryCategoryId, out string errorMessage);
        InventoryCategoryListResponse GetInventoryCategoryList();
    }
}

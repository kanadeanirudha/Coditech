using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IInventoryUoMMasterAgent
    {
        /// <summary>
        /// Get list of General InventoryUoMMaster.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>InventoryUoMMasterListViewModel</returns>
        InventoryUoMMasterListViewModel GetInventoryUoMMasterList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create InventoryUoMMaster.
        /// </summary>
        /// <param name="inventoryUoMMasterViewModel"> InventoryUoMMaster View Model.</param>
        /// <returns>Returns created model.</returns>
        InventoryUoMMasterViewModel CreateInventoryUoMMaster(InventoryUoMMasterViewModel inventoryUoMMasterViewModel);

        /// <summary>
        /// Get InventoryUoMMaster by InventoryUoMMasterId.
        /// </summary>
        /// <param name="inventoryUoMMasterId">inventoryUoMMasterId</param>
        /// <returns>Returns InventoryUoMMasterViewModel.</returns>
        InventoryUoMMasterViewModel GetInventoryUoMMaster(short inventoryUoMMasterId);

        /// <summary>
        /// Update InventoryUoMMaster.
        /// </summary>
        /// <param name="inventoryUoMMasterViewModel">inventoryUoMMasterViewModel.</param>
        /// <returns>Returns updated InventoryUoMMasterViewModel</returns>
        InventoryUoMMasterViewModel UpdateInventoryUoMMaster(InventoryUoMMasterViewModel inventoryUoMMasterViewModel);

        /// <summary>
        /// Delete InventoryUoMMaster.
        /// </summary>
        /// <param name="inventoryUoMMasterId">inventoryUoMMasterId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteInventoryUoMMaster(string inventoryUoMMasterId, out string errorMessage);
        InventoryUoMMasterListResponse GetInventoryUoMMasterList();
    }
}

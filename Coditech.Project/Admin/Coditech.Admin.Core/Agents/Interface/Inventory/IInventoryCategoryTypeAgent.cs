using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IInventoryCategoryTypeAgent
    {
        /// <summary>
        /// Get list of InventoryCategoryType.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>InventoryCategoryTypeListViewModel</returns>
        InventoryCategoryTypeListViewModel GetInventoryCategoryTypeList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create InventoryCategoryType.
        /// </summary>
        /// <param name="inventoryCategoryTypeViewModel"> Inventory Category Type View Model.</param>
        /// <returns>Returns created model.</returns>
        InventoryCategoryTypeViewModel CreateInventoryCategoryType(InventoryCategoryTypeViewModel inventoryCategoryTypeViewModel);

        /// <summary>
        /// Get InventoryCategoryType by InventoryCategoryTypeMasterId.
        /// </summary>
        /// <param name="inventoryCategoryTypeMasterId">inventoryCategoryTypeMasterId</param>
        /// <returns>Returns InventoryCategoryTypeViewModel.</returns>
        InventoryCategoryTypeViewModel GetInventoryCategoryType(byte inventoryCategoryTypeMasterId);

        /// <summary>
        /// Update InventoryCategoryType.
        /// </summary>
        /// <param name="inventoryCategoryTypeViewModel">InventoryCategoryTypeViewModel.</param>
        /// <returns>Returns updated InventoryCategoryTypeViewModel</returns>
        InventoryCategoryTypeViewModel UpdateInventoryCategoryType(InventoryCategoryTypeViewModel inventoryCategoryTypeViewModel);

        /// <summary>
        /// Delete InventoryCategoryType.
        /// </summary>
        /// <param name="inventoryCategoryTypeMasterId">inventoryCategoryTypeMasterId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteInventoryCategoryType(string inventoryCategoryTypeMasterId, out string errorMessage);
    }
}

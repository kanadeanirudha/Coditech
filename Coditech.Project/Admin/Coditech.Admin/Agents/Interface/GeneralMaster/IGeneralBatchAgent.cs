using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGeneralBatchAgent
    {
        /// <summary>
        /// Get list of General Batch.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralBatchListViewModel</returns>
        GeneralBatchListViewModel GetBatchList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create General Batch.
        /// </summary>
        /// <param name="generalBatchViewModel"> General Batch View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralBatchViewModel CreateGeneralBatch(GeneralBatchViewModel generalBatchViewModel);

        /// <summary>
        /// Get GeneralBatch by GeneralBatchMasterId.
        /// </summary>
        /// <param name="generalBatchMasterId">generalBatchMasterId</param>
        /// <returns>Returns GeneralBatchViewModel.</returns>
        GeneralBatchViewModel GetGeneralBatch(int generalBatchMasterId);

        /// <summary>
        /// Update General Batch.
        /// </summary>
        /// <param name="generalBatchViewModel">generalBatchViewModel.</param>
        /// <returns>Returns updated GeneralBatchViewModel</returns>
        GeneralBatchViewModel UpdateGeneralBatch(GeneralBatchViewModel generalBatchViewModel);

        /// <summary>
        /// Delete General Batch.
        /// </summary>
        /// <param name="generalBatchMasterId">generalBatchMasterId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteGeneralBatch(string generalBatchMasterId, out string errorMessage);
       

        /// <summary>
        /// Get list of Associated Batch.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralBatchUserListViewModel</returns>
        GeneralBatchUserListViewModel GetGeneralBatchUserList(int generalBatchMasterId, string userType, DataTableViewModel dataTableModel);

        /// <summary>
        /// Update Associate UnAssociate Batchwise User.
        /// </summary>
        /// <param name="generalBatchUserViewModel">generalBatchUserViewModel.</param>
        /// <returns>Returns updated GeneralBatchUserViewModel</returns>
        GeneralBatchUserViewModel AssociateUnAssociateBatchwiseUser(GeneralBatchUserViewModel generalBatchUserViewModel);
    }
}

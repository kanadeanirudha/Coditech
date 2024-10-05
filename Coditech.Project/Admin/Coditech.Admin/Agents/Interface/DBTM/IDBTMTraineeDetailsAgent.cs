using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IDBTMTraineeDetailsAgent
    {
        /// <summary>
        /// Get list of DBTMTraineeDetails.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>DBTMTraineeDetailsListViewModel</returns>
        DBTMTraineeDetailsListViewModel GetDBTMTraineeDetailsList(DataTableViewModel dataTableModelstring, string listType = null);

        /// <summary>
        /// Create DBTMTraineeDetails.
        /// </summary>
        /// <param name="dBTMTraineeDetailsCreateEditViewModel">DBTM Trainee Details View Model.</param>
        /// <returns>Returns created model.</returns>
        DBTMTraineeDetailsCreateEditViewModel CreateDBTMTraineeDetails(DBTMTraineeDetailsCreateEditViewModel dBTMTraineeDetailsCreateEditViewModel);

        /// <summary>
        /// Get DBTMTrainee Details by personId.
        /// </summary>
        /// <param name="personId">personId</param>
        /// <returns>Returns DBTMTraineeDetailsCreateEditViewModel.</returns>
        DBTMTraineeDetailsCreateEditViewModel GetDBTMTraineePersonalDetails(long dBTMTraineeDetailId, long personId);

        /// <summary>
        /// Update DBTM Trainee Details.
        /// </summary>
        /// <param name="dBTMTraineeDetailsCreateEditViewModel">dBTMTraineeDetailsCreateEditViewModel.</param>
        /// <returns>Returns updated DBTMTraineeDetailsCreateEditViewModel</returns>
        DBTMTraineeDetailsCreateEditViewModel UpdateDBTMTraineePersonalDetails(DBTMTraineeDetailsCreateEditViewModel dBTMTraineeDetailsCreateEditViewModel);

        /// <summary>
        /// Get DBTMTraineeDetails by dBTMTraineeDetailId.
        /// </summary>
        /// <param name="dBTMTraineeDetailId">dBTMTraineeDetailId</param>
        /// <returns>Returns DBTMTraineeDetailsResponse.</returns>
        DBTMTraineeDetailsViewModel GetDBTMTraineeOtherDetails(long dBTMTraineeDetailId);

        /// <summary>
        /// Update DBTM Trainee Other Details
        /// </summary>
        /// <param name="DBTMTraineeDetailsModel">DBTMTraineeDetailsModel.</param>
        /// <returns>Returns updated DBTMTraineeDetailsViewModel</returns>
        DBTMTraineeDetailsViewModel UpdateDBTMTraineeOtherDetails(DBTMTraineeDetailsViewModel gymMemberDetailsModel);


        /// <summary>
        /// Delete DBTM Trainee Details.
        /// </summary>
        /// <param name="dBTMTraineeDetailIds">dBTMTraineeDetailIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteDBTMTraineeDetails(string dBTMTraineeDetailIds, out string errorMessage);
    }
}

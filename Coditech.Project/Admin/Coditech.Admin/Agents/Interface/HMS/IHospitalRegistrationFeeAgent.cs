using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IHospitalRegistrationFeeAgent
    {
        /// <summary>
        /// Get list of Registration Fee.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>HospitalRegistrationFeeListViewModel</returns>
        HospitalRegistrationFeeListViewModel GetHospitalRegistrationFeeList(string selectedCentreCode, DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Registration Fee.
        /// </summary>
        /// <param name="HospitalRegistrationFeeCreateEditViewModel">Registration Fee Create View Model.</param>
        /// <returns>Returns created model.</returns>
        HospitalRegistrationFeeCreateEditViewModel CreateRegistrationFee(HospitalRegistrationFeeCreateEditViewModel hospitalRegistrationFeeCreateEditViewModel);

        /// <summary>
        /// Get Registration Fee Personal Details by personId.
        /// </summary>
        /// <param name="hospitalRegistrationFeeId">personId</param>
        /// <returns>Returns HospitalRegistrationFeeCreateEditViewModel.</returns>
        HospitalRegistrationFeeCreateEditViewModel GetRegistrationFee(int hospitalRegistrationFeeId, long personId);

        /// <summary>
        /// Update RegistrationFee Personal Details.
        /// </summary>
        /// <param name="HospitalRegistrationFeeViewModel">HospitalRegistrationFeeCreateEditViewModel.</param>
        /// <returns>Returns updated HospitalRegistrationFeeCreateEditViewModel</returns>
        HospitalRegistrationFeeCreateEditViewModel UpdateRegistrationFee(HospitalRegistrationFeeCreateEditViewModel hospitalRegistrationFeeCreateEditViewModel);

        /// <summary>
        /// Delete Registration Fee
        /// </summary>
        /// <param name="hospitalRegistrationFeeIds">hospitalRegistrationFeeIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteRegistrationFee(string hospitalRegistrationFeeIds, out string errorMessage);
    }
}

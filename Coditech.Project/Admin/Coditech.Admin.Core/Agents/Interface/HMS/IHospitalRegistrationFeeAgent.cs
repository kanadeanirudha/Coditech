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
        /// <param name="HospitalRegistrationFeeViewModel">Registration Fee Create View Model.</param>
        /// <returns>Returns created model.</returns>
        HospitalRegistrationFeeViewModel CreateRegistrationFee(HospitalRegistrationFeeViewModel hospitalRegistrationFeeViewModel);

        /// <summary>
        /// Get Registration Fee by hospitalRegistrationFeeId.
        /// </summary>
        /// <param name="hospitalRegistrationFeeId">hospitalRegistrationFeeId</param>
        /// <returns>Returns HospitalRegistrationFeeViewModel.</returns>
        HospitalRegistrationFeeViewModel GetRegistrationFee(int hospitalRegistrationFeeId);

        /// <summary>
        /// Update HospitalRegistrationFee.
        /// </summary>
        /// <param name="hospitalRegistrationFeeViewModel">hospitalRegistrationFeeViewModel.</param>
        /// <returns>Returns updated HospitalRegistrationFeeViewModel</returns>
        HospitalRegistrationFeeViewModel UpdateRegistrationFee(HospitalRegistrationFeeViewModel hospitalRegistrationFeeViewModel);

        /// <summary>
        /// Delete HospitalRegistrationFee.
        /// </summary>
        /// <param name="hospitalRegistrationFeeIds">hospitalRegistrationFeeIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteRegistrationFee(string hospitalRegistrationFeeIds, out string errorMessage);
    }
}

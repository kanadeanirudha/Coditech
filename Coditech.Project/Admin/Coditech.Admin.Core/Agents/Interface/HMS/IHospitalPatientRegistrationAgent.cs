using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IHospitalPatientRegistrationAgent
    {
        /// <summary>
        /// Get list of Patient Registration.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>HospitalPatientRegistrationListViewModel</returns>
        HospitalPatientRegistrationListViewModel GetHospitalPatientRegistrationList(string selectedCentreCode, DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Patient Registration.
        /// </summary>
        /// <param name="HospitalPatientRegistrationCreateEditViewModel">Patient Registration Create View Model.</param>
        /// <returns>Returns created model.</returns>
        HospitalPatientRegistrationCreateEditViewModel CreatePatientRegistration(HospitalPatientRegistrationCreateEditViewModel hospitalPatientRegistrationCreateEditViewModel);

        /// <summary>
        /// Get Patient Registration Personal Details by personId.
        /// </summary>
        /// <param name="hospitalPatientRegistrationId">personId</param>
        /// <returns>Returns HospitalPatientRegistrationCreateEditViewModel.</returns>
        HospitalPatientRegistrationCreateEditViewModel GetPatientRegistrationPersonalDetails(long hospitalPatientRegistrationId, long personId);

        /// <summary>
        /// Update PatientRegistration Personal Details.
        /// </summary>
        /// <param name="HospitalPatientRegistrationViewModel">HospitalPatientRegistrationCreateEditViewModel.</param>
        /// <returns>Returns updated HospitalPatientRegistrationCreateEditViewModel</returns>
        HospitalPatientRegistrationCreateEditViewModel UpdatePatientRegistrationPersonalDetails(HospitalPatientRegistrationCreateEditViewModel hospitalPatientRegistrationCreateEditViewModel);

        /// <summary>
        /// Delete Patient Registration
        /// </summary>
        /// <param name="hospitalPatientRegistrationIds">hospitalPatientRegistrationIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeletePatientRegistration(string hospitalPatientRegistrationIds, out string errorMessage);
    }
}

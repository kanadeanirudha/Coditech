using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IHospitalDoctorVisitingChargesAgent
    {
        /// <summary>
        /// Get list of General HospitalDoctorVisitingCharges.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>HospitalDoctorVisitingChargesListViewModel</returns>
        HospitalDoctorVisitingChargesListViewModel GetHospitalDoctorVisitingChargesList(string selectedCentreCode, short selectedDepartmentId, DataTableViewModel dataTableModel);

        /// <summary>
        /// Create HospitalDoctorVisitingCharges.
        /// </summary>
        /// <param name="hospitalDoctorVisitingChargesViewModel">General HospitalDoctorVisitingCharges View Model.</param>
        /// <returns>Returns created model.</returns>
        HospitalDoctorVisitingChargesViewModel CreateHospitalDoctorVisitingCharges(HospitalDoctorVisitingChargesViewModel hospitalDoctorVisitingChargesViewModel);

        /// <summary>
        /// Get HospitalDoctorVisitingCharges by hospitalDoctorVisitingChargesId.
        /// </summary>
        /// <param name="hospitalDoctorVisitingChargesId">hospitalDoctorVisitingChargesId</param>
        /// <returns>Returns HospitalDoctorVisitingChargesViewModel.</returns>
        HospitalDoctorVisitingChargesViewModel GetHospitalDoctorVisitingCharges(short hospitalDoctorVisitingChargesId);

        /// <summary>
        /// Update HospitalDoctorVisitingCharges.
        /// </summary>
        /// <param name="hospitalDoctorVisitingChargesViewModel">hospitalDoctorVisitingChargesViewModel.</param>
        /// <returns>Returns updated HospitalDoctorVisitingChargesViewModel</returns>
        HospitalDoctorVisitingChargesViewModel UpdateHospitalDoctorVisitingCharges(HospitalDoctorVisitingChargesViewModel hospitalDoctorVisitingChargesViewModel);

        /// <summary>
        /// Delete HospitalDoctorVisitingCharges.
        /// </summary>
        /// <param name="hospitalDoctorVisitingChargesId">hospitalDoctorVisitingChargesId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteHospitalDoctorVisitingCharges(string hospitalDoctorVisitingChargesId, out string errorMessage);
    }
}

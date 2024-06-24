using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IHospitalPatientAppointmentPurposeAgent
    {
        /// <summary>
        /// Get list of Appointment Purpose.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>HospitalPatientAppointmentPurposeListViewModel</returns>
        HospitalPatientAppointmentPurposeListViewModel GetHospitalPatientAppointmentPurposeList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Appointment Purpose.
        /// </summary>
        /// <param name="HospitalPatientAppointmentPurposeViewModel">Hospital Patient Appointment Purpose View Model.</param>
        /// <returns>Returns created model.</returns>
        HospitalPatientAppointmentPurposeViewModel CreateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeViewModel HospitalPatientAppointmentPurposeViewModel);

        /// <summary>
        /// Get Appointment Purpose by HospitalPatientAppointmentPurposeId.
        /// </summary>
        /// <param name="HospitalPatientAppointmentPurposeId">HospitalPatientAppointmentPurposeId</param>
        /// <returns>Returns HospitalPatientAppointmentPurposeViewModel.</returns>
        HospitalPatientAppointmentPurposeViewModel GetHospitalPatientAppointmentPurpose(short HospitalPatientAppointmentPurposeId);

        /// <summary>
        /// Update Appointment Purpose.
        /// </summary>
        /// <param name="generalCountryViewModel">HospitalPatientAppointmentPurposeViewModel.</param>
        /// <returns>Returns updated HospitalPatientAppointmentPurposeViewModel</returns>
        HospitalPatientAppointmentPurposeViewModel UpdateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeViewModel HospitalPatientAppointmentPurposeViewModel);

        /// <summary>
        /// Delete Appointment Purpose.
        /// </summary>
        /// <param name="HospitalPatientAppointmentPurposeId">HospitalPatientAppointmentPurposeId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteHospitalPatientAppointmentPurpose(string HospitalPatientAppointmentPurposeId, out string errorMessage);

    }
}


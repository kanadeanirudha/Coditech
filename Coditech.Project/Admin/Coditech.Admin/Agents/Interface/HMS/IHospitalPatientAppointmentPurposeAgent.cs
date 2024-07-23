using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IHospitalPatientAppointmentPurposeAgent
    {
        /// <summary>
        /// Get list of HospitalPatientAppointmentPurpose.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>HospitalPatientAppointmentPurposeListViewModel</returns>
        HospitalPatientAppointmentPurposeListViewModel GetHospitalPatientAppointmentPurposeList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Hospital Patient Appointment Purpose.
        /// </summary>
        /// <param name="hospitalPatientAppointmentPurposeViewModel">Hospital Patient Appointment Purpose View Model.</param>
        /// <returns>Returns created model.</returns>
        HospitalPatientAppointmentPurposeViewModel CreateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeViewModel hospitalPatientAppointmentPurposeViewModel);

        /// <summary>
        /// Get HospitalPatientAppointmentPurpose by hospitalPatientAppointmentPurposeId.
        /// </summary>
        /// <param name="hospitalPatientAppointmentPurposeId">hospitalPatientAppointmentPurposeId</param>
        /// <returns>Returns HospitalPatientAppointmentPurposeViewModel.</returns>
        HospitalPatientAppointmentPurposeViewModel GetHospitalPatientAppointmentPurpose(short hospitalPatientAppointmentPurposeId);

        /// <summary>
        /// Update Hospital Patient Appointment Purpose.
        /// </summary>
        /// <param name="hospitalPatientAppointmentPurposeViewModel">HospitalPatientAppointmentPurposeViewModel.</param>
        /// <returns>Returns updated HospitalPatientAppointmentPurposeViewModel</returns>
        HospitalPatientAppointmentPurposeViewModel UpdateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeViewModel hospitalPatientAppointmentPurposeViewModel);

        /// <summary>
        /// Delete Hospital Patient Appointment Purpose.
        /// </summary>
        /// <param name="hospitalPatientAppointmentPurposeId">hospitalPatientAppointmentPurposeId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteHospitalPatientAppointmentPurpose(string hospitalPatientAppointmentPurposeId, out string errorMessage);

    }
}


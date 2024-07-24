using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IHospitalPatientAppointmentAgent
    {
        /// <summary>
        /// Get list of HospitalPatientAppointment.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>HospitalPatientAppointmentListViewModel</returns>
        HospitalPatientAppointmentListViewModel GetHospitalPatientAppointmentList(/*string selectedCentreCode, short selectedDepartmentId,*/ DataTableViewModel dataTableModel);

        /// <summary>
        /// Create HospitalPatientAppointment.
        /// </summary>
        /// <param name="hospitalPatientAppointmentViewModel">Hospital Patient Appointment View Model.</param>
        /// <returns>Returns created model.</returns>
        HospitalPatientAppointmentViewModel CreateHospitalPatientAppointment(HospitalPatientAppointmentViewModel hospitalPatientAppointmentViewModel);

        /// <summary>
        /// Get HospitalPatientAppointment by hospitalPatientAppointmentId.
        /// </summary>
        /// <param name="hospitalPatientAppointmentId">hospitalPatientAppointmentId</param>
        /// <returns>Returns HospitalPatientAppointmentViewModel.</returns>
        HospitalPatientAppointmentViewModel GetHospitalPatientAppointment(long hospitalPatientAppointmentId);

        /// <summary>
        /// Update HospitalPatientAppointment.
        /// </summary>
        /// <param name="hospitalPatientAppointmentViewModel">hospitalPatientAppointmentViewModel.</param>
        /// <returns>Returns updated HospitalPatientAppointmentViewModel</returns>
        HospitalPatientAppointmentViewModel UpdateHospitalPatientAppointment(HospitalPatientAppointmentViewModel hospitalPatientAppointmentViewModel);

        /// <summary>
        /// Delete HospitalPatientAppointment.
        /// </summary>
        /// <param name="hospitalPatientAppointmentId">hospitalPatientAppointmentId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteHospitalPatientAppointment(string hospitalPatientAppointmentId, out string errorMessage);
    }
}

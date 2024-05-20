using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IHospitalDoctorLeaveScheduleAgent
    {
        /// <summary>
        /// Get list of HospitalDoctorLeaveSchedule.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>HospitalDoctorLeaveScheduleListViewModel</returns>
        HospitalDoctorLeaveScheduleListViewModel GetHospitalDoctorLeaveScheduleList(string selectedCentreCode, short selectedDepartmentId, DataTableViewModel dataTableModel);

        /// <summary>
        /// Create HospitalDoctorLeaveSchedule.
        /// </summary>
        /// <param name="hospitalDoctorLeaveScheduleViewModel">Hospital Doctor Leave Schedule View Model.</param>
        /// <returns>Returns created model.</returns>
        HospitalDoctorLeaveScheduleViewModel CreateHospitalDoctorLeaveSchedule(HospitalDoctorLeaveScheduleViewModel hospitalDoctorLeaveScheduleViewModel);

        /// <summary>
        /// Get HospitalDoctorLeaveSchedule by hospitalDoctorLeaveScheduleId.
        /// </summary>
        /// <param name="hospitalDoctorLeaveScheduleId">hospitalDoctorLeaveScheduleId</param>
        /// <returns>Returns HospitalDoctorLeaveScheduleViewModel.</returns>
        HospitalDoctorLeaveScheduleViewModel GetHospitalDoctorLeaveSchedule(long hospitalDoctorLeaveScheduleId);

        /// <summary>
        /// Update HospitalDoctorLeaveSchedule.
        /// </summary>
        /// <param name="hospitalDoctorLeaveScheduleViewModel">hospitalDoctorLeaveScheduleViewModel.</param>
        /// <returns>Returns updated HospitalDoctorLeaveScheduleViewModel</returns>
        HospitalDoctorLeaveScheduleViewModel UpdateHospitalDoctorLeaveSchedule(HospitalDoctorLeaveScheduleViewModel hospitalDoctorLeaveScheduleViewModel);

        /// <summary>
        /// Delete HospitalDoctorAllocatedOPDRoom.
        /// </summary>
        /// <param name="hospitalDoctorLeaveScheduleId">hospitalDoctorLeaveScheduleId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteHospitalDoctorLeaveSchedule(string hospitalDoctorLeaveScheduleId, out string errorMessage);
    }
}

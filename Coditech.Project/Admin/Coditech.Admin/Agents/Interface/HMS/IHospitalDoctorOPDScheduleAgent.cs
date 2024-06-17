using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IHospitalDoctorOPDScheduleAgent
    {
        /// <summary>
        /// Get list of AllocatedOPDRoom.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>HospitalDoctorOPDScheduleListViewModel</returns>
        HospitalDoctorOPDScheduleListViewModel GetHospitalDoctorOPDScheduleList(string selectedCentreCode, short selectedDepartmentId, DataTableViewModel dataTableModel);

        /// <summary>
        /// Get HospitalDoctorOPDSchedule by hospitalDoctorOPDScheduleId.
        /// </summary>
        /// <param name="weekDayEnumId">weekDayEnumId</param>
        /// <returns>Returns HospitalDoctorOPDScheduleViewModel.</returns>
        HospitalDoctorOPDScheduleViewModel GetHospitalDoctorOPDSchedule(int hospitalDoctorId, int weekDayEnumId);

        /// <summary>
        /// Update HospitalDoctorOPDSchedule.
        /// </summary>
        /// <param name="hospitalDoctorOPDScheduleViewModel">hospitalDoctorOPDScheduleViewModel.</param>
        /// <returns>Returns updated HospitalDoctorOPDScheduleViewModel</returns>
        HospitalDoctorOPDScheduleViewModel UpdateHospitalDoctorOPDSchedule(HospitalDoctorOPDScheduleViewModel hospitalDoctorOPDScheduleViewModel);
    }
}

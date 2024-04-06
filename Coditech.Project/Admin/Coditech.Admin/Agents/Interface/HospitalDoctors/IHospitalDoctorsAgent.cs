using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IHospitalDoctorsAgent
    {
        /// <summary>
        /// Get list of Hospital Doctors.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>HospitalDoctorsListViewModel</returns>
        HospitalDoctorsListViewModel GetHospitalDoctorsList(string selectedCentreCode, short selectedDepartmentId, bool isAssociated, DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Hospital Doctors.
        /// </summary>
        /// <param name="hospitalDoctorsViewModel">Hospital DoctorsView Model.</param>
        /// <returns>Returns created model.</returns>
        HospitalDoctorsViewModel CreateHospitalDoctors(HospitalDoctorsViewModel hospitalDoctorsViewModel);


        /// <summary>
        /// Get HospitalDoctors by doctorId.
        /// </summary>
        /// <param name="doctorId">doctorId</param>
        /// <returns>Returns HospitalDoctorsViewModel.</returns>
        HospitalDoctorsViewModel GetHospitalDoctors(int doctorId);

        /// <summary>
        /// Update Hospital Doctors.
        /// </summary>
        /// <param name="hospitalDoctorsViewModel">hospitalDoctorsViewModel.</param>
        /// <returns>Returns updated HospitalDoctorsViewModel</returns>
        HospitalDoctorsViewModel UpdateHospitalDoctors(HospitalDoctorsViewModel hospitalDoctorsViewModel);

        /// <summary>
        /// Delete Hospital Doctors.
        /// </summary>
        /// <param name="hospitalDoctorIds">hospitalDoctorIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteHospitalDoctors(string hospitalDoctorIds, out string errorMessage);
    }
}

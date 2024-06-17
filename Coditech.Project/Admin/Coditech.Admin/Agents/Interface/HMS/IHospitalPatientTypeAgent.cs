using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IHospitalPatientTypeAgent
    {
        /// <summary>
        /// Get list of Hospital Patient Type.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>HospitalPatientTypeListViewModel</returns>
        HospitalPatientTypeListViewModel GetHospitalPatientTypeList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Hospital Patient Type.
        /// </summary>
        /// <param name="hospitalPatientTypeViewModel">Hospital PatientType View Model.</param>
        /// <returns>Returns created model.</returns>
        HospitalPatientTypeViewModel CreateHospitalPatientType(HospitalPatientTypeViewModel hospitalPatientTypeViewModel);

        /// <summary>
        /// Get HospitalPatientTypes by hospitalPatientTypeId.
        /// </summary>
        /// <param name="hospitalPatientTypeId">Hospital PatientType Id</param>
        /// <returns>Returns HospitalPatientTypeViewModel.</returns>
        HospitalPatientTypeViewModel GetHospitalPatientType(byte hospitalPatientTypeId);

        /// <summary>
        /// Update Hospital Patient Type.
        /// </summary>
        /// <param name="hospitalPatientTypeViewModel">hospitalPatientTypeViewModel.</param>
        /// <returns>Returns updated HospitalPatientTypeViewModel</returns>
        HospitalPatientTypeViewModel UpdateHospitalPatientType(HospitalPatientTypeViewModel hospitalPatientTypeViewModel);

        /// <summary>
        /// Delete Hospital PatientType.
        /// </summary>
        /// <param name="hospitalPatientTypeIds">hospitalPatientTypeIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteHospitalPatientType(string hospitalPatientTypeIds, out string errorMessage);
        HospitalPatientTypeListResponse GetHospitalPatientTypeList();
    }
}

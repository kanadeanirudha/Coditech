using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.API.Model.Responses.EmployeeMaster;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IHospitalDoctorsClient : IBaseClient
    {
        /// <summary>
        /// Get list of Hospital Doctors.
        /// </summary>
        /// <returns>HospitalDoctorsListResponse</returns>
        HospitalDoctorsListResponse List(string selectedCentreCode, short selectedDepartmentId, bool isAssociated, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Hospital Doctors.
        /// </summary>
        /// <param name="HospitalDoctorsModel">HospitalDoctorsModel.</param>
        /// <returns>Returns HospitalDoctorsResponse.</returns>
        HospitalDoctorsResponse CreateHospitalDoctors(HospitalDoctorsModel body);

        /// <summary>
        /// Get Hospital Doctors by hospitalDoctorId.
        /// </summary>
        /// <param name="hospitalDoctorId">hospitalDoctorId</param>
        /// <returns>Returns HospitalDoctorsResponse.</returns>
        HospitalDoctorsResponse GetHospitalDoctors(long hospitalDoctorId);

        /// <summary>
        /// Update Hospital Doctors.
        /// </summary>
        /// <param name="HospitalDoctorsModel">HospitalDoctorsModel.</param>
        /// <returns>Returns updated HospitalDoctorsResponse</returns>
        HospitalDoctorsResponse UpdateHospitalDoctors(HospitalDoctorsModel model);

        /// <summary>
        /// Delete Hospital Doctors.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteHospitalDoctors(ParameterModel body);
    }
}

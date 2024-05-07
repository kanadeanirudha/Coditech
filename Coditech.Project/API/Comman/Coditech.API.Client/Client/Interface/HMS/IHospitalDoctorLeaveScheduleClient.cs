using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IHospitalDoctorLeaveScheduleClient : IBaseClient
    {
        /// <summary>
        /// Get list of HospitalDoctorLeaveSchedule.
        /// </summary>
        /// <returns>HospitalDoctorLeaveScheduleListResponse</returns>
        HospitalDoctorLeaveScheduleListResponse List(string selectedCentreCode, short selectedDepartmentId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create HospitalDoctorLeaveSchedule.
        /// </summary>
        /// <param name="HospitalDoctorLeaveScheduleModel">HospitalDoctorLeaveScheduleModel.</param>
        /// <returns>Returns HospitalDoctorLeaveScheduleResponse.</returns>
        HospitalDoctorLeaveScheduleResponse CreateHospitalDoctorLeaveSchedule(HospitalDoctorLeaveScheduleModel body);

        /// <summary>
        /// Get HospitalDoctorLeaveSchedule by hospitalDoctorLeaveScheduleId.
        /// </summary>
        /// <param name="hospitalDoctorLeaveScheduleId">hospitalDoctorLeaveScheduleId</param>
        /// <returns>Returns HospitalDoctorLeaveScheduleResponse.</returns>
        HospitalDoctorLeaveScheduleResponse GetHospitalDoctorLeaveSchedule(long hospitalDoctorLeaveScheduleId);

        /// <summary>
        /// Update HospitalDoctorLeaveSchedule.
        /// </summary>
        /// <param name="HospitalDoctorLeaveScheduleModel">HospitalDoctorLeaveScheduleModel.</param>
        /// <returns>Returns updated HospitalDoctorLeaveScheduleResponse</returns>
        HospitalDoctorLeaveScheduleResponse UpdateHospitalDoctorLeaveSchedule(HospitalDoctorLeaveScheduleModel body);

        /// <summary>
        /// Delete HospitalDoctorLeaveSchedule.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteHospitalDoctorLeaveSchedule(ParameterModel body);
    }
}

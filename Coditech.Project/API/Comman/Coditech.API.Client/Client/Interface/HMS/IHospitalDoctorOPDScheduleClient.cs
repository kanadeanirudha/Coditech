using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IHospitalDoctorOPDScheduleClient : IBaseClient
    {
        /// <summary>
        /// Get list of HospitalDoctorOPDSchedule.
        /// </summary>
        /// <returns>HospitalDoctorOPDScheduleListResponse</returns>
        HospitalDoctorOPDScheduleListResponse List(string selectedCentreCode, short selectedDepartmentId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Get HospitalDoctorOPDSchedule by hospitalDoctorOPDScheduleId.
        /// </summary>
        /// <param name="hospitalDoctorOPDScheduleId">hospitalDoctorOPDScheduleId</param>
        /// <returns>Returns HospitalDoctorOPDScheduleResponse.</returns>
        HospitalDoctorOPDScheduleResponse GetHospitalDoctorOPDSchedule(int hospitalDoctorId, long hospitalDoctorOPDScheduleId);

        /// <summary>
        /// Update HospitalDoctorOPDSchedule.
        /// </summary>
        /// <param name="HospitalDoctorOPDScheduleModel">HospitalDoctorOPDScheduleModel.</param>
        /// <returns>Returns updated HospitalDoctorOPDScheduleResponse</returns>
        HospitalDoctorOPDScheduleResponse UpdateHospitalDoctorOPDSchedule(HospitalDoctorOPDScheduleModel body);
    }
}

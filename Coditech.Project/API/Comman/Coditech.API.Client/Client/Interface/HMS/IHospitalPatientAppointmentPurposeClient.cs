using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IHospitalPatientAppointmentPurposeClient : IBaseClient
    {
        /// <summary>
        /// Get list of HospitalPatientAppointmentPurpose.
        /// </summary>
        /// <returns>HospitalPatientAppointmentPurposeListResponse</returns>
        HospitalPatientAppointmentPurposeListResponse List(/*string selectedCentreCode, short selectedDepartmentId,*/ IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create HospitalPatientAppointmentPurpose.
        /// </summary>
        /// <param name="HospitalPatientAppointmentPurposeModel">HospitalPatientAppointmentPurposeModel.</param>
        /// <returns>Returns HospitalPatientAppointmentPurposeResponse.</returns>
        HospitalPatientAppointmentPurposeResponse CreateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeModel body);

        /// <summary>
        /// Get HospitalPatientAppointmentPurpose by hospitalPatientAppointmentPurposeId.
        /// </summary>
        /// <param name="hospitalPatientAppointmentPurposeId">hospitalPatientAppointmentPurposeId</param>
        /// <returns>Returns HospitalPatientAppointmentPurposeResponse.</returns>
        HospitalPatientAppointmentPurposeResponse GetHospitalPatientAppointmentPurpose(short hospitalPatientAppointmentPurposeId);

        /// <summary>
        /// Update HospitalPatientAppointmentPurpose.
        /// </summary>
        /// <param name="HospitalPatientAppointmentPurposeModel">HospitalPatientAppointmentPurposeModel.</param>
        /// <returns>Returns updated HospitalPatientAppointmentPurposeResponse</returns>
        HospitalPatientAppointmentPurposeResponse UpdateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeModel body);

        /// <summary>
        /// Delete HospitalPatientAppointmentPurpose.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteHospitalPatientAppointmentPurpose(ParameterModel body);
    }
}


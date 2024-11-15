using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IHospitalPatientAppointmentClient : IBaseClient
    {
        /// <summary>
        /// Get list of HospitalPatientAppointment.
        /// </summary>
        /// <returns>HospitalPatientAppointmentListResponse</returns>
        HospitalPatientAppointmentListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create HospitalPatientAppointment.
        /// </summary>
        /// <param name="HospitalPatientAppointmentModel">HospitalPatientAppointmentModel.</param>
        /// <returns>Returns HospitalPatientAppointmentResponse.</returns>
        HospitalPatientAppointmentResponse CreateHospitalPatientAppointment(HospitalPatientAppointmentModel body);

        /// <summary>
        /// Get HospitalPatientAppointment by hospitalPatientAppointmentId.
        /// </summary>
        /// <param name="hospitalPatientAppointmentId">hospitalPatientAppointmentId</param>
        /// <returns>Returns HospitalPatientAppointmentResponse.</returns>
        HospitalPatientAppointmentResponse GetHospitalPatientAppointment(long hospitalPatientAppointmentId);

        /// <summary>
        /// Update HospitalPatientAppointment.
        /// </summary>
        /// <param name="HospitalPatientAppointmentModel">HospitalPatientAppointmentModel.</param>
        /// <returns>Returns updated HospitalPatientAppointmentResponse</returns>
        HospitalPatientAppointmentResponse UpdateHospitalPatientAppointment(HospitalPatientAppointmentModel body);

        /// <summary>
        /// Delete HospitalPatientAppointment.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteHospitalPatientAppointment(ParameterModel body);

        HospitalDoctorsListResponse GetDoctorsByCentreCodeAndSpecialization(string selectedCentreCode, int medicalSpecializationEnumId);
        HospitalPatientTimeSlotListResponse GetTimeSlotByDoctorsAndAppointmentDate(int hospitalDoctorId, DateTime appointmentDate);
    }
}

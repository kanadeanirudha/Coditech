using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IHospitalDoctorVisitingChargesClient : IBaseClient
    {
        /// <summary>
        /// Get list of Hospital Doctor Visiting Charges.
        /// </summary>
        /// <returns>HospitalDoctorVisitingChargesListResponse</returns>
        HospitalDoctorVisitingChargesListResponse List(string selectedCentreCode, short selectedDepartmentId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Hospital Doctor Visiting Charges.
        /// </summary>
        /// <param name="HospitalDoctorVisitingChargesModel">HospitalDoctorVisitingChargesModel.</param>
        /// <returns>Returns HospitalDoctorVisitingChargesResponse.</returns>
        HospitalDoctorVisitingChargesResponse CreateHospitalDoctorVisitingCharges(HospitalDoctorVisitingChargesModel body);

        /// <summary>
        /// Get HospitalDoctorVisitingCharges by hospitalDoctorVisitingChargesId.
        /// </summary>
        /// <param name="hospitalDoctorVisitingChargesId">hospitalDoctorVisitingChargesId</param>
        /// <returns>Returns HospitalDoctorVisitingChargesResponse.</returns>
        HospitalDoctorVisitingChargesResponse GetHospitalDoctorVisitingCharges(short hospitalDoctorVisitingChargesId);

        /// <summary>
        /// Update HospitalDoctorVisitingChargesy.
        /// </summary>
        /// <param name="HospitalDoctorVisitingChargesModel">HospitalDoctorVisitingChargesModel.</param>
        /// <returns>Returns updated HospitalDoctorVisitingChargesResponse</returns>
        HospitalDoctorVisitingChargesResponse UpdateHospitalDoctorVisitingCharges(HospitalDoctorVisitingChargesModel body);

        /// <summary>
        /// Delete HospitalDoctorVisitingCharges.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteHospitalDoctorVisitingCharges(ParameterModel body);
    }
}

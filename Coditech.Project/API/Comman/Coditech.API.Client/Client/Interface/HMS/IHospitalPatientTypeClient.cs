using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IHospitalPatientTypeClient : IBaseClient
    {
        /// <summary>
        /// Get list of Hospital Patient Type.
        /// </summary>
        /// <returns>HospitalPatientTypeListResponse</returns>
        HospitalPatientTypeListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Hospital PatientType.
        /// </summary>
        /// <param name="HospitalPatientTypeModel">HospitalPatientTypeModel.</param>
        /// <returns>Returns HospitalPatientTypeResponse.</returns>
        HospitalPatientTypeResponse CreateHospitalPatientType(HospitalPatientTypeModel body);

        /// <summary>
        /// Get Hospital PatientType by PatientTypeId.
        /// </summary>
        /// <param name="hospitalPatientTypeId">hospitalPatientTypeId</param>
        /// <returns>Returns HospitalPatientTypeResponse.</returns>
        HospitalPatientTypeResponse GetHospitalPatientType(byte hospitalPatientTypeId);

        /// <summary>
        /// Update Hospital PatientType.
        /// </summary>
        /// <param name="HospitalPatientTypeModel">HospitalPatientTypeModel.</param>
        /// <returns>Returns updated HospitalPatientTypeResponse</returns>
        HospitalPatientTypeResponse UpdateHospitalPatientType(HospitalPatientTypeModel model);

        /// <summary>
        /// Delete Hospital PatientType.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteHospitalPatientType(ParameterModel body);
    
    }
}

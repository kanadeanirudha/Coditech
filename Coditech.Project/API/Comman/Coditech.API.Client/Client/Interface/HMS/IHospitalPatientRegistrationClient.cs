using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IHospitalPatientRegistrationClient : IBaseClient
    {
        /// <summary>
        /// Get list of Patient Registration.
        /// </summary>
        /// <returns>HospitalPatientRegistrationListResponse</returns>
        HospitalPatientRegistrationListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Get PatientRegistration by hospitalPatientRegistrationId.
        /// </summary>
        /// <param name="hospitalPatientRegistrationId">hospitalPatientRegistrationId</param>
        /// <returns>Returns HospitalPatientRegistrationResponse.</returns>
        HospitalPatientRegistrationResponse GetPatientRegistrationOtherDetail(long hospitalPatientRegistrationId);

        /// <summary>
        /// Delete Patient Registration.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeletePatientRegistration(ParameterModel body);
    }
}

using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IHospitalPathologyTestClient : IBaseClient
    {
        /// <summary>
        /// Get list of Hospital Pathology Test.
        /// </summary>
        /// <returns>HospitalPathologyTestListResponse</returns>
        HospitalPathologyTestListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create HospitalPathologyTest.
        /// </summary>
        /// <param name="HospitalPathologyTestModel">HospitalPathologyTestModel.</param>
        /// <returns>Returns HospitalPathologyTestResponse.</returns>
        HospitalPathologyTestResponse CreateHospitalPathologyTest(HospitalPathologyTestModel body);

        /// <summary>
        /// Get HospitalPathologyTest by hospitalPathologyTestId.
        /// </summary>
        /// <param name="hospitalPathologyTestId">HospitalPathologyTestId</param>
        /// <returns>Returns HospitalPathologyTestResponse.</returns>
        HospitalPathologyTestResponse GetHospitalPathologyTest(long hospitalPathologyTestId);

        /// <summary>
        /// Update HospitalPathologyTest.
        /// </summary>
        /// <param name="HospitalPathologyTestModel">HospitalPathologyTestModel.</param>
        /// <returns>Returns updated HospitalPathologyTestResponse</returns>
        HospitalPathologyTestResponse UpdateHospitalPathologyTest(HospitalPathologyTestModel body);

        /// <summary>
        /// Delete HospitalPathologyTest.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteHospitalPathologyTest(ParameterModel body);
    }
}

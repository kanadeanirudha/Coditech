using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IHospitalPathologyTestPricesClient : IBaseClient
    {
        /// <summary>
        /// Get list of Hospital Pathology Test Prices.
        /// </summary>
        /// <returns>HospitalPathologyTestPricesListResponse</returns>
        HospitalPathologyTestPricesListResponse List(int hospitalPathologyPriceCategoryEnumId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create HospitalPathologyTestPrices.
        /// </summary>
        /// <param name="HospitalPathologyTestPricesModel">HospitalPathologyTestPricesModel.</param>
        /// <returns>Returns HospitalPathologyTestPricesResponse.</returns>
        HospitalPathologyTestPricesResponse CreateHospitalPathologyTestPrices(HospitalPathologyTestPricesModel body);

        /// <summary>
        /// Get HospitalPathologyTestPrices by hospitalPathologyTestPricesId.
        /// </summary>
        /// <param name="hospitalPathologyTestPricesId">HospitalPathologyTestPricesId</param>
        /// <returns>Returns HospitalPathologyTestPricesResponse.</returns>
        HospitalPathologyTestPricesResponse GetHospitalPathologyTestPrices(long hospitalPathologyTestPricesId);

        /// <summary>
        /// Update HospitalPathologyTestPrices.
        /// </summary>
        /// <param name="HospitalPathologyTestPricesModel">HospitalPathologyTestPricesModel.</param>
        /// <returns>Returns updated HospitalPathologyTestPricesResponse</returns>
        HospitalPathologyTestPricesResponse UpdateHospitalPathologyTestPrices(HospitalPathologyTestPricesModel body);

        /// <summary>
        /// Delete HospitalPathologyTestPrices.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteHospitalPathologyTestPrices(ParameterModel body);
    }
}

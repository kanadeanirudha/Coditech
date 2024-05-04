using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralCityClient : IBaseClient
    {
        /// <summary>
        /// Get list of General City.
        /// </summary>
        /// <returns>GeneralCityListResponse</returns>
        GeneralCityListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create City.
        /// </summary>
        /// <param name="GeneralCityModel">GeneralCityModel.</param>
        /// <returns>Returns GeneralCityResponse.</returns>
        GeneralCityResponse CreateCity(GeneralCityModel body);

        /// <summary>
        /// Get City by cityId.
        /// </summary>
        /// <param name="cityId">cityId</param>
        /// <returns>Returns GeneralCityResponse.</returns>
        GeneralCityResponse GetCity(int cityId);

        /// <summary>
        /// Update City.
        /// </summary>
        /// <param name="GeneralCityModel">GeneralCityModel.</param>
        /// <returns>Returns updated GeneralCityResponse</returns>
        GeneralCityResponse UpdateCity(GeneralCityModel body);

        /// <summary>
        /// Delete City.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteCity(ParameterModel body);
    }
}

using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralCountryClient : IBaseClient
    {
        /// <summary>
        /// Get list of General Country.
        /// </summary>
        /// <returns>GeneralCountryListResponse</returns>
        GeneralCountryListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Country.
        /// </summary>
        /// <param name="GeneralCountryModel">GeneralCountryModel.</param>
        /// <returns>Returns GeneralCountryResponse.</returns>
        GeneralCountryResponse CreateCountry(GeneralCountryModel body);

        /// <summary>
        /// Get Country by generalCountryId.
        /// </summary>
        /// <param name="generalCountryId">generalCountryId</param>
        /// <returns>Returns GeneralCountryResponse.</returns>
        GeneralCountryResponse GetCountry(int generalCountryId);

        /// <summary>
        /// Update Country.
        /// </summary>
        /// <param name="GeneralCountryModel">GeneralCountryModel.</param>
        /// <returns>Returns updated GeneralCountryResponse</returns>
        GeneralCountryResponse UpdateCountry(GeneralCountryModel body);

        /// <summary>
        /// Delete Country.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteCountry(ParameterModel body);
    }
}

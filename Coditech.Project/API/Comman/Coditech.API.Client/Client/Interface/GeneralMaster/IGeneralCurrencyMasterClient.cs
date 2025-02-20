using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralCurrencyMasterClient : IBaseClient
    {
        /// <summary>
        /// Get list of General Country.
        /// </summary>
        /// <returns>GeneralCurrencyMasterResponse</returns>
        GeneralCurrencyMasterListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Country.
        /// </summary>
        /// <param name="GeneralCurrencyMasterModel">GeneralCurrencyMasterModel.</param>
        /// <returns>Returns GeneralCurrencyMasterResponse.</returns>
        GeneralCurrencyMasterResponse CreateCurrency(GeneralCurrencyMasterModel body);

        /// <summary>
        /// Get Country by generalCurrencyId.
        /// </summary>
        /// <param name="generalCurrencyId">generalCountryId</param>
        /// <returns>Returns GeneralCountryResponse.</returns>
        GeneralCurrencyMasterResponse GetCurrency(short generalCurrencyId);

        /// <summary>
        /// Update Country.
        /// </summary>
        /// <param name="GeneralCurrencyMasterModel">GeneralCurrencyMasterModel.</param>
        /// <returns>Returns updated GeneralCurrencyMasterResponse</returns>
        GeneralCurrencyMasterResponse UpdateCurrency(GeneralCurrencyMasterModel body);

        /// <summary>
        /// Delete Country.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteCurrency(ParameterModel body);
    }
}

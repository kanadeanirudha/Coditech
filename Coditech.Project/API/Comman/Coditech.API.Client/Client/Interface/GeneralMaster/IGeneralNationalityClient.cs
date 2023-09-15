using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralNationalityClient : IBaseClient
    {
        /// <summary>
        /// Get list of General Nationality.
        /// </summary>
        /// <returns>GeneralNationalityListResponse</returns>
        GeneralNationalityListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Nationality.
        /// </summary>
        /// <param name="GeneralNationalityModel">GeneralNationalityModel.</param>
        /// <returns>Returns GeneralNationalityResponse.</returns>
        GeneralNationalityResponse CreateNationality(GeneralNationalityModel body);

        /// <summary>
        /// Get Nationality by nationalityId.
        /// </summary>
        /// <param name="nationalityId">nationalityId</param>
        /// <returns>Returns GeneralNationalityResponse.</returns>
        GeneralNationalityResponse GetNationality(int nationalityId);

        /// <summary>
        /// Update Nationality.
        /// </summary>
        /// <param name="GeneralNationalityModel">GeneralNationalityModel.</param>
        /// <returns>Returns updated GeneralNationalityResponse</returns>
        GeneralNationalityResponse UpdateNationality(GeneralNationalityModel body);

        /// <summary>
        /// Delete Nationality.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteNationality(ParameterModel body);
    }
}

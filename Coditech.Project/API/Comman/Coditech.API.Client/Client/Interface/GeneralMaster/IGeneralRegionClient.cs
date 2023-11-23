using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralRegionClient : IBaseClient
    {
        /// <summary>
        /// Get list of General Region.
        /// </summary>
        /// <returns>GeneralRegionListResponse</returns>
        GeneralRegionListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Region.
        /// </summary>
        /// <param name="GeneralRegionModel">GeneralRegionModel.</param>
        /// <returns>Returns GeneralRegionResponse.</returns>
        GeneralRegionResponse CreateRegion(GeneralRegionModel body);

        /// <summary>
        /// Get Region by generalRegionId.
        /// </summary>
        /// <param name="generalRegionId">generalRegionId</param>
        /// <returns>Returns GeneralRegionResponse.</returns>
        GeneralRegionResponse GetRegion(short generalRegionId);

        /// <summary>
        /// Update Region.
        /// </summary>
        /// <param name="GeneralRegionModel">GeneralRegionModel.</param>
        /// <returns>Returns updated GeneralRegionResponse</returns>
        GeneralRegionResponse UpdateRegion(GeneralRegionModel body);

        /// <summary>
        /// Delete Region.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteRegion(ParameterModel body);

        /// <summary>
        /// Get list of General Region.
        /// </summary>
        /// <returns>GeneralRegionListResponse</returns>
        GeneralRegionListResponse GetRegionByCountryWise(string countryCode);
    }
}

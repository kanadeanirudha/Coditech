using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGazetteChaptersClient : IBaseClient
    {
        /// <summary>
        /// Get list of Gazette Chapters.
        /// </summary>
        /// <returns>GazetteChaptersListResponse</returns>
        GazetteChaptersListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create GazetteChapters.
        /// </summary>
        /// <param name="GazetteChaptersModel">GazetteChaptersModel.</param>
        /// <returns>Returns GazetteChaptersResponse.</returns>
        GazetteChaptersResponse CreateGazetteChapters(GazetteChaptersModel body);

        /// <summary>
        /// Get GazetteChapters by gazetteChaptersId.
        /// </summary>
        /// <param name="gazetteChaptersId">gazetteChaptersId</param>
        /// <returns>Returns GazetteChaptersResponse.</returns>
        GazetteChaptersResponse GetGazetteChapters(int gazetteChaptersId);

        /// <summary>
        /// Update GazetteChapters.
        /// </summary>
        /// <param name="GazetteChaptersModel">GazetteChaptersModel.</param>
        /// <returns>Returns updated GazetteChaptersResponse</returns>
        GazetteChaptersResponse UpdateGazetteChapters(GazetteChaptersModel body);

        /// <summary>
        /// Delete GazetteChapters.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteGazetteChapters(ParameterModel body);

        /// <summary>
        /// Get list of Gazette Chapters.
        /// </summary>
        /// <returns>GazetteChaptersListResponse</returns>
        GazetteChaptersListResponse GetGazetteChaptersByDistrictWise(Int16 generalDistrictMasterId);
    }
}

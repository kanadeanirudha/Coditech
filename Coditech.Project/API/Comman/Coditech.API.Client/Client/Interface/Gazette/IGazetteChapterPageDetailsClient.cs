using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGazetteChaptersPageDetailClient : IBaseClient
    {
        /// <summary>
        /// Get list of Gazette Chapters Page Detail.
        /// </summary>
        /// <returns>GazetteChaptersPageDetailListResponse</returns>
        GazetteChaptersPageDetailListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create GazetteChaptersPageDetail.
        /// </summary>
        /// <param name="GazetteChaptersPageDetailModel">GazetteChaptersPageDetailModel.</param>
        /// <returns>Returns GazetteChaptersPageDetailResponse.</returns>
        GazetteChaptersPageDetailResponse CreateGazetteChaptersPageDetail(GazetteChaptersPageDetailModel body);

        /// <summary>
        /// Get GazetteChaptersPageDetail by gazetteChaptersPageDetailId.
        /// </summary>
        /// <param name="gazetteChaptersPageDetailId">gazetteChaptersPageDetailId</param>
        /// <returns>Returns GazetteChaptersPageDetailResponse.</returns>
        GazetteChaptersPageDetailResponse GetGazetteChaptersPageDetail(int gazetteChaptersPageDetailId);

        /// <summary>
        /// Update GazetteChaptersPageDetail.
        /// </summary>
        /// <param name="GazetteChaptersPageDetailModel">GazetteChaptersPageDetailModel.</param>
        /// <returns>Returns updated GazetteChaptersPageDetailResponse</returns>
        GazetteChaptersPageDetailResponse UpdateGazetteChaptersPageDetail(GazetteChaptersPageDetailModel body);

        /// <summary>
        /// Delete GazetteChaptersPageDetail.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteGazetteChaptersPageDetail(ParameterModel body);
    }
}

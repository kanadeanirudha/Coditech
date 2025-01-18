using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGazetteChaptersPageDetailAgent
    {
        /// <summary>
        /// Get list of Gazette Chapters Page Detail.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GazetteChaptersPageDetailListViewModel</returns>
        GazetteChaptersPageDetailListViewModel GetGazetteChaptersPageDetailList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create GazetteChaptersPageDetail.
        /// </summary>
        /// <param name="gazetteChaptersPageDetailViewModel">Gazette Chapters Page Detail View Model.</param>
        /// <returns>Returns created model.</returns>
        GazetteChaptersPageDetailViewModel CreateGazetteChaptersPageDetail(GazetteChaptersPageDetailViewModel gazetteChaptersPageDetailViewModel);

        /// <summary>
        /// Get GazetteChaptersPageDetail by gazetteChaptersPageDetailId.
        /// </summary>
        /// <param name="gazetteChaptersPageDetailId">gazetteChaptersPageDetailId</param>
        /// <returns>Returns GazetteChaptersPageDetailViewModel.</returns>
        GazetteChaptersPageDetailViewModel GetGazetteChaptersPageDetail(int gazetteChaptersPageDetailId);

        /// <summary>
        /// Update GazetteChaptersPageDetail.
        /// </summary>
        /// <param name="gazetteChaptersPageDetailViewModel">gazetteChaptersPageDetailViewModel.</param>
        /// <returns>Returns updated GazetteChaptersPageDetailViewModel</returns>
        GazetteChaptersPageDetailViewModel UpdateGazetteChaptersPageDetail(GazetteChaptersPageDetailViewModel gazetteChaptersPageDetailViewModel);

        /// <summary>
        /// Delete GazetteChaptersPageDetail.
        /// </summary>
        /// <param name="gazetteChaptersPageDetailId">gazetteChaptersPageDetailId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteGazetteChaptersPageDetail(string gazetteChaptersPageDetailId, out string errorMessage);

    }
}

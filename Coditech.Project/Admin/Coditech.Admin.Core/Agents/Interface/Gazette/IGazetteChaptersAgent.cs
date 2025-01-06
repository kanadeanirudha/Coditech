using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGazetteChaptersAgent
    {
        /// <summary>
        /// Get list of Gazette Chapters.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GazetteChaptersListViewModel</returns>
        GazetteChaptersListViewModel GetGazetteChaptersList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create GazetteChapters.
        /// </summary>
        /// <param name="gazetteChaptersViewModel">Gazette Chapters View Model.</param>
        /// <returns>Returns created model.</returns>
        GazetteChaptersViewModel CreateGazetteChapters(GazetteChaptersViewModel gazetteChaptersViewModel);

        /// <summary>
        /// Get GazetteChapters by gazetteChaptersId.
        /// </summary>
        /// <param name="gazetteChaptersId">gazetteChaptersId</param>
        /// <returns>Returns GazetteChaptersViewModel.</returns>
        GazetteChaptersViewModel GetGazetteChapters(int gazetteChaptersId);

        /// <summary>
        /// Update GazetteChapters.
        /// </summary>
        /// <param name="gazetteChaptersViewModel">gazetteChaptersViewModel.</param>
        /// <returns>Returns updated GazetteChaptersViewModel</returns>
        GazetteChaptersViewModel UpdateGazetteChapters(GazetteChaptersViewModel gazetteChaptersViewModel);

        /// <summary>
        /// Delete GazetteChapters.
        /// </summary>
        /// <param name="gazetteChaptersId">gazetteChaptersId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteGazetteChapters(string gazetteChaptersId, out string errorMessage);

    }
}

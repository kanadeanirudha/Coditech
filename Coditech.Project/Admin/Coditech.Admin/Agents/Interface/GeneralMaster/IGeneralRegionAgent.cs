using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGeneralRegionAgent
    {
        /// <summary>
        /// Get list of General Region.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralRegionListViewModel</returns>
        GeneralRegionListViewModel GetRegionList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Region.
        /// </summary>
        /// <param name="generalRegionViewModel">General Region View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralRegionViewModel CreateRegion(GeneralRegionViewModel generalRegionViewModel);

        /// <summary>
        /// Get Region by generalRegionId.
        /// </summary>
        /// <param name="generalRegionId">generalRegionId</param>
        /// <returns>Returns GeneralRegionViewModel.</returns>
        GeneralRegionViewModel GetRegion(short generalRegionId);

        /// <summary>
        /// Update Region.
        /// </summary>
        /// <param name="generalRegionViewModel">generalRegionViewModel.</param>
        /// <returns>Returns updated GeneralRegionViewModel</returns>
        GeneralRegionViewModel UpdateRegion(GeneralRegionViewModel generalRegionViewModel);

        /// <summary>
        /// Delete Region.
        /// </summary>
        /// <param name="generalRegionId">generalRegionId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteRegion(string generalRegionId, out string errorMessage);
    }
}

using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralLeadGenerationAgent
    {
        /// <summary>
        /// Get list of General LeadGeneration.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralLeadGenerationListViewModel</returns>
        GeneralLeadGenerationListViewModel GetLeadGenerationList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create LeadGeneration.
        /// </summary>
        /// <param name="GeneralLeadGenerationViewModel">General LeadGeneration View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralLeadGenerationViewModel CreateLeadGeneration(GeneralLeadGenerationViewModel GeneralLeadGenerationViewModel);

        /// <summary>
        /// Get LeadGeneration by GeneralLeadGenerationId.
        /// </summary>
        /// <param name="GeneralLeadGenerationId">GeneralLeadGenerationId</param>
        /// <returns>Returns GeneralLeadGenerationViewModel.</returns>
        GeneralLeadGenerationViewModel GetLeadGeneration(long GeneralLeadGenerationId);

        /// <summary>
        /// Update LeadGeneration.
        /// </summary>
        /// <param name="GeneralLeadGenerationViewModel">GeneralLeadGenerationViewModel.</param>
        /// <returns>Returns updated GeneralLeadGenerationViewModel</returns>
        GeneralLeadGenerationViewModel UpdateLeadGeneration(GeneralLeadGenerationViewModel GeneralLeadGenerationViewModel);

        /// <summary>
        /// Delete LeadGeneration.
        /// </summary>
        /// <param name="GeneralLeadGenerationId">GeneralLeadGenerationId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteLeadGeneration(string GeneralLeadGenerationId, out string errorMessage);
        GeneralLeadGenerationListResponse GetLeadGenerationList();
    }
}

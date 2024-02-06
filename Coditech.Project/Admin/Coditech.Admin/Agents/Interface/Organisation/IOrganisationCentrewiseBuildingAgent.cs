using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IOrganisationCentrewiseBuildingAgent
    {
        /// <summary>
        /// Get list of Organisation Centre.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>OrganisationCentreListViewModel</returns>
        OrganisationCentrewiseBuildingListViewModel GetOrganisationCentrewiseBuildingList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Organisation Centre.
        /// </summary>
        /// <param name="organisationCentrewiseBuildingViewModel">Organisation Centre View Model.</param>
        /// <returns>Returns created model.</returns>
        OrganisationCentrewiseBuildingViewModel CreateOrganisationCentrewiseBuilding(OrganisationCentrewiseBuildingViewModel organisationCentrewiseBuildingViewModel);

        /// <summary>
        /// Get Organisation Centre by organisationCentrewiseBuildingId.
        /// </summary>
        /// <param name="organisationCentrewiseBuildingId">organisationCentrewiseBuildingId</param>
        /// <returns>Returns OrganisationCentreViewModel.</returns>
        OrganisationCentrewiseBuildingViewModel GetOrganisationCentrewiseBuilding(short organisationCentrewiseBuildingId);

        /// <summary>
        /// Update Organisation Centre.
        /// </summary>
        /// <param name="organisationCentrewiseBuildingViewModel">organisationCentreViewModel.</param>
        /// <returns>Returns updated OrganisationCentreViewModel</returns>
        OrganisationCentrewiseBuildingViewModel UpdateOrganisationCentrewiseBuilding(OrganisationCentrewiseBuildingViewModel organisationCentrewiseBuildingViewModel);

        /// <summary>
        /// Delete Organisation Centre.
        /// </summary>
        /// <param name="organisationCentrewiseBuildingId">organisationCentrewiseBuildingId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteOrganisationCentrewiseBuilding(string organisationCentrewiseBuildingId, out string errorMessage);
                
    }
}

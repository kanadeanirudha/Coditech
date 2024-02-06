using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IOrganisationCentrewiseBuildingAgent
    {
        /// <summary>
        /// Get list of Organisation Centrewise Building Master.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>OrganisationCentrewiseBuildingListViewModel</returns>
        OrganisationCentrewiseBuildingListViewModel GetOrganisationCentrewiseBuildingList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Organisation Centrewise Building Master.
        /// </summary>
        /// <param name="organisationCentrewiseBuildingViewModel">Organisation Centrewise Building View Model.</param>
        /// <returns>Returns created model.</returns>
        OrganisationCentrewiseBuildingViewModel CreateOrganisationCentrewiseBuilding(OrganisationCentrewiseBuildingViewModel organisationCentrewiseBuildingViewModel);

        /// <summary>
        /// Get OrganisationCentrewiseBuildingMaster by organisationCentrewiseBuildingId.
        /// </summary>
        /// <param name="organisationCentrewiseBuildingId">organisationCentrewiseBuildingId</param>
        /// <returns>Returns OrganisationCentrewiseBuildingViewModel.</returns>
        OrganisationCentrewiseBuildingViewModel GetOrganisationCentrewiseBuilding(short organisationCentrewiseBuildingId);

        /// <summary>
        /// Update Organisation Centrewise Building Master.
        /// </summary>
        /// <param name="organisationCentrewiseBuildingViewModel">organisationCentrewiseBuildingViewModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseBuildingViewModel</returns>
        OrganisationCentrewiseBuildingViewModel UpdateOrganisationCentrewiseBuilding(OrganisationCentrewiseBuildingViewModel organisationCentrewiseBuildingViewModel);

        /// <summary>
        /// Delete Organisation Centrewise Building Master.
        /// </summary>
        /// <param name="organisationCentrewiseBuildingId">organisationCentrewiseBuildingId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteOrganisationCentrewiseBuilding(string organisationCentrewiseBuildingId, out string errorMessage);
                
    }
}

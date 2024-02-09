using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IOrganisationCentrewiseBuildingRoomsAgent
    {
        /// <summary>
        /// Get list of Organisation Centrewise Building Rooms.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>OrganisationCentrewiseBuildingRoomsListViewModel</returns>
        OrganisationCentrewiseBuildingRoomsListViewModel GetOrganisationCentrewiseBuildingRoomsList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create OrganisationCentrewiseBuildingRooms.
        /// </summary>
        /// <param name="organisationCentrewiseBuildingRoomsViewModel">Organisation Centrewise Building Rooms View Model.</param>
        /// <returns>Returns created model.</returns>
        OrganisationCentrewiseBuildingRoomsViewModel CreateOrganisationCentrewiseBuildingRooms(OrganisationCentrewiseBuildingRoomsViewModel organisationCentrewiseBuildingRoomsViewModel);

        /// <summary>
        /// Get OrganisationCentrewiseBuildingRooms by organisationCentrewiseBuildingRoomId.
        /// </summary>
        /// <param name="organisationCentrewiseBuildingRoomId">organisationCentrewiseBuildingRoomId</param>
        /// <returns>Returns OrganisationCentrewiseBuildingRoomsViewModel.</returns>
        OrganisationCentrewiseBuildingRoomsViewModel GetOrganisationCentrewiseBuildingRooms(short organisationCentrewiseBuildingRoomId);

        /// <summary>
        /// Update OrganisationCentrewiseBuildingRooms.
        /// </summary>
        /// <param name="organisationCentrewiseBuildingRoomsViewModel">organisationCentrewiseBuildingRoomsViewModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseBuildingRoomsViewModel</returns>
        OrganisationCentrewiseBuildingRoomsViewModel UpdateOrganisationCentrewiseBuildingRooms(OrganisationCentrewiseBuildingRoomsViewModel organisationCentrewiseBuildingRoomsViewModel);

        /// <summary>
        /// Delete OrganisationCentrewiseBuildingRooms.
        /// </summary>
        /// <param name="organisationCentrewiseBuildingRoomId">organisationCentrewiseBuildingRoomId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteOrganisationCentrewiseBuildingRooms(string organisationCentrewiseBuildingRoomId, out string errorMessage);
    }
}

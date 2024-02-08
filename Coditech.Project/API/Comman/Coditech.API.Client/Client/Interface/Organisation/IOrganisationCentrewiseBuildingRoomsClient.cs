using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IOrganisationCentrewiseBuildingRoomsClient : IBaseClient
    {
        /// <summary>
        /// Get list of OrganisationCentrewiseBuildingRooms.
        /// </summary>
        /// <returns>OrganisationCentrewiseBuildingRoomsListResponse</returns>
        OrganisationCentrewiseBuildingRoomsListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create OrganisationCentrewiseBuildingRooms.
        /// </summary>
        /// <param name="OrganisationCentrewiseBuildingRoomsModel">OrganisationCentrewiseBuildingRoomsModel.</param>
        /// <returns>Returns OrganisationCentrewiseBuildingRoomsResponse.</returns>
        OrganisationCentrewiseBuildingRoomsResponse CreateOrganisationCentrewiseBuildingRooms(OrganisationCentrewiseBuildingRoomsModel body);

        /// <summary>
        /// Get Organisation Centrewise Building Rooms by organisationCentrewiseBuildingRoomId.
        /// </summary>
        /// <param name="organisationCentrewiseBuildingRoomId">organisationCentrewiseBuildingRoomId</param>
        /// <returns>Returns OrganisationCentrewiseBuildingRoomsResponse.</returns>
        OrganisationCentrewiseBuildingRoomsResponse GetOrganisationCentrewiseBuildingRooms(short organisationCentrewiseBuildingRoomId);

        /// <summary>
        /// Update OrganisationCentrewiseBuildingRooms.
        /// </summary>
        /// <param name="OrganisationCentrewiseBuildingRoomsModel">OrganisationCentrewiseBuildingRoomsModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseBuildingRoomsResponse</returns>
        OrganisationCentrewiseBuildingRoomsResponse UpdateOrganisationCentrewiseBuildingRooms(OrganisationCentrewiseBuildingRoomsModel body);

        /// <summary>
        /// Delete OrganisationCentrewiseBuildingRooms.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteOrganisationCentrewiseBuildingRooms(ParameterModel body);
    }
}

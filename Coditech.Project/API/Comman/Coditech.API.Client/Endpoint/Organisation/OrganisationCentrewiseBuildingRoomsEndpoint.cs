using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class OrganisationCentrewiseBuildingRoomsEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewiseBuildingRooms/GetOrganisationCentrewiseBuildingRoomsList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateOrganisationCentrewiseBuildingRoomsAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewiseBuildingRooms/CreateOrganisationCentrewiseBuildingRooms";

        public string GetOrganisationCentrewiseBuildingRoomsAsync(short organisationCentrewiseBuildingRoomId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewiseBuildingRooms/GetOrganisationCentrewiseBuildingRooms?organisationCentrewiseBuildingRoomId={organisationCentrewiseBuildingRoomId}";

        public string UpdateOrganisationCentrewiseBuildingRoomsAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewiseBuildingRooms/UpdateOrganisationCentrewiseBuildingRooms";

        public string DeleteOrganisationCentrewiseBuildingRoomsAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewiseBuildingRooms/DeleteOrganisationCentrewiseBuildingRooms";
    }
}

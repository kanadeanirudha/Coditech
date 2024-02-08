using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IOrganisationCentrewiseBuildingRoomsService
    {
        OrganisationCentrewiseBuildingRoomsListModel GetOrganisationCentrewiseBuildingRoomsList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        OrganisationCentrewiseBuildingRoomsModel CreateOrganisationCentrewiseBuildingRooms(OrganisationCentrewiseBuildingRoomsModel model);
        OrganisationCentrewiseBuildingRoomsModel GetOrganisationCentrewiseBuildingRooms(short organisationCentrewiseBuildingRoomId);
        bool UpdateOrganisationCentrewiseBuildingRooms(OrganisationCentrewiseBuildingRoomsModel model);
        bool DeleteOrganisationCentrewiseBuildingRooms(ParameterModel parameterModel);
    }
}

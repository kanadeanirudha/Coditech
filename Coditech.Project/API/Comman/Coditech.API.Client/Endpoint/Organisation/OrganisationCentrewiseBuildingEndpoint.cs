using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class OrganisationCentrewiseBuildingEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewiseBuildingMaster/GetOrganisationCentrewiseBuildingList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateOrganisationAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewiseBuildingMaster/CreateOrganisationCentrewiseBuilding";

        public string GetOrganisationAsync(short organisationCentrewiseBuildingId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewiseBuildingMaster/GetOrganisationCentrewisebuilding?OrganisationCentrewiseBuildingMasterId={organisationCentrewiseBuildingId}";
       
        public string UpdateOrganisationAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewiseBuildingMaster/UpdateOrganisationCentrewiseBuilding";

        public string DeleteOrganisationAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewiseBuildingMaster/DeleteOrganisationCentrewiseBuilding";
        
    }
}

using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class OrganisationCentreEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/GetOrganisationCentreList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateOrganisationAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/CreateOrganisationCentre";

        public string GetOrganisationAsync(short organisationCentreId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/GetOrganisationCentre?organisationCentreMasterId={organisationCentreId}";
       
        public string UpdateOrganisationAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/UpdateOrganisationCentre";

        public string DeleteOrganisationAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/DeleteOrganisationCentre";

        public string GetPrintingFormatAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/GetPrintingFormat";
    }
}

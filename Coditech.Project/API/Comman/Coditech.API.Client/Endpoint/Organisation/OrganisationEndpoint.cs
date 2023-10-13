using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class OrganisationEndpoint : BaseEndpoint
    {
        public string GetOrganisationAsync(short organisationId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationMaster/GetOrganisation?organisationMasterId={organisationId}";
        public string UpdateOrganisationAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationMaster/UpdateOrganisation";
    }
}

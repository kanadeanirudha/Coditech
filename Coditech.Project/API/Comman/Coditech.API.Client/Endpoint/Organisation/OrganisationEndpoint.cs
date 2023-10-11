using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class OrganisationEndpoint : BaseEndpoint
    {
        public string UpdateOrganisationAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationMaster/UpdateOrganisation";
    }
}

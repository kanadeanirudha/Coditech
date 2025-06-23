using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;
namespace Coditech.API.Endpoint
{
    public class OrganisationCentrewiseAccountSetupEndpoint : BaseEndpoint
    {
        public string GetOrganisationCentrewiseAccountSetupAsync(string centreCode) =>
          $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewiseAccountSetup/GetOrganisationCentrewiseAccountSetup?centreCode={centreCode}";

        public string UpdateOrganisationCentrewiseAccountSetupAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewiseAccountSetup/UpdateOrganisationCentrewiseAccountSetup";
    }
}

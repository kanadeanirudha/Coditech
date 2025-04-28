using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class OrganisationCentrewisePolicyEndpoint : BaseEndpoint
    {
        public string ListAsync(string centreCode)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewisePolicy/GetOrganisationCentrewisePolicyList?centreCode={centreCode}";
            return endpoint;
        }
        public string GetCentrewisePolicyDetailsAsync(string centreCode, short generalPolicyRulesId) =>
           $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewisePolicy/GetCentrewisePolicyDetails?centreCode={centreCode}&generalPolicyRulesId={generalPolicyRulesId}";

        public string CentrewisePolicyDetailsAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewisePolicy/CentrewisePolicyDetails";
        public string DeleteCentrewisePolicyAsync() =>
                 $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewisePolicy/DeleteCentrewisePolicy";
    }
}

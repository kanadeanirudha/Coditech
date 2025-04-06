using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.API.Data;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralPolicyEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralPolicyMaster/GetPolicyList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreatePolicyAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralPolicyMaster/CreatePolicy";

        public string GetPolicyAsync(string policyCode) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralPolicyMaster/GetPolicy?policyCode={policyCode}";
       
        public string UpdatePolicyAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralPolicyMaster/UpdatePolicy";

        public string DeletePolicyAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralPolicyMaster/DeletePolicy";

        //General Policy Rules
        public string GetGeneralPolicyRulesListAsync(string policyCode,IEnumerable<string> expand)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralPolicyMaster/GetGeneralPolicyRulesList?policyCode={policyCode}{BuildEndpointQueryString(true,expand)}";
            return endpoint;
        }
        public string CreatePolicyRulesAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralPolicyMaster/CreatePolicyRules";

        public string GetPolicyRulesAsync(short generalPolicyRulesId) =>
           $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralPolicyMaster/GetPolicyRules?generalPolicyRulesId={generalPolicyRulesId}";

        public string UpdatePolicyRulesAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralPolicyMaster/UpdatePolicyRules";

        public string DeletePolicyRulesAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralPolicyMaster/DeletePolicyRules";
    }
}

using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
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

        public string GetPolicyAsync(short generalPolicyMasterId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralPolicyMaster/GetPolicy?generalPolicyMasterId={generalPolicyMasterId}";
       
        public string UpdatePolicyAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralPolicyMaster/UpdatePolicy";

        public string DeletePolicyAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralPolicyMaster/DeletePolicy";
    }
}

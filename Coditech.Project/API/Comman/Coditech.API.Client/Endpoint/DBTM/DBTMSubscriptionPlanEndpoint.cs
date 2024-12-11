using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class DBTMSubscriptionPlanEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMSubscriptionPlan/GetDBTMSubscriptionPlanList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateDBTMSubscriptionPlanAsync() =>
            $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMSubscriptionPlan/CreateDBTMSubscriptionPlan";

        public string GetDBTMSubscriptionPlanAsync(int dBTMSubscriptionPlanId) =>
            $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMSubscriptionPlan/GetDBTMSubscriptionPlan?dBTMSubscriptionPlanId={dBTMSubscriptionPlanId}";

        public string UpdateDBTMSubscriptionPlanAsync() =>
               $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMSubscriptionPlan/UpdateDBTMSubscriptionPlan";

        public string DeleteDBTMSubscriptionPlanAsync() =>
                  $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMSubscriptionPlan/DeleteDBTMSubscriptionPlan";

    }
}

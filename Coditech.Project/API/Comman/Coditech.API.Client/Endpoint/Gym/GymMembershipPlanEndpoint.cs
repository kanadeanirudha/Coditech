using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GymMembershipPlanEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMembershipPlan/GetGymMembershipPlanList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateGymMembershipPlanAsync() =>
           $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMembershipPlan/CreateGymMembershipPlan";

        public string GetGymMembershipPlanAsync(int gymMembershipPlanId) =>
            $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMembershipPlan/GetGymMembershipPlan?gymMembershipPlanId={gymMembershipPlanId}";

        public string UpdateGymMembershipPlanAsync() =>
               $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMembershipPlan/UpdateGymMembershipPlan";

        public string DeleteGymMembershipPlanAsync() =>
                  $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMembershipPlan/DeleteGymMembershipPlan";
    }
}

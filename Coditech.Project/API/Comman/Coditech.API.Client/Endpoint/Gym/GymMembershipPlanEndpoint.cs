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

        public string GetGymMembershipPlanAsync(int gymMemberDetailId) =>
            $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMembershipPlan/GetGymMembershipPlan?gymMemberDetailId={gymMemberDetailId}";

        public string UpdateGymMembershipPlanAsync() =>
               $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMembershipPlan/UpdateGymMembershipPlan";

        public string DeleteGymMembersAsync() =>
                  $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMembershipPlan/DeleteGymMembers";

        public string GymMemberFollowUpListAsync(int gymMemberDetailId,long personId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMembershipPlan/GymMemberFollowUpList?gymMemberDetailId={gymMemberDetailId}&personId={personId}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
    }
}

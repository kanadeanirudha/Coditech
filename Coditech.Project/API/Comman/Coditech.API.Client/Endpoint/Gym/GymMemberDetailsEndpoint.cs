using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GymMemberDetailsEndpoint : BaseEndpoint
    {
        public string ListAsync(string selectedCentreCode, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberDetails/GetGymMemberDetailsList?selectedCentreCode={selectedCentreCode}{BuildEndpointQueryString(true,expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string GetGymMemberOtherDetailsAsync(int gymMemberDetailId) =>
            $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberDetails/GetGymMemberOtherDetails?gymMemberDetailId={gymMemberDetailId}";

        public string UpdateGymMemberOtherDetailsAsync() =>
               $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberDetails/UpdateGymMemberOtherDetails";

        public string DeleteGymMembersAsync() =>
                  $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberDetails/DeleteGymMembers";


        #region Gym Member FollowUp
        public string GymMemberFollowUpListAsync(int gymMemberDetailId, long personId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberDetails/GymMemberFollowUpList?gymMemberDetailId={gymMemberDetailId}&personId={personId}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string GetGymMemberFollowUpAsync(long gymMemberFollowUpId) =>
          $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberDetails/GetGymMemberFollowUp?gymMemberFollowUpId={gymMemberFollowUpId}";

        public string InserUpdateGymMemberFollowUpAsync() =>
              $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberDetails/InserUpdateGymMemberFollowUp";

        public string DeleteGymMemberFollowUpAsync() =>
                  $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberDetails/DeleteGymMemberFollowUp";
        #endregion

        #region Gym Member FollowUp
        public string GetGymMemberMembershipPlanListAsync(int gymMemberDetailId, long personId)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberDetails/GetGymMemberMembershipPlanList?gymMemberDetailId={gymMemberDetailId}&personId={personId}";
            return endpoint;
        }

        public string AssociateGymMemberMembershipPlanAsync() =>
            $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberDetails/AssociateGymMemberMembershipPlan";
        #endregion
    }
}

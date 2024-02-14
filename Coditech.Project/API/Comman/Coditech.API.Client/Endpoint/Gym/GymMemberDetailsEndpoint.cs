using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GymMemberDetailsEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberDetails/GetGymMemberDetailsList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
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

        #region Gym Member Attendance
        public string GeneralPersonAttendanceDetailsListAsync(int gymMemberDetailId, long personId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberDetails/GeneralPersonAttendanceDetailsList?gymMemberDetailId={gymMemberDetailId}&personId={personId}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string GetGeneralPersonAttendanceDetailsAsync(long generalPersonAttendanceDetailId) =>
          $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberDetails/GetGeneralPersonAttendanceDetails?generalPersonAttendanceDetailId={generalPersonAttendanceDetailId}";

        public string InserUpdateGeneralPersonAttendanceDetailsAsync() =>
              $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberDetails/InserUpdateGeneralPersonAttendanceDetails";

        public string DeleteGeneralPersonAttendanceDetailsAsync() =>
                  $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberDetails/DeleteGeneralPersonAttendanceDetails";

        internal string GetGeneralPersonAttendanceDetailsAsync(object generalPersonAttendanceDetailId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

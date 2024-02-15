using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralPersonAttendanceDetailsEndpoint : BaseEndpoint
    {
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
        #endregion
    }
}

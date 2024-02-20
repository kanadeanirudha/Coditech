using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralPersonAttendanceDetailsEndpoint : BaseEndpoint
    {
        #region Gym Member Attendance
        public string GeneralPersonAttendanceDetailsListAsync(long entityId, string userType, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechUserApiRootUri}/GeneralPersonAttendanceDetails/GetPersonAttendanceList?entityId={entityId}&userType={userType}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string GetGeneralPersonAttendanceDetailsAsync(long generalPersonAttendanceDetailId) =>
          $"{CoditechAdminSettings.CoditechUserApiRootUri}/GeneralPersonAttendanceDetails/GetPersonAttendance?generalPersonAttendanceDetailId={generalPersonAttendanceDetailId}";

        public string InserUpdateGeneralPersonAttendanceDetailsAsync() =>
              $"{CoditechAdminSettings.CoditechUserApiRootUri}/GeneralPersonAttendanceDetails/InserUpdateGeneralPersonAttendanceDetails";

        public string DeleteGeneralPersonAttendanceDetailsAsync() =>
                  $"{CoditechAdminSettings.CoditechUserApiRootUri}/GeneralPersonAttendanceDetails/DeletePersonAttendance";
        #endregion
    }
}

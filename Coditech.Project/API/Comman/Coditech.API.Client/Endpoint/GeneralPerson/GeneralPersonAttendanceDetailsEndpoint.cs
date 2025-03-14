using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralPersonAttendanceDetailsEndpoint : BaseEndpoint
    {
        #region  GeneralPerson Attendance
        public string GeneralPersonAttendanceDetailsListAsync(long entityId, string userType, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralPersonAttendanceDetails/GetPersonAttendanceList?entityId={entityId}&userType={userType}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string GetGeneralPersonAttendanceDetailsAsync(long generalPersonAttendanceDetailId) =>
          $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralPersonAttendanceDetails/GetPersonAttendance?generalPersonAttendanceDetailId={generalPersonAttendanceDetailId}";

        public string InserUpdateGeneralPersonAttendanceDetailsAsync() =>
              $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralPersonAttendanceDetails/InserUpdateGeneralPersonAttendanceDetails";

        public string DeleteGeneralPersonAttendanceDetailsAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralPersonAttendanceDetails/DeletePersonAttendance";
        #endregion
    }
}

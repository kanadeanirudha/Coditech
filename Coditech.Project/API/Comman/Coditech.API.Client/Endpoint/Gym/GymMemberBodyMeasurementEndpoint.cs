using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GymMemberBodyMeasurementEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberBodyMeasurement/GetMemberBodyMeasurementList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string GetBodyMeasurementTypeListByMemberIdAsync(int gymMemberDetailId, long personId, short pageSize) =>
           $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberBodyMeasurement/GetBodyMeasurementTypeListByMemberId?gymMemberDetailId={gymMemberDetailId}&personId={personId}&pageSize={pageSize}";

        public string CreateMemberBodyMeasurementAsync() =>
            $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberBodyMeasurement/CreateMemberBodyMeasurement";

        public string GetMemberBodyMeasurementAsync(long GymMemberBodyMeasurementId) =>
            $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberBodyMeasurement/GetMemberBodyMeasurement?GymMemberBodyMeasurementId={GymMemberBodyMeasurementId}";
       
        public string UpdateMemberBodyMeasurementAsync() =>
               $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberBodyMeasurement/UpdateMemberBodyMeasurement";

        public string DeleteMemberBodyMeasurementAsync() =>
                  $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymMemberBodyMeasurement/DeleteMemberBodyMeasurement";
    }
}

using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GymWorkoutPlanEndpoint : BaseEndpoint
    {
        public string ListAsync(string selectedCentreCode, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymWorkoutPlan/GetGymWorkoutPlanList?selectedCentreCode={selectedCentreCode}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateGymWorkoutPlanAsync() =>
           $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymWorkoutPlan/CreateGymWorkoutPlan";
        public string GetGymWorkoutPlanAsync(long gymWorkoutPlanId) =>
            $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymWorkoutPlan/GetGymWorkoutPlan?gymWorkoutPlanId={gymWorkoutPlanId}";

        public string UpdateGymWorkoutPlanAsync() =>
               $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymWorkoutPlan/UpdateGymWorkoutPlan";

        public string DeleteGymWorkoutPlanAsync() =>
                  $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymWorkoutPlan/DeleteGymWorkoutPlan";
    }
}

using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class GymDashboardEndpoint : BaseEndpoint
    {
        public string GetGymDashboardDetailsAsync(int selectedAdminRoleMasterId,long userMasterId) =>
            $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymDashboardController/GetGymDashboardDetails?selectedAdminRoleMasterId={selectedAdminRoleMasterId}&userMasterId={userMasterId}";
    }
}

using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class DashboardEndpoint : BaseEndpoint
    {
        public string GetDashboardDetailsAsync(int selectedAdminRoleMasterId,long userMasterId) =>
            $"{CoditechAdminSettings.CoditechUserApiRootUri}/DashboardController/GetDashboardDetails?selectedAdminRoleMasterId={selectedAdminRoleMasterId}&userMasterId={userMasterId}";
    }
}

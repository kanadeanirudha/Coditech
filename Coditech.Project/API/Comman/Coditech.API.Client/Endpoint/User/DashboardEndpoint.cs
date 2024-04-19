using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class DashboardEndpoint : BaseEndpoint
    {
        public string GetDashboardAsync(int selectedAdminRoleMasterId) =>
            $"{CoditechAdminSettings.CoditechUserApiRootUri}/DashboardController/GetDashboardDetails?selectedAdminRoleMasterId={selectedAdminRoleMasterId}";
    }
}

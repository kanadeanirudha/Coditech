using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class DBTMDashboardEndpoint : BaseEndpoint
    {
        public string GetDBTMDashboardDetailsAsync(int selectedAdminRoleMasterId,long userMasterId) =>
            $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMDashboardController/GetDBTMDashboardDetails?selectedAdminRoleMasterId={selectedAdminRoleMasterId}&userMasterId={userMasterId}";
    }
}

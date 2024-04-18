using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public interface IDashboardService
    {
        DashboardModel GetDashboard(int selectedAdminRoleMasterId);
    }
}

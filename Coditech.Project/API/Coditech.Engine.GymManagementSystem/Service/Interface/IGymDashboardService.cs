using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public interface IGymDashboardService
    {
        GymDashboardModel GetGymDashboardDetails(int selectedAdminRoleMasterId, long userMasterId);
    }
}

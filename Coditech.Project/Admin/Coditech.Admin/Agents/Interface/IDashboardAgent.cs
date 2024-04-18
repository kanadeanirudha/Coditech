using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IDashboardAgent
    {
        /// <summary>
        /// Get Dashboard by selectedAdminRoleMasterId.
        /// </summary>
        /// <param name="selectedAdminRoleMasterId">selectedAdminRoleMasterId</param>
        /// <returns>Returns DashboardViewModel.</returns>
        DashboardViewModel GetDashboardDetails(int selectedAdminRoleMasterId);
    }
}

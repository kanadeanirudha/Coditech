using Coditech.Common.API.Model.Responses;
namespace Coditech.API.Client
{
    public interface IDashboardClient : IBaseClient
    {
        /// <summary>
        /// Get Dashboard by selectedAdminRoleMasterId.
        /// </summary>
        /// <param name="selectedAdminRoleMasterId">selectedAdminRoleMasterId</param>
        /// <returns>Returns DashboardResponse.</returns>
        DashboardResponse GetDashboardDetails(int selectedAdminRoleMasterId);
    }
}

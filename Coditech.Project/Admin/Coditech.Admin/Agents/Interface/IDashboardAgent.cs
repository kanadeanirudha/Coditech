using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IDashboardAgent
    {
        /// <summary>
        /// Get GetDashboardDetails.
        /// </summary>
        /// <returns>Returns DashboardViewModel.</returns>
        DashboardViewModel GetDashboardDetails(short numberOfDaysRecord);
    }
}

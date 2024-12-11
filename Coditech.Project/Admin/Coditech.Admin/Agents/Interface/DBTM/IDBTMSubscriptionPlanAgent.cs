using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IDBTMSubscriptionPlanAgent
    {
        /// <summary>
        /// Get list of DBTM Subscription Plan.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>DBTMSubscriptionPlanListViewModel</returns>
        DBTMSubscriptionPlanListViewModel GetDBTMSubscriptionPlanList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create DBTM Subscription Plan.
        /// </summary>
        /// <param name="dBTMSubscriptionPlanViewModel"> DBTM Subscription Plan View Model.</param>
        /// <returns>Returns created model.</returns>
        DBTMSubscriptionPlanViewModel CreateDBTMSubscriptionPlan(DBTMSubscriptionPlanViewModel dBTMSubscriptionPlanViewModel);

        /// <summary>
        /// Get DBTMSubscriptionPlan by dBTMSubscriptionPlanId.
        /// </summary>
        /// <param name="dBTMSubscriptionPlanId">dBTMSubscriptionPlanId</param>
        /// <returns>Returns DBTMSubscriptionPlanViewModel.</returns>
        DBTMSubscriptionPlanViewModel GetDBTMSubscriptionPlan(int dBTMSubscriptionPlanId);

        /// <summary>
        /// Update DBTM Subscription Plan.
        /// </summary>
        /// <param name="dBTMSubscriptionPlanViewModel">dBTMSubscriptionPlanViewModel.</param>
        /// <returns>Returns updated DBTMSubscriptionPlanViewModel</returns>
        DBTMSubscriptionPlanViewModel UpdateDBTMSubscriptionPlan(DBTMSubscriptionPlanViewModel dBTMSubscriptionPlanViewModel);

        /// <summary>
        /// Delete DBTM Subscription Plan.
        /// </summary>
        /// <param name="dBTMSubscriptionPlanIds">dBTMSubscriptionPlanIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteDBTMSubscriptionPlan(string dBTMSubscriptionPlanIds, out string errorMessage);

    }
}

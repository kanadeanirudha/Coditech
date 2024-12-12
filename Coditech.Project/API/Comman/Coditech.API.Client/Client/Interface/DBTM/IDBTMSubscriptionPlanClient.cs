using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IDBTMSubscriptionPlanClient : IBaseClient
    {
        /// <summary>
        /// Get list of DBTMSubscriptionPlan.
        /// </summary>
        /// <returns>DBTMSubscriptionPlanListResponse</returns>
        DBTMSubscriptionPlanListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create DBTMSubscriptionPlan.
        /// </summary>
        /// <param name="DBTMSubscriptionPlanModel">DBTMSubscriptionPlanModel.</param>
        /// <returns>Returns DBTMSubscriptionPlanResponse.</returns>
        DBTMSubscriptionPlanResponse CreateDBTMSubscriptionPlan(DBTMSubscriptionPlanModel body);

        /// <summary>
        /// Get DBTMSubscriptionPlan by dBTMSubscriptionPlanId.
        /// </summary>
        /// <param name="dBTMSubscriptionPlanId">dBTMSubscriptionPlanId</param>
        /// <returns>Returns DBTMSubscriptionPlanResponse.</returns>
        DBTMSubscriptionPlanResponse GetDBTMSubscriptionPlan(int dBTMSubscriptionPlanId);

        /// <summary>
        /// Update DBTMSubscriptionPlan.
        /// </summary>
        /// <param name="DBTMSubscriptionPlanModel">DBTMSubscriptionPlanModel.</param>
        /// <returns>Returns updated DBTMSubscriptionPlanResponse</returns>
        DBTMSubscriptionPlanResponse UpdateDBTMSubscriptionPlan(DBTMSubscriptionPlanModel body);

        /// <summary>
        /// Delete DBTMSubscriptionPlan.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteDBTMSubscriptionPlan(ParameterModel body);

    }
}

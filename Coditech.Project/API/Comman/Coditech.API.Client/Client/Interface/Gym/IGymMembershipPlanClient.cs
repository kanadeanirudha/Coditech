using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGymMembershipPlanClient : IBaseClient
    {
        /// <summary>
        /// Get list of Gym Member.
        /// </summary>
        /// <returns>GymMembershipPlanListResponse</returns>
        GymMembershipPlanListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Get Gym Member Other shipPlan by gymMemberDetailId.
        /// </summary>
        /// <param name="gymMemberDetailId">gymMemberDetailId</param>
        /// <returns>Returns GymMembershipPlanResponse.</returns>
        GymMembershipPlanResponse GetGymMembershipPlan(int gymMemberDetailId);

        /// <summary>
        /// Update Gym Member Other shipPlan.
        /// </summary>
        /// <param name="GymMembershipPlanModel">GymMembershipPlanModel.</param>
        /// <returns>Returns updated GymMembershipPlanResponse</returns>
        GymMembershipPlanResponse UpdateGymMembershipPlan(GymMembershipPlanModel body);

        /// <summary>
        /// Delete Gym Member.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteGymMembers(ParameterModel body);

        /// <summary>
        /// Get list of Gym Member.
        /// </summary>
        /// <returns>GymMemberFollowUpListResponse</returns>
        GymMemberFollowUpListResponse GymMemberFollowUpList(int GymMemberDetailId, long personId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

    }
}

using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGymMembershipPlanAgent
    {
        /// <summary>
        /// Get list of Gym Member.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GymMembershipPlanListViewModel</returns>
        GymMembershipPlanListViewModel GetGymMembershipPlanList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Membership Plan.
        /// </summary>
        /// <param name="GymMembershipPlanViewModel">GymMembershipPlanViewModel</param>
        /// <returns>Returns created model.</returns>
        GymMembershipPlanViewModel CreateGymMembershipPlan(GymMembershipPlanViewModel gymMembershipPlanViewModel);

        /// <summary>
        /// Get Member ship plan by GymMembershipPlanld.
        /// </summary>
        /// <param name="personId">GymMembershipPlanld</param>
        /// <returns>Returns GymMembershipPlanViewModel.</returns>
        GymMembershipPlanViewModel GetGymMembershipPlan(int gymMembershipPlanId);

        /// <summary>
        /// Update Member Personal shipPlan.
        /// </summary>
        /// <param name="GymMembershipPlanViewModel">GymMembershipPlanViewModel.</param>
        /// <returns>Returns updated GymMembershipPlanViewModel</returns>
        GymMembershipPlanViewModel UpdateGymMembershipPlan(GymMembershipPlanViewModel gymMembershipPlanModel);

        /// <summary>
        /// Delete Gym Members.
        /// </summary>
        /// <param name="gymMembershipPlanlIds">gymMembershipPlanlIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteGymMembershipPlan(string gymMembershipPlanlIds, out string errorMessage);
    }
}

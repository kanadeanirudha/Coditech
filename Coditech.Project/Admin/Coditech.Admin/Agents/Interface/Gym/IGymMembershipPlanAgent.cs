using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.API.Model;

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
        /// Create MembershipPlan.
        /// </summary>
        /// <param name="gymCreateEditMemberViewModel">Gym CreateEdi tMember View Model.</param>
        /// <returns>Returns created model.</returns>
        GymCreateEditMemberViewModel CreateMembershipPlan(GymCreateEditMemberViewModel gymCreateEditMemberViewModel);

        /// <summary>
        /// Get Member Personal shipPlan by personId.
        /// </summary>
        /// <param name="personId">personId</param>
        /// <returns>Returns GymCreateEditMemberViewModel.</returns>
        GymCreateEditMemberViewModel GetMemberPersonalshipPlan(long personId);

        /// <summary>
        /// Update Member Personal shipPlan.
        /// </summary>
        /// <param name="gymCreateEditMemberViewModel">gymCreateEditMemberViewModel.</param>
        /// <returns>Returns updated GymCreateEditMemberViewModel</returns>
        GymCreateEditMemberViewModel UpdateMemberPersonalshipPlan(GymCreateEditMemberViewModel gymCreateEditMemberViewModel);

        /// <summary>
        /// Get MemberOthershipPlan by gymMemberDetailId.
        /// </summary>
        /// <param name="gymMemberDetailId">gymMemberDetailId</param>
        /// <returns>Returns GymMembershipPlanResponse.</returns>
        GymMembershipPlanViewModel GetGymMemberOthershipPlan(int gymMemberDetailId);

        /// <summary>
        /// Update GymMember Other shipPlan
        /// </summary>
        /// <param name="GymMembershipPlanModel">GymMembershipPlanModel.</param>
        /// <returns>Returns updated GymMembershipPlanViewModel</returns>
        GymMembershipPlanViewModel UpdateGymMemberOthershipPlan(GymMembershipPlanViewModel gymMembershipPlanModel);
        /// <summary>
        /// Delete Gym Members.
        /// </summary>
        /// <param name="gymMemberDetailIds">gymMemberDetailIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteGymMembers(string gymMemberDetailIds, out string errorMessage);

        /// <summary>
        /// Get list of Gym Member.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GymMemberFollowUpListViewModel</returns>
        GymMemberFollowUpListViewModel GymMemberFollowUpList(int gymMemberDetailId, long personId, DataTableViewModel dataTableModel);
    }
}

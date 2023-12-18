using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGymMemberDetailsAgent
    {
        /// <summary>
        /// Get list of Gym Member.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GymMemberDetailsListViewModel</returns>
        GymMemberDetailsListViewModel GetGymMemberDetailsList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create MemberDetails.
        /// </summary>
        /// <param name="gymCreateEditMemberViewModel">Gym CreateEdi tMember View Model.</param>
        /// <returns>Returns created model.</returns>
        GymCreateEditMemberViewModel CreateMemberDetails(GymCreateEditMemberViewModel gymCreateEditMemberViewModel);

        /// <summary>
        /// Get Member Details by personId.
        /// </summary>
        /// <param name="personId">personId</param>
        /// <returns>Returns GymCreateEditMemberViewModel.</returns>
        GymCreateEditMemberViewModel GetMemberDetails(long personId);

        /// <summary>
        /// Update MemberDetails.
        /// </summary>
        /// <param name="gymCreateEditMemberViewModel">gymCreateEditMemberViewModel.</param>
        /// <returns>Returns updated GymCreateEditMemberViewModel</returns>
        GymCreateEditMemberViewModel UpdateMemberDetails(GymCreateEditMemberViewModel gymCreateEditMemberViewModel);

        /// <summary>
        /// Delete Members.
        /// </summary>
        /// <param name="gymMemberDetailId">gymMemberDetailId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteMembers(string gymMemberDetailId, out string errorMessage);
    }
}

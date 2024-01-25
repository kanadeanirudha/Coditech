using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.API.Model;

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
        /// Get Member Personal Details by personId.
        /// </summary>
        /// <param name="personId">personId</param>
        /// <returns>Returns GymCreateEditMemberViewModel.</returns>
        GymCreateEditMemberViewModel GetMemberPersonalDetails(long personId);

        /// <summary>
        /// Get Member Address Details by personId.
        /// </summary>
        /// <param name="personId">personId</param>
        /// <returns>Returns GeneralPersonAddressListViewModel.</returns>
        GeneralPersonAddressListViewModel GetMemberAddressDetails(long personId);

        /// <summary>
        /// Update Member Personal Details.
        /// </summary>
        /// <param name="gymCreateEditMemberViewModel">gymCreateEditMemberViewModel.</param>
        /// <returns>Returns updated GymCreateEditMemberViewModel</returns>
        GymCreateEditMemberViewModel UpdateMemberPersonalDetails(GymCreateEditMemberViewModel gymCreateEditMemberViewModel);

        /// <summary>
        /// Update Member Address Details.
        /// </summary>
        /// <param name="gymCreateEditMemberViewModel">gymCreateEditMemberViewModel.</param>
        /// <returns>Returns updated GymCreateEditMemberViewModel</returns>
        GymCreateEditMemberViewModel UpdateMemberAddressDetails(GymCreateEditMemberViewModel gymCreateEditMemberViewModel);

        /// <summary>
        /// Get MemberOtherDetails by gymMemberDetailId.
        /// </summary>
        /// <param name="gymMemberDetailId">gymMemberDetailId</param>
        /// <returns>Returns GymMemberDetailsResponse.</returns>
        GymMemberDetailsViewModel GetGymMemberOtherDetails(int gymMemberDetailId);

        /// <summary>
        /// Update GymMember Other Details
        /// </summary>
        /// <param name="GymMemberDetailsModel">GymMemberDetailsModel.</param>
        /// <returns>Returns updated GymMemberDetailsViewModel</returns>
        GymMemberDetailsViewModel UpdateGymMemberOtherDetails(GymMemberDetailsViewModel gymMemberDetailsModel);
        /// <summary>
        /// Delete Gym Members.
        /// </summary>
        /// <param name="gymMemberDetailIds">gymMemberDetailIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteGymMembers(string gymMemberDetailIds, out string errorMessage);
    }
}

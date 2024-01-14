using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGymMemberDetailsClient : IBaseClient
    {
        /// <summary>
        /// Get list of Gym Member.
        /// </summary>
        /// <returns>GymMemberDetailsListResponse</returns>
        GymMemberDetailsListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Get Gym Member Other Details by gymMemberDetailId.
        /// </summary>
        /// <param name="gymMemberDetailId">gymMemberDetailId</param>
        /// <returns>Returns GymMemberDetailsResponse.</returns>
        GymMemberDetailsResponse GetGymMemberOtherDetails(int gymMemberDetailId);

        /// <summary>
        /// Update Gym Member Other Details.
        /// </summary>
        /// <param name="GymMemberDetailsModel">GymMemberDetailsModel.</param>
        /// <returns>Returns updated GymMemberDetailsResponse</returns>
        GymMemberDetailsResponse UpdateGymMemberOtherDetails(GymMemberDetailsModel body);

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

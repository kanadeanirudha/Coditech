using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGymMemberBodyMeasurementClient : IBaseClient
    {
        /// <summary>
        /// Get Body Measurement Type List By MemberId
        /// </summary>
        /// <param name="gymMemberDetailId">gymMemberDetailId</param>
        /// <param name="personId">personId</param>
        /// <param name="listCount">listCount</param>
        /// <returns>GymMemberBodyMeasurementListViewModel</returns>
        GymMemberBodyMeasurementListResponse GetBodyMeasurementTypeListByMemberId(int gymMemberDetailId, long personId, short pageSize);

        /// <summary>
        /// Get list of  MemberBodyMeasurement.
        /// </summary>
        /// <returns>GymMemberBodyMeasurementListResponse</returns>
        GymMemberBodyMeasurementListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create MemberBodyMeasurement.
        /// </summary>
        /// <param name="GymMemberBodyMeasurementModel">GymMemberBodyMeasurementModel.</param>
        /// <returns>Returns GymMemberBodyMeasurementResponse.</returns>
        GymMemberBodyMeasurementResponse CreateMemberBodyMeasurement(GymMemberBodyMeasurementModel body);

        /// <summary>
        /// Get MemberBodyMeasurement by GymMemberBodyMeasurementId.
        /// </summary>
        /// <param name="GymMemberBodyMeasurementId">GymMemberBodyMeasurementId</param>
        /// <returns>Returns GymMemberBodyMeasurementResponse.</returns>
        GymMemberBodyMeasurementResponse GetMemberBodyMeasurement(long GymMemberBodyMeasurementId);

        /// <summary>
        /// Update MemberBodyMeasurement.
        /// </summary>
        /// <param name="GymMemberBodyMeasurementModel">GymMemberBodyMeasurementModel.</param>
        /// <returns>Returns updated GymMemberBodyMeasurementResponse</returns>
        GymMemberBodyMeasurementResponse UpdateMemberBodyMeasurement(GymMemberBodyMeasurementModel body);

        /// <summary>
        /// Delete MemberBodyMeasurement.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteMemberBodyMeasurement(ParameterModel body);
    }
}

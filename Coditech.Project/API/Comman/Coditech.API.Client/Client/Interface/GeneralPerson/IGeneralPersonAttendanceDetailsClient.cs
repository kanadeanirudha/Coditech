using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralPersonAttendanceDetailsClient : IBaseClient
    {

        /// <summary>
        /// Get list of Gym Member.
        /// </summary>
        /// <returns>GeneralPersonAttendanceDetailsListResponse</returns>
        GeneralPersonAttendanceDetailsListResponse GeneralPersonAttendanceDetailsList(long personId, string userType, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Get GeneralPersonAttendanceDetails  by generalPersonAttendanceDetailId.
        /// </summary>
        /// <param name="generalPersonAttendanceDetailId">generalPersonAttendanceDetailId</param>
        /// <returns>Returns GeneralPersonAttendanceDetailsResponse.</returns>
        GeneralPersonAttendanceDetailsResponse GetGeneralPersonAttendanceDetails(long generalPersonAttendanceDetailsId);

        /// <summary>
        /// Inser Update General Person Attendance Details.
        /// </summary>
        /// <param name="GeneralPersonAttendanceDetailsModel">GeneralPersonAttendanceDetailsModel.</param>
        /// <returns>Returns updated GeneralPersonAttendanceDetailsResponse</returns>
        GeneralPersonAttendanceDetailsResponse InserUpdateGeneralPersonAttendanceDetails(GeneralPersonAttendanceDetailsModel body);

        /// <summary>
        /// Delete General Person Attendance Details.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteGeneralPersonAttendanceDetails(ParameterModel body);
    }
}

using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IHospitalPathologyTestGroupClient : IBaseClient
    {
        /// <summary>
        /// Get list of Hospital Pathology Test Group.
        /// </summary>
        /// <returns>HospitalPathologyTestGroupListResponse</returns>
        HospitalPathologyTestGroupListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create HospitalPathologyTestGroup.
        /// </summary>
        /// <param name="HospitalPathologyTestGroupModel">HospitalPathologyTestGroupModel.</param>
        /// <returns>Returns HospitalPathologyTestGroupResponse.</returns>
        HospitalPathologyTestGroupResponse CreateHospitalPathologyTestGroup(HospitalPathologyTestGroupModel body);

        /// <summary>
        /// GetHospitalPathologyTestGroup by hospitalPathologyTestGroupId.
        /// </summary>
        /// <param name="hospitalPathologyTestGroupId">HospitalPathologyTestGroupId</param>
        /// <returns>Returns HospitalPathologyTestGroupResponse.</returns>
        HospitalPathologyTestGroupResponse GetHospitalPathologyTestGroup(int hospitalPathologyTestGroupId);

        /// <summary>
        /// Update HospitalPathologyTestGroup.
        /// </summary>
        /// <param name="HospitalPathologyTestGroupModel">HospitalPathologyTestGroupModel.</param>
        /// <returns>Returns updated HospitalPathologyTestGroupResponse</returns>
        HospitalPathologyTestGroupResponse UpdateHospitalPathologyTestGroup(HospitalPathologyTestGroupModel body);

        /// <summary>
        /// Delete HospitalPathologyTestGroup.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteHospitalPathologyTestGroup(ParameterModel body);
    }
}

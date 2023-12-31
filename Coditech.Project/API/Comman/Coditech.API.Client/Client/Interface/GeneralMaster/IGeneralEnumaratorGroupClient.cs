using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralEnumaratorGroupClient : IBaseClient
    {
        /// <summary>
        /// Get list of General EnumaratorGroup.
        /// </summary>
        /// <returns>GeneralEnumaratorGroupListResponse</returns>
        GeneralEnumaratorGroupListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create EnumaratorGroup.
        /// </summary>
        /// <param name="GeneralEnumaratorGroupModel">GeneralEnumaratorGroupModel.</param>
        /// <returns>Returns GeneralEnumaratorGroupResponse.</returns>
        GeneralEnumaratorGroupResponse CreateEnumaratorGroup(GeneralEnumaratorGroupModel body);

        /// <summary>
        /// Get EnumaratorGroup by generalEnumaratorGroupId.
        /// </summary>
        /// <param name="generalEnumaratorGroupId">generalEnumaratorGroupId</param>
        /// <returns>Returns GeneralEnumaratorGroupResponse.</returns>
        GeneralEnumaratorGroupResponse GetEnumaratorGroup(int generalEnumaratorGroupId);

        /// <summary>
        /// Update EnumaratorGroup.
        /// </summary>
        /// <param name="GeneralEnumaratorGroupModel">GeneralEnumaratorGroupModel.</param>
        /// <returns>Returns updated GeneralEnumaratorGroupResponse</returns>
        GeneralEnumaratorGroupResponse UpdateEnumaratorGroup(GeneralEnumaratorGroupModel body);

        /// <summary>
        /// Delete EnumaratorGroup.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteEnumaratorGroup(ParameterModel body);
    }
}

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

        /// <summary>
        /// Get Enumarator by generalEnumaratorId.
        /// </summary>
        /// <param name="generalEnumaratorId">generalEnumaratorId</param>
        /// <returns>Returns GeneralEnumaratorResponse.</returns>
        GeneralEnumaratorResponse GetEnumarator(int generalEnumaratorId);

        /// <summary>
        /// Update Enumarator.
        /// </summary>
        /// <param name="GeneralEnumaratorModel">GeneralEnumaratorModel.</param>
        /// <returns>Returns updated GeneralEnumaratorResponse</returns>
        GeneralEnumaratorResponse InsertUpdateEnumarator(GeneralEnumaratorModel body);

        /// <summary>
        /// Delete Enumarator.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteEnumarator(ParameterModel body);
    }
}

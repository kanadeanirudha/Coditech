using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralPolicyClient : IBaseClient
    {
        /// <summary>
        /// Get list of General Policy.
        /// </summary>
        /// <returns>GeneralPolicyListResponse</returns>
        GeneralPolicyListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Policy.
        /// </summary>
        /// <param name="GeneralPolicyModel">GeneralPolicyModel.</param>
        /// <returns>Returns GeneralPolicyResponse.</returns>
        GeneralPolicyResponse CreatePolicy(GeneralPolicyModel body);

        /// <summary>
        /// Get Policy by generalPolicyId.
        /// </summary>
        /// <param name="generalPolicyMasterId">generalPolicyMasterId</param>
        /// <returns>Returns GeneralPolicyResponse.</returns>
        GeneralPolicyResponse GetPolicy(short generalPolicyMasterId);

        /// <summary>
        /// Update Policy.
        /// </summary>
        /// <param name="GeneralPolicyModel">GeneralPolicyModel.</param>
        /// <returns>Returns updated GeneralPolicyResponse</returns>
        GeneralPolicyResponse UpdatePolicy(GeneralPolicyModel body);

        /// <summary>
        /// Delete Policy.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeletePolicy(ParameterModel body);
    }
}

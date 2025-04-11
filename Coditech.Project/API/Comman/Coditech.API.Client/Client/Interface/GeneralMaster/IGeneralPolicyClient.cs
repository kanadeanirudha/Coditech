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
        /// <param name="policyCode">policyCode</param>
        /// <returns>Returns GeneralPolicyResponse.</returns>
        GeneralPolicyResponse GetPolicy(string policyCode);

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

        /// <summary>
        /// Get list of General Policy Rules.
        /// </summary>
        /// <returns>GeneralPolicyRulesListResponse</returns>
        GeneralPolicyRulesListResponse GetGeneralPolicyRulesList(string policyCode, IEnumerable<string> expand);

        /// <summary>
        /// Create Policy Rules.
        /// </summary>
        /// <param name="GeneralPolicyRulesModel">GeneralPolicyRulesModel.</param>
        /// <returns>Returns GeneralPolicyRulesResponse.</returns>
        GeneralPolicyRulesResponse CreatePolicyRules(GeneralPolicyRulesModel body);

        /// <summary>
        /// Get Policy by generalPolicyRulesId.
        /// </summary>
        /// <param name="generalPolicyRulesId">generalPolicyRulesId</param>
        /// <returns>Returns GeneralPolicyRulesResponse.</returns>
        GeneralPolicyRulesResponse GetPolicyRules(short generalPolicyRulesId, string policyApplicableStatus);

        /// <summary>
        /// Update PolicyRules.
        /// </summary>
        /// <param name="GeneralPolicyRulesModel">GeneralPolicyRulesModel.</param>
        /// <returns>Returns updated GeneralPolicyRulesResponse</returns>
        GeneralPolicyRulesResponse UpdatePolicyRules(GeneralPolicyRulesModel body);

        /// <summary>
        /// Delete PolicyRules.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeletePolicyRules(ParameterModel body);

        /// <summary>
        /// Get Policy by generalPolicyDetailsId.
        /// </summary>
        /// <param name="generalPolicyDetailsId">generalPolicyDetailsId</param>
        /// <returns>Returns GeneralPolicyDetailsResponse.</returns>
        GeneralPolicyDetailsResponse GetPolicyDetails(short generalPolicyDetailsId);

        /// <summary>
        /// Update PolicyDetails.
        /// </summary>
        /// <param name="GeneralPolicyDetailsModel">GeneralPolicyDetailsModel.</param>
        /// <returns>Returns updated GeneralPolicyDetailsResponse</returns>
        GeneralPolicyDetailsResponse UpdatePolicyDetails(GeneralPolicyDetailsModel body);
    }
}

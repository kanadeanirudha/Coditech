using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IOrganisationCentrewisePolicyClient : IBaseClient
    {
        /// <summary>
        /// Get list of OrganisationCentrewisePolicy.
        /// </summary>
        /// <returns>GeneralPolicyDetailsListResponse</returns>
        GeneralPolicyDetailsListResponse List(string centreCode);

        /// <summary>
        /// Get Policy by generalPolicyRulesId.
        /// </summary>
        /// <param name="generalPolicyRulesId">generalPolicyRulesId</param>
        /// <returns>Returns GeneralPolicyDetailsResponse.</returns>
        GeneralPolicyDetailsResponse GetCentrewisePolicyDetails(string policyApplicableStatus, short generalPolicyRulesId);

        /// <summary>
        /// Update  Centrewise Policy.
        /// </summary>
        /// <param name="GeneralPolicyDetailsModel">GeneralPolicyDetailsModel.</param>
        /// <returns>Returns updated GeneralPolicyDetailsResponse</returns>
        GeneralPolicyDetailsResponse CentrewisePolicyDetails(GeneralPolicyDetailsModel body);

        /// <summary>
        /// Delete Centrewise Policy.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteCentrewisePolicy(ParameterModel body);
    }
}

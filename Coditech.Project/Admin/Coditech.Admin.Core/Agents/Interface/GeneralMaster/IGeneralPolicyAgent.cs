using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralPolicyAgent
    {
        /// <summary>
        /// Get list of General Policy.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralPolicyListViewModel</returns>
        GeneralPolicyListViewModel GetPolicyList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Policy.
        /// </summary>
        /// <param name="generalPolicyViewModel">General Policy View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralPolicyViewModel CreatePolicy(GeneralPolicyViewModel generalPolicyViewModel);

        /// <summary>
        /// Get Policy by generalPolicyMasterId.
        /// </summary>
        /// <param name="policyCode">policyCode</param>
        /// <returns>Returns GeneralPolicyViewModel.</returns>
        GeneralPolicyViewModel GetPolicy(string policyCode);

        /// <summary>
        /// Update Policy.
        /// </summary>
        /// <param name="generalPolicyViewModel">generalPolicyViewModel.</param>
        /// <returns>Returns updated GeneralPolicyViewModel</returns>
        GeneralPolicyViewModel UpdatePolicy(GeneralPolicyViewModel generalPolicyViewModel);

        /// <summary>
        /// Delete Policy.
        /// </summary>
        /// <param name="generalPolicyMasterId">generalPolicyMasterId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeletePolicy(string policyCode, out string errorMessage);

        /// <summary>
        /// Get list of Policy Rules.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralPolicyRulesListViewModel</returns>
        GeneralPolicyRulesListViewModel GetGeneralPolicyRulesList(string policyCode,DataTableViewModel dataTableViewModel);

        /// <summary>
        /// Create Policy Rules.
        /// </summary>
        /// <param name="generalPolicyViewModel">General Policy View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralPolicyRulesViewModel CreatePolicyRules(GeneralPolicyRulesViewModel generalPolicyViewModel);

        /// <summary>
        /// Get Policy by generalPolicyRulesId.
        /// </summary>
        /// <param name="generalPolicyRulesId">generalPolicyRulesId</param>
        /// <returns>Returns GeneralPolicyRulesViewModel.</returns>
        GeneralPolicyRulesViewModel GetPolicyRules(short generalPolicyRulesId);

        /// <summary>
        /// Update PolicyRules.
        /// </summary>
        /// <param name="generalPolicyRulesViewModel">generalPolicyRulesViewModel.</param>
        /// <returns>Returns updated GeneralPolicyRulesViewModel</returns>
        GeneralPolicyRulesViewModel UpdatePolicyRules(GeneralPolicyRulesViewModel generalPolicyRulesViewModel);

        /// <summary>
        /// Delete PolicyRules.
        /// </summary>
        /// <param name="generalPolicyRulesId">generalPolicyRulesId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeletePolicyRules(string generalPolicyRulesId, out string errorMessage);
    }
}

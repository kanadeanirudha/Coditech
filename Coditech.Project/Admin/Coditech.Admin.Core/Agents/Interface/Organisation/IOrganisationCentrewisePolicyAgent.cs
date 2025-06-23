using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IOrganisationCentrewisePolicyAgent
    {
        /// <summary>
        /// Get list of Organisation Centrewise Policy.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>OrganisationCentrewisePolicyListViewModel</returns>
        GeneralPolicyDetailsListViewModel GetOrganisationCentrewisePolicyList(string centreCode);

        /// <summary>
        /// Get Organisation Centrewise Policy by generalPolicyRulesId.
        /// </summary>
        /// <param name="generalPolicyRulesId">generalPolicyRulesId</param>
        /// <returns>Returns GeneralPolicyDetailsViewModel.</returns>
        GeneralPolicyDetailsViewModel GetCentrewisePolicyDetails(string centreCode, short generalPolicyRulesId);

        /// <summary>
        /// Update Organisation Centrewise Policy.
        /// </summary>
        /// <param name="generalPolicyDetailsViewModel">generalPolicyDetailsViewModel.</param>
        /// <returns>Returns updated generalPolicyDetailsViewModel</returns>
        GeneralPolicyDetailsViewModel CentrewisePolicyDetails(GeneralPolicyDetailsViewModel generalPolicyDetailsViewModel);

        /// <summary>
        /// Delete CentrewisePolicy.
        /// </summary>
        /// <param name="generalPolicyRulesId">generalPolicyRulesId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteCentrewisePolicy(string generalPolicyRulesId, out string errorMessage);
    }
}

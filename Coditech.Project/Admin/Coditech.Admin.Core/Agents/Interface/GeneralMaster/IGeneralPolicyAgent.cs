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
        /// <param name="generalPolicyMasterId">generalPolicyMasterId</param>
        /// <returns>Returns GeneralPolicyViewModel.</returns>
        GeneralPolicyViewModel GetPolicy(short generalPolicyMasterId);

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
        bool DeletePolicy(string generalPolicyMasterId, out string errorMessage);
    }
}

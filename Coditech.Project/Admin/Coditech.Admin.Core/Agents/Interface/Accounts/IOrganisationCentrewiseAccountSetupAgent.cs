using Coditech.Admin.ViewModel;
using Coditech.API.Data;
namespace Coditech.Admin.Agents
{
    public interface IOrganisationCentrewiseAccountSetupAgent
    {
        /// <summary>
        /// Get Organisation Centrewise Account Setup by organisationCentrewiseAccountSetupId.
        /// </summary>
        /// <param name="organisationCentrewiseAccountSetupId">organisationCentrewiseAccountSetupId</param>
        /// <param name="centreCode">centreCode</param>
        /// <returns>Returns OrganisationCentrewiseAccountSetupViewModel.</returns>
        OrganisationCentrewiseAccountSetupViewModel GetOrganisationCentrewiseAccountSetup(string centreCode);

        /// <summary>
        /// Update Organisation Centrewise Account Setup ViewModel.
        /// </summary>
        /// <param name="organisationCentrewiseAccountSetupViewModel">organisationCentrewiseAccountSetupViewModel.</param>
        /// <returns>Returns updated organisationCentrewiseAccountSetupViewModel</returns>
        OrganisationCentrewiseAccountSetupViewModel UpdateOrganisationCentrewiseAccountSetup(OrganisationCentrewiseAccountSetupViewModel organisationCentrewiseAccountSetupViewModel);
    }
}

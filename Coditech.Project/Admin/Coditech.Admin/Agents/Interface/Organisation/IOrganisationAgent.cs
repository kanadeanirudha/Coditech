using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IOrganisationAgent
    {
        /// <summary>
        /// Get Organisation by organisationId.
        /// </summary>
        /// <param name="organisationId">organisationId</param>
        /// <returns>Returns OrganisationMasterViewModel.</returns>
        OrganisationMasterViewModel GetOrganisation(short organisationId);

        /// <summary>
        /// Update Organisation.
        /// </summary>
        /// <param name="organisationMasterViewModel">organisationMasterViewModel.</param>
        /// <returns>Returns updated OrganisationMasterViewModel</returns>
        OrganisationMasterViewModel UpdateOrganisation(OrganisationMasterViewModel organisationMasterViewModel);
    }
}

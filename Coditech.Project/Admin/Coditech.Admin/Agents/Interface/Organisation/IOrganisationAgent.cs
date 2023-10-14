using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IOrganisationAgent
    {
        /// <summary>
        /// Get Organisation by organisationId.
        /// </summary>
        /// <returns>Returns OrganisationMasterViewModel.</returns>
        OrganisationMasterViewModel GetOrganisation();

        /// <summary>
        /// Update Organisation.
        /// </summary>
        /// <param name="organisationMasterViewModel">organisationMasterViewModel.</param>
        /// <returns>Returns updated OrganisationMasterViewModel</returns>
        OrganisationMasterViewModel UpdateOrganisation(OrganisationMasterViewModel organisationMasterViewModel);
    }
}

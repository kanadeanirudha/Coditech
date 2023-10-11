using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IOrganisationAgent
    {
        /// <summary>
        /// Update Organisation.
        /// </summary>
        /// <param name="organisationMasterViewModel">organisationMasterViewModel.</param>
        /// <returns>Returns updated OrganisationMasterViewModel</returns>
        OrganisationMasterViewModel UpdateOrganisation(OrganisationMasterViewModel organisationMasterViewModel);
    }
}

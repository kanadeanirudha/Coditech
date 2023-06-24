using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public class OrganisationService : IOrganisationService
    {
        public OrganisationService()
        {
        }

        public virtual OrganisationModel UpdateOrganisation(OrganisationModel model)
        {
            OrganisationModel organisationModel = new OrganisationModel();
            return organisationModel;
        }
    }
}

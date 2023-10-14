using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public interface IOrganisationService
    {
        OrganisationModel GetOrganisation();

        OrganisationModel UpdateOrganisation(OrganisationModel model);
    }
}


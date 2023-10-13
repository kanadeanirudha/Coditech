using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public interface IOrganisationService
    {
        OrganisationModel GetOrganisation(short organisationMasterId);

        OrganisationModel UpdateOrganisation(OrganisationModel model);
    }
}


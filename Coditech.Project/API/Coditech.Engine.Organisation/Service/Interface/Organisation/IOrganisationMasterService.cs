using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public interface IOrganisationMasterService
    {
        OrganisationModel GetOrganisation();
        OrganisationModel UpdateOrganisation(OrganisationModel model);
    }
}


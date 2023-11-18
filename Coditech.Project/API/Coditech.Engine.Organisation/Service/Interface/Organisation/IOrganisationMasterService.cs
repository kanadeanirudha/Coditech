using Coditech.Common.API.Model;

namespace Coditech.API.Organisation.Service.Interface.Organisation
{
    public interface IOrganisationMasterService
    {
        OrganisationModel GetOrganisation();
        OrganisationModel UpdateOrganisation(OrganisationModel model);
    }
}


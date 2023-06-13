using Coditech.API.Data;
using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public class OrganisationService : IOrganisationService
    {
        private readonly GeneralDepartmentMasterDBContext _OrganisationDBContext;
        public OrganisationService(GeneralDepartmentMasterDBContext OrganisationDBContext)
        {
            _OrganisationDBContext = OrganisationDBContext;
        }

        public virtual OrganisationModel UpdateOrganisation(OrganisationModel model)
        {
            OrganisationModel organisationModel = new OrganisationModel();
            return organisationModel;
        }
    }
}

using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
namespace Coditech.API.Service
{
    public class OrganisationMasterService : IOrganisationMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<OrganisationMaster> _organisationMasterRepository;
        public OrganisationMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _organisationMasterRepository = new CoditechRepository<OrganisationMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        //Get Organisation by organisation id.
        public virtual OrganisationModel GetOrganisation()
        {
            OrganisationMaster organisationMasterData = _organisationMasterRepository.Table.FirstOrDefault();
            OrganisationModel model = HelperUtility.IsNull(organisationMasterData) ? new OrganisationModel() : organisationMasterData.FromEntityToModel<OrganisationModel>();
            return model;
        }

        //Update OrganisationMaster.
        public virtual OrganisationModel UpdateOrganisation(OrganisationModel organisationModel)
        {
            bool isOrganisationMasterUpdated = false;
            if (HelperUtility.IsNull(organisationModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (organisationModel.OrganisationMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "OrganisationMasterID"));

            //Update OrganisationMaster
            isOrganisationMasterUpdated = _organisationMasterRepository.Update(organisationModel.FromModelToEntity<OrganisationMaster>());
            if (!isOrganisationMasterUpdated)
            {
                organisationModel.HasError = true;
                organisationModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return organisationModel;
        }
    }
}
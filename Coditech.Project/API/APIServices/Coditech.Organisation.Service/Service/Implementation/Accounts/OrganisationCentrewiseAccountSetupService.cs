using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class OrganisationCentrewiseAccountSetupService : IOrganisationCentrewiseAccountSetupService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<OrganisationCentrewiseAccountSetup> _organisationCentrewiseAccountSetupRepository;
        public OrganisationCentrewiseAccountSetupService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _organisationCentrewiseAccountSetupRepository = new CoditechRepository<OrganisationCentrewiseAccountSetup>(_serviceProvider.GetService<Coditech_Entities>());
        }

        //Get Organisation Centrewise Account Setup by organisationCentrewiseAccountSetupId.
        public virtual OrganisationCentrewiseAccountSetupModel GetOrganisationCentrewiseAccountSetup(string centreCode)
        {
            //Get the OrganisationCentrewiseAccountSetup Details based on id.
            OrganisationCentrewiseAccountSetup organisationCentrewiseAccountSetup = _organisationCentrewiseAccountSetupRepository.Table.Where(x => x.CentreCode == centreCode)?.FirstOrDefault();
            OrganisationCentrewiseAccountSetupModel organisationCentrewiseAccountSetupModel = organisationCentrewiseAccountSetup?.FromEntityToModel<OrganisationCentrewiseAccountSetupModel>() ?? new OrganisationCentrewiseAccountSetupModel();
            if (IsNull(organisationCentrewiseAccountSetup))
            {
                organisationCentrewiseAccountSetupModel.CentreCode = centreCode;
            }
            return organisationCentrewiseAccountSetupModel;
        }

        //Update Organisation Centrewise Account Setup .
        public virtual bool UpdateOrganisationCentrewiseAccountSetup(OrganisationCentrewiseAccountSetupModel organisationCentrewiseAccountSetupModel)
        {
            if (IsNull(organisationCentrewiseAccountSetupModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            bool isOrganisationCentrewiseAccountSetupUpdated = false;
            OrganisationCentrewiseAccountSetup organisationCentrewiseAccountSetup = organisationCentrewiseAccountSetupModel.FromModelToEntity<OrganisationCentrewiseAccountSetup>();

            if (organisationCentrewiseAccountSetupModel.OrganisationCentrewiseAccountSetupId > 0)
                isOrganisationCentrewiseAccountSetupUpdated = _organisationCentrewiseAccountSetupRepository.Update(organisationCentrewiseAccountSetup);
            else
            {
                organisationCentrewiseAccountSetup = _organisationCentrewiseAccountSetupRepository.Insert(organisationCentrewiseAccountSetup);
                isOrganisationCentrewiseAccountSetupUpdated = organisationCentrewiseAccountSetup.OrganisationCentrewiseAccountSetupId > 0;
            }

            if (!isOrganisationCentrewiseAccountSetupUpdated)
            {
                organisationCentrewiseAccountSetupModel.HasError = true;
                organisationCentrewiseAccountSetupModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isOrganisationCentrewiseAccountSetupUpdated;
        }
    }
}
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class OrganisationAgent : BaseAgent, IOrganisationAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IOrganisationClient _organisationClient;
        #endregion

        #region Public Constructor
        public OrganisationAgent(ICoditechLogging coditechLogging, IOrganisationClient organisationClient)
        {
            _coditechLogging = coditechLogging;
            _organisationClient = GetClient<IOrganisationClient>(organisationClient);
        }
        #endregion

        #region Public Methods
        //Get Organisation by organisation id.
        public virtual OrganisationMasterViewModel GetOrganisation(short organisationId)
        {
            OrganisationResponse response = _organisationClient.GetOrganisation(organisationId);
            return response?.OrganisationModel.ToViewModel<OrganisationMasterViewModel>();
        }

        //Update Organisation.
        public virtual OrganisationMasterViewModel UpdateOrganisation(OrganisationMasterViewModel organisationMasterViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Organisation.ToString(), TraceLevel.Info);
                OrganisationResponse response = _organisationClient.UpdateOrganisation(organisationMasterViewModel.ToModel<OrganisationModel>());
                OrganisationModel organisationModel = response?.OrganisationModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.Organisation.ToString(), TraceLevel.Info);
                return IsNotNull(organisationModel) ? organisationModel.ToViewModel<OrganisationMasterViewModel>() : (OrganisationMasterViewModel)GetViewModelWithErrorMessage(new OrganisationMasterViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Organisation.ToString(), TraceLevel.Error);
                return (OrganisationMasterViewModel)GetViewModelWithErrorMessage(organisationMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
        #endregion
    }
}

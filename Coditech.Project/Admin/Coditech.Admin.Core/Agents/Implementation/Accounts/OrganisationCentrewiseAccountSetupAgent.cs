using System.Diagnostics;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.Admin.Agents
{
    public class OrganisationCentrewiseAccountSetupAgent : BaseAgent, IOrganisationCentrewiseAccountSetupAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IOrganisationCentrewiseAccountSetupClient _organisationCentrewiseAccountSetupClient;
        #endregion

        #region Public Constructor
        public OrganisationCentrewiseAccountSetupAgent(ICoditechLogging coditechLogging, IOrganisationCentrewiseAccountSetupClient organisationCentrewiseAccountSetupClient)
        {
            _coditechLogging = coditechLogging;
            _organisationCentrewiseAccountSetupClient = GetClient<IOrganisationCentrewiseAccountSetupClient>(organisationCentrewiseAccountSetupClient);
        }
        #endregion

        #region Public Methods
        //Get Organisation Centrewise Account Setup by organisationCentreId.
        public virtual OrganisationCentrewiseAccountSetupViewModel GetOrganisationCentrewiseAccountSetup(string centreCode)
        {
            OrganisationCentrewiseAccountSetupResponse response = _organisationCentrewiseAccountSetupClient.GetOrganisationCentrewiseAccountSetup(centreCode);
            return response?.OrganisationCentrewiseAccountSetupModel.ToViewModel<OrganisationCentrewiseAccountSetupViewModel>();
        }

        //Update Organisation Centrewise Account Setup.
        public virtual OrganisationCentrewiseAccountSetupViewModel UpdateOrganisationCentrewiseAccountSetup(OrganisationCentrewiseAccountSetupViewModel organisationCentrewiseAccountSetupViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.OrganisationCentrewiseAccountSetup.ToString(), TraceLevel.Info);
                OrganisationCentrewiseAccountSetupResponse response = _organisationCentrewiseAccountSetupClient.UpdateOrganisationCentrewiseAccountSetup(organisationCentrewiseAccountSetupViewModel.ToModel<OrganisationCentrewiseAccountSetupModel>());
                OrganisationCentrewiseAccountSetupModel organisationCentrewiseAccountSetupModel = response?.OrganisationCentrewiseAccountSetupModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.OrganisationCentrewiseAccountSetup.ToString(), TraceLevel.Info);
                if (!organisationCentrewiseAccountSetupModel.HasError)
                {
                    RemoveInSession(AdminConstants.AccountPrerequisiteSession);
                }
                return IsNotNull(organisationCentrewiseAccountSetupModel) ? organisationCentrewiseAccountSetupModel.ToViewModel<OrganisationCentrewiseAccountSetupViewModel>() : (OrganisationCentrewiseAccountSetupViewModel)GetViewModelWithErrorMessage(new OrganisationCentrewiseAccountSetupViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseAccountSetup.ToString(), TraceLevel.Error);
                return (OrganisationCentrewiseAccountSetupViewModel)GetViewModelWithErrorMessage(organisationCentrewiseAccountSetupViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
        #endregion
    }
}

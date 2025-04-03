using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Data;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;
namespace Coditech.Admin.Controllers
{
    public class OrganisationCentrewiseAccountSetupController : BaseController
    {
        private readonly IOrganisationCentrewiseAccountSetupAgent _organisationCentrewiseAccountSetupAgent;
        private const string createEdit = "~/Views/Accounts/OrganisationCentrewiseAccountSetup/CreateEdit.cshtml";

        public OrganisationCentrewiseAccountSetupController(IOrganisationCentrewiseAccountSetupAgent organisationCentrewiseAccountSetupAgent)
        {
            _organisationCentrewiseAccountSetupAgent = organisationCentrewiseAccountSetupAgent;
        }

        #region OrganisationCentrewiseAccountSetup
        [HttpGet]
        public virtual ActionResult GetAccountSetup()
        {
            return ActionView(createEdit, new OrganisationCentrewiseAccountSetupViewModel());
        }

        [HttpGet]
        public virtual ActionResult GetOrganisationCentrewiseAccountSetup(string centreCode)
        {
            OrganisationCentrewiseAccountSetupViewModel organisationCentrewiseAccountSetupViewModel = _organisationCentrewiseAccountSetupAgent.GetOrganisationCentrewiseAccountSetup(centreCode);
            return PartialView("~/Views/Accounts/OrganisationCentrewiseAccountSetup/_OrganisationCentrewiseAccountSetup.cshtml", organisationCentrewiseAccountSetupViewModel);
        }

        [HttpPost]
        public virtual ActionResult GetAccountSetup(OrganisationCentrewiseAccountSetupViewModel organisationCentrewiseAccountSetupViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentrewiseAccountSetupAgent.UpdateOrganisationCentrewiseAccountSetup(organisationCentrewiseAccountSetupViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("GetAccountSetup", new { centreCode = organisationCentrewiseAccountSetupViewModel.CentreCode });
            }
            return View(createEdit, organisationCentrewiseAccountSetupViewModel);
        }
        #endregion
    }
}


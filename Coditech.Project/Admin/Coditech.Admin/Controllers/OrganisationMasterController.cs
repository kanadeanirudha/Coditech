using Coditech.Admin.Agents;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class OrganisationMasterController : BaseController
    {
        private readonly IOrganisationAgent _organisationAgent;
        private const string createEdit = "~/Views/Organisation/CreateEdit.cshtml";

        public OrganisationMasterController(IOrganisationAgent organisationAgent)
        {
            _organisationAgent = organisationAgent;
        }

        [HttpGet]
        public virtual ActionResult Edit()
        {
            return ActionView(createEdit, new OrganisationMasterViewModel());
        }

        [HttpPost]
        public virtual ActionResult Edit(OrganisationMasterViewModel organisationMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationAgent.UpdateOrganisation(organisationMasterViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { organisationId = organisationMasterViewModel.OrganisationMasterId });
            }
            return View(createEdit, organisationMasterViewModel);
        }
        #region Protected
        #endregion
    }
}
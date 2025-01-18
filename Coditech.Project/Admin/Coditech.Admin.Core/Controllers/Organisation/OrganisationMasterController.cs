using Coditech.Admin.Agents;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class OrganisationMasterController : BaseController
    {
        private readonly IOrganisationAgent _organisationAgent;
        private const string createEdit = "~/Views/Organisation/OrganisationDetails/CreateEdit.cshtml";

        public OrganisationMasterController(IOrganisationAgent organisationAgent)
        {
            _organisationAgent = organisationAgent;
        }

        [HttpGet]
        public virtual ActionResult Update()
        {
            OrganisationMasterViewModel organisationMasterViewModel = _organisationAgent.GetOrganisation();
            return ActionView(createEdit, organisationMasterViewModel);
        }

        [HttpPost]
        public virtual ActionResult Update(OrganisationMasterViewModel organisationMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationAgent.UpdateOrganisation(organisationMasterViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction<OrganisationMasterController>(x => x.Update());
            }
            return View(createEdit, organisationMasterViewModel);
        }
        #region Protected
        #endregion
    }
}
using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
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
        public virtual ActionResult GetAccountSetup(DataTableViewModel dataTableModel)
        {
            GetListOnlyIfSingleCentre(dataTableModel);
            OrganisationCentrewiseAccountSetupViewModel organisationCentrewiseAccountSetupViewModel = new OrganisationCentrewiseAccountSetupViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode))
                organisationCentrewiseAccountSetupViewModel = _organisationCentrewiseAccountSetupAgent.GetOrganisationCentrewiseAccountSetup(dataTableModel.SelectedCentreCode);

            return ActionView(createEdit, organisationCentrewiseAccountSetupViewModel);
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
                if (string.Equals(organisationCentrewiseAccountSetupViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("GetAccountSetup", new DataTableViewModel() { SelectedCentreCode = organisationCentrewiseAccountSetupViewModel.CentreCode });

                }
                else if (string.Equals(organisationCentrewiseAccountSetupViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("GetAccountSetup", new DataTableViewModel() { SelectedCentreCode = organisationCentrewiseAccountSetupViewModel.CentreCode });
                }
            }
            return View(createEdit, organisationCentrewiseAccountSetupViewModel);
        }
        #endregion
    }
}


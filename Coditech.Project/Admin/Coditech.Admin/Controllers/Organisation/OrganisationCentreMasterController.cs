using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Coditech.ViewModel;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class OrganisationCentreMasterController : BaseController
    {
        private readonly IOrganisationCentreAgent _organisationCentreAgent;
        private const string createEdit = "~/Views/Organisation/OrganisationCentre/CreateEdit.cshtml";
        private const string OrganisationCentrePrintingFormat = "~/Views/Organisation/OrganisationCentre/OrganisationCentrePrintingFormat.cshtml";
        private const string OrganisationCentrewiseGSTCredential = "~/Views/Organisation/OrganisationCentre/OrganisationCentrewiseGSTCredential.cshtml";

        public OrganisationCentreMasterController(IOrganisationCentreAgent organisationCentreAgent)
        {
            _organisationCentreAgent = organisationCentreAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            OrganisationCentreListViewModel list = _organisationCentreAgent.GetOrganisationCentreList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Organisation/OrganisationCentre/_List.cshtml", list);
            }
            return View($"~/Views/Organisation/OrganisationCentre/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new OrganisationCentreViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(OrganisationCentreViewModel organisationCentreViewModel)
        {
            if (ModelState.IsValid)
            {
                organisationCentreViewModel = _organisationCentreAgent.CreateOrganisationCentre(organisationCentreViewModel);
                if (!organisationCentreViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<OrganisationCentreMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(organisationCentreViewModel.ErrorMessage));
            return View(createEdit, organisationCentreViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short organisationCentreId)
        {
            OrganisationCentreViewModel organisationCentreViewModel = _organisationCentreAgent.GetOrganisationCentre(organisationCentreId);
            return ActionView(createEdit, organisationCentreViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(OrganisationCentreViewModel organisationCentreViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentreAgent.UpdateOrganisationCentre(organisationCentreViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { organisationCentreId = organisationCentreViewModel.OrganisationCentreMasterId });
            }
            return View(createEdit, organisationCentreViewModel);
        }

        public virtual ActionResult Delete(string organisationCentreIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(organisationCentreIds))
            {
                status = _organisationCentreAgent.DeleteOrganisationCentre(organisationCentreIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<OrganisationCentreMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<OrganisationCentreMasterController>(x => x.List(null));
        }

        [HttpGet]
        public virtual ActionResult PrintingFormat(short organisationCentreId)
        {
            OrganisationCentrePrintingFormatViewModel organisationCentrePrintingFormatViewModel = _organisationCentreAgent.GetPrintingFormat(organisationCentreId);
            return ActionView(OrganisationCentrePrintingFormat, organisationCentrePrintingFormatViewModel);
        }

        [HttpPost]
        public virtual ActionResult PrintingFormat(OrganisationCentrePrintingFormatViewModel organisationCentrePrintingFormatViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentreAgent.UpdatePrintingFormat(organisationCentrePrintingFormatViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("PrintingFormat", new { organisationCentreId = organisationCentrePrintingFormatViewModel.OrganisationCentreMasterId });
            }
            return View(OrganisationCentrePrintingFormat, organisationCentrePrintingFormatViewModel);
        }

        [HttpGet]
        public virtual ActionResult CentrewiseGSTSetup(short organisationCentreId)
        {
            OrganisationCentrewiseGSTCredentialViewModel organisationCentrewiseGSTCredentialViewModel = _organisationCentreAgent.GetCentrewiseGSTSetup(organisationCentreId);
            return ActionView(OrganisationCentrewiseGSTCredential, organisationCentrewiseGSTCredentialViewModel);
        }

        [HttpPost]
        public virtual ActionResult CentrewiseGSTSetup(OrganisationCentrewiseGSTCredentialViewModel organisationCentrewiseGSTCredentialViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentreAgent.UpdateCentrewiseGSTSetup(organisationCentrewiseGSTCredentialViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("CentrewiseGSTSetup", new { organisationCentreId = organisationCentrewiseGSTCredentialViewModel.OrganisationCentreMasterId });
            }
            return View(OrganisationCentrewiseGSTCredential, organisationCentrewiseGSTCredentialViewModel);
        }
    }
}

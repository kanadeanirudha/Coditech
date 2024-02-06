using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class OrganisationCentrewiseBuildingMasterController : BaseController
    {
        private readonly IOrganisationCentrewiseBuildingAgent _organisationCentrewiseBuildingAgent;
        private const string createEdit = "~/Views/Organisation/OrganisationCentrewiseBuilding/CreateEdit.cshtml";

        public OrganisationCentrewiseBuildingMasterController(IOrganisationCentrewiseBuildingAgent organisationCentrewiseBuildingAgent)
        {
            _organisationCentrewiseBuildingAgent = organisationCentrewiseBuildingAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {

            OrganisationCentrewiseBuildingListViewModel list = new OrganisationCentrewiseBuildingListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode))
            {
                list = _organisationCentrewiseBuildingAgent.GetOrganisationCentrewiseBuildingList(dataTableViewModel);
            }
            list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Organisation/OrganisationCentrewiseBuilding/_List.cshtml", list);
            }
            return View($"~/Views/Organisation/OrganisationCentrewiseBuilding/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new OrganisationCentrewiseBuildingViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(OrganisationCentrewiseBuildingViewModel organisationCentrewiseBuildingViewModel)
        {
            if (ModelState.IsValid)
            {
                organisationCentrewiseBuildingViewModel = _organisationCentrewiseBuildingAgent.CreateOrganisationCentrewiseBuilding(organisationCentrewiseBuildingViewModel);
                if (!organisationCentrewiseBuildingViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<OrganisationCentrewiseBuildingMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(organisationCentrewiseBuildingViewModel.ErrorMessage));
            return View(createEdit, organisationCentrewiseBuildingViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short organisationCentrewiseBuildingId)
        {
            OrganisationCentrewiseBuildingViewModel organisationCentrewiseBuildingViewModel = _organisationCentrewiseBuildingAgent.GetOrganisationCentrewiseBuilding(organisationCentrewiseBuildingId);
            return ActionView(createEdit, organisationCentrewiseBuildingViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(OrganisationCentrewiseBuildingViewModel organisationCentrewiseBuildingViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentrewiseBuildingAgent.UpdateOrganisationCentrewiseBuilding(organisationCentrewiseBuildingViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { organisationCentrewiseBuildingId = organisationCentrewiseBuildingViewModel.OrganisationCentrewiseBuildingMasterId });
            }
            return View(createEdit, organisationCentrewiseBuildingViewModel);
        }

        public virtual ActionResult Delete(string organisationCentrewiseBuildingIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(organisationCentrewiseBuildingIds))
            {
                status = _organisationCentrewiseBuildingAgent.DeleteOrganisationCentrewiseBuilding(organisationCentrewiseBuildingIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<OrganisationCentrewiseBuildingMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<OrganisationCentrewiseBuildingMasterController>(x => x.List(null));
        }

    }
}

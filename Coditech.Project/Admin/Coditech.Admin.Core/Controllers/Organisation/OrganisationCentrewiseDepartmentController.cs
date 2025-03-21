﻿using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class OrganisationCentrewiseDepartmentController : BaseController
    {
        private readonly IOrganisationCentrewiseDepartmentAgent _organisationCentrewiseDepartmentAgent;
        private const string createEdit = "~/Views/Organisation/OrganisationCentrewiseDepartment/CreateEdit.cshtml";
        public OrganisationCentrewiseDepartmentController(IOrganisationCentrewiseDepartmentAgent organisationCentrewiseDepartmentAgent)
        {
            _organisationCentrewiseDepartmentAgent = organisationCentrewiseDepartmentAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {
            OrganisationCentrewiseDepartmentListViewModel list = new OrganisationCentrewiseDepartmentListViewModel();
            GetListOnlyIfSingleCentre(dataTableViewModel);
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode))
            {
                list = _organisationCentrewiseDepartmentAgent.GetOrganisationCentrewiseDepartmentList(dataTableViewModel);
            }
            list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Organisation/OrganisationCentrewiseDepartment/_List.cshtml", list);
            }
            return View($"~/Views/Organisation/OrganisationCentrewiseDepartment/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult GetAssociateUnAssociateCentrewiseDepartment(OrganisationCentrewiseDepartmentViewModel organisationCentrewiseDepartmentViewModel)
        {
            return PartialView("~/Views/Organisation/OrganisationCentrewiseDepartment/_AssociateUnAssociateCentrewiseDepartment.cshtml", organisationCentrewiseDepartmentViewModel);
        }

        [HttpPost]
        public virtual ActionResult AssociateUnAssociateCentrewiseDepartment(OrganisationCentrewiseDepartmentViewModel organisationCentrewiseDepartmentViewModel)
        {
            SetNotificationMessage(_organisationCentrewiseDepartmentAgent.AssociateUnAssociateCentrewiseDepartment(organisationCentrewiseDepartmentViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
            return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = organisationCentrewiseDepartmentViewModel.CentreCode });
        }
    }
}
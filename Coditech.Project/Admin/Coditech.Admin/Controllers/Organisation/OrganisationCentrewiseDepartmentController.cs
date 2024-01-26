using Coditech.Admin.Agents;
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
            OrganisationCentrewiseDepartmentListViewModel list = _organisationCentrewiseDepartmentAgent.GetOrganisationCentrewiseDepartmentList(dataTableViewModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Organisation/OrganisationCentrewiseDepartment/_List.cshtml", list);
            }
            return View($"~/Views/Organisation/OrganisationCentrewiseDepartment/List.cshtml", list);
        }

        [HttpPost]
        public virtual ActionResult AssociateUnAssociateCentrewiseDepartment(OrganisationCentrewiseDepartmentViewModel organisationCentrewiseDepartmentViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentrewiseDepartmentAgent.UpdateAssociateUnAssociateCentrewiseDepartment(organisationCentrewiseDepartmentViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("AssociateUnAssociateCentrewiseDepartment", new { generalDepartmentId = organisationCentrewiseDepartmentViewModel.GeneralDepartmentMasterId });
            }
            return View(createEdit, organisationCentrewiseDepartmentViewModel);
        }
    }
}
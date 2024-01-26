using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

using System.Data;

namespace Coditech.Admin.Controllers
{
    public class OrganisationCentrewiseDepartmentController : BaseController
    {
        private readonly IOrganisationCentrewiseDepartmentAgent _organisationCentrewiseDepartmentAgent;
        public OrganisationCentrewiseDepartmentController(IOrganisationCentrewiseDepartmentAgent organisationCentrewiseDepartmentAgent)
        {
            _organisationCentrewiseDepartmentAgent = organisationCentrewiseDepartmentAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {
            OrganisationCentrewiseDepartmentListViewModel list = new OrganisationCentrewiseDepartmentListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode))
            {
                list = _organisationCentrewiseDepartmentAgent.GetOrganisationCentrewiseDepartmentList(dataTableViewModel);
            }
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
            //SetNotificationMessage(_gymMemberDetailsAgent.UpdateMemberPersonalDetails(gymCreateEditMemberViewModel).HasError
            //    ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
            //    : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
            return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = organisationCentrewiseDepartmentViewModel.CentreCode });
        }
    }
}
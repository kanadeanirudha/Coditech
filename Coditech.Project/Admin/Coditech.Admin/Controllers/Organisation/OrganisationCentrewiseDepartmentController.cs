using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;

using Microsoft.AspNetCore.Mvc;

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
            OrganisationCentrewiseDepartmentListViewModel list = _organisationCentrewiseDepartmentAgent.GetOrganisationCentrewiseDepartmentList(dataTableViewModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Organisation/OrganisationCentrewiseDepartment/_List.cshtml", list);
            }
            return View($"~/Views/Organisation/OrganisationCentrewiseDepartment/List.cshtml", list);
        }
    }
}
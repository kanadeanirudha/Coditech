using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class OrganisationCentrewisePolicyController : BaseController
    {
        private readonly IOrganisationCentrewisePolicyAgent _organisationCentrewisePolicyAgent;
        private const string createEdit = "~/Views/Organisation/OrganisationCentrewisePolicy/CreateEdit.cshtml";
        public OrganisationCentrewisePolicyController(IOrganisationCentrewisePolicyAgent organisationCentrewisePolicyAgent)
        {
            _organisationCentrewisePolicyAgent = organisationCentrewisePolicyAgent;
        }
        public virtual ActionResult List(string centreCode)
        {
            GeneralPolicyDetailsListViewModel list = new GeneralPolicyDetailsListViewModel();
           // GetListOnlyIfSingleCentre(new DataTableViewModel { SelectedCentreCode = centreCode });
            if (!string.IsNullOrEmpty(centreCode))
            {
                list = _organisationCentrewisePolicyAgent.GetOrganisationCentrewisePolicyList(centreCode);
            }
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Organisation/OrganisationCentrewisePolicy/_List.cshtml", list);
            }
            return View($"~/Views/Organisation/OrganisationCentrewisePolicy/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult GetCentrewisePolicyDetails(string centreCode, short generalPolicyRulesId)
        {
            GeneralPolicyDetailsViewModel generalPolicyDetailsViewModel = _organisationCentrewisePolicyAgent.GetCentrewisePolicyDetails(centreCode, generalPolicyRulesId);
            return PartialView("~/Views/Organisation/OrganisationCentrewisePolicy/_CentrewisePolicy.cshtml", generalPolicyDetailsViewModel);
        }

        [HttpPost]
        public virtual ActionResult CentrewisePolicyDetails(GeneralPolicyDetailsViewModel generalPolicyDetailsViewModel)
        {
            SetNotificationMessage(_organisationCentrewisePolicyAgent.CentrewisePolicyDetails(generalPolicyDetailsViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
            return RedirectToAction("List", new { centreCode = generalPolicyDetailsViewModel.CentreCode });
        }

        public virtual ActionResult DeleteCentrewisePolicy(string generalPolicyRulesIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(generalPolicyRulesIds))
            {
                status = _organisationCentrewisePolicyAgent.DeleteCentrewisePolicy(generalPolicyRulesIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<OrganisationCentrewisePolicyController>(x => x.List(null));
            }
            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<OrganisationCentrewisePolicyController>(x => x.List(null));
        }
    }
}
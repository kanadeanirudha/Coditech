using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;
namespace Coditech.Admin.Controllers
{
    public class OrganisationCentrewiseJoiningCodeController : BaseController
    {
        private readonly IOrganisationCentrewiseJoiningCodeAgent _oganisationCentrewiseJoiningCodeAgent;
        private const string createEdit = "~/Views/Organisation/OrganisationCentrewiseJoiningCode/CreateEdit.cshtml";
        public OrganisationCentrewiseJoiningCodeController(IOrganisationCentrewiseJoiningCodeAgent oganisationCentrewiseJoiningCodeAgent)
        {
            _oganisationCentrewiseJoiningCodeAgent = oganisationCentrewiseJoiningCodeAgent;
        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            OrganisationCentrewiseJoiningCodeListViewModel list = new OrganisationCentrewiseJoiningCodeListViewModel();
            GetListOnlyIfSingleCentre(dataTableModel);
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode))
            {
                list = _oganisationCentrewiseJoiningCodeAgent.GetOrganisationCentrewiseJoiningCodeList(dataTableModel);
            }
            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;

            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Organisation/OrganisationCentrewiseJoiningCode/_List.cshtml", list);
            }
            return View($"~/Views/Organisation/OrganisationCentrewiseJoiningCode/List.cshtml", list);
        }
        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new OrganisationCentrewiseJoiningCodeViewModel());
        }
        [HttpPost]
        public virtual ActionResult Create(OrganisationCentrewiseJoiningCodeViewModel organisationCentrewiseJoiningCodeViewModel)
        {
            if (ModelState.IsValid)
            {
                organisationCentrewiseJoiningCodeViewModel = _oganisationCentrewiseJoiningCodeAgent.CreateOrganisationCentrewiseJoiningCode(organisationCentrewiseJoiningCodeViewModel);
                if (!organisationCentrewiseJoiningCodeViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", new { SelectedCentreCode = organisationCentrewiseJoiningCodeViewModel.CentreCode });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(organisationCentrewiseJoiningCodeViewModel.ErrorMessage));
            return View(createEdit, organisationCentrewiseJoiningCodeViewModel);
        }
    }
}

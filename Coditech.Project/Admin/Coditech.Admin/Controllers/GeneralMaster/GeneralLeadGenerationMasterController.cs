using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralLeadGenerationMasterController : BaseController
    {
        private readonly IGeneralLeadGenerationAgent _generalLeadGenerationAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralLeadGenerationMaster/CreateEdit.cshtml";

        public GeneralLeadGenerationMasterController(IGeneralLeadGenerationAgent generalLeadGenerationAgent)
        {
            _generalLeadGenerationAgent = generalLeadGenerationAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralLeadGenerationListViewModel list = _generalLeadGenerationAgent.GetLeadGenerationList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralLeadGenerationMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralLeadGenerationMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralLeadGenerationViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralLeadGenerationViewModel generalLeadGenerationViewModel)
        {
            if (ModelState.IsValid)
            {
                generalLeadGenerationViewModel = _generalLeadGenerationAgent.CreateLeadGeneration(generalLeadGenerationViewModel);
                if (!generalLeadGenerationViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GeneralLeadGenerationMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalLeadGenerationViewModel.ErrorMessage));
            return View(createEdit, generalLeadGenerationViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(long generalLeadGenerationId)
        {
            GeneralLeadGenerationViewModel generalLeadGenerationViewModel = _generalLeadGenerationAgent.GetLeadGeneration(generalLeadGenerationId);
            return ActionView(createEdit, generalLeadGenerationViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralLeadGenerationViewModel generalLeadGenerationViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalLeadGenerationAgent.UpdateLeadGeneration(generalLeadGenerationViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { generalLeadGenerationId = generalLeadGenerationViewModel.GeneralLeadGenerationMasterId });
            }
            return View(createEdit, generalLeadGenerationViewModel);
        }

        public virtual ActionResult Delete(string LeadGenerationIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(LeadGenerationIds))
            {
                status = _generalLeadGenerationAgent.DeleteLeadGeneration(LeadGenerationIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralLeadGenerationMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralLeadGenerationMasterController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}
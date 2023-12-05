using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralRegionMasterController : BaseController
    {
        private readonly IGeneralRegionAgent _generalRegionAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralRegionMaster/CreateEdit.cshtml";

        public GeneralRegionMasterController(IGeneralRegionAgent generalRegionAgent)
        {
            _generalRegionAgent = generalRegionAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralRegionListViewModel list = _generalRegionAgent.GetRegionList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralRegionMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralRegionMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralRegionViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralRegionViewModel generalRegionViewModel)
        {
            if (ModelState.IsValid)
            {
                generalRegionViewModel = _generalRegionAgent.CreateRegion(generalRegionViewModel);
                if (!generalRegionViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GeneralRegionMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalRegionViewModel.ErrorMessage));
            return View(createEdit, generalRegionViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short generalRegionId)
        {
            GeneralRegionViewModel generalRegionViewModel = _generalRegionAgent.GetRegion(generalRegionId);
            return ActionView(createEdit, generalRegionViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralRegionViewModel generalRegionViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalRegionAgent.UpdateRegion(generalRegionViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { generalRegionId = generalRegionViewModel.GeneralRegionMasterId });
            }
            return View(createEdit, generalRegionViewModel);
        }

        public virtual ActionResult Delete(string regionIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(regionIds))
            {
                status = _generalRegionAgent.DeleteRegion(regionIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralRegionMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralRegionMasterController>(x => x.List(null));
        }
        #region Protected
        #endregion
    }
}
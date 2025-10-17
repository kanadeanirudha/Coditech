using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralOccupationMasterController : BaseController
    {
        private readonly IGeneralOccupationAgent _generalOccupationAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralOccupationMaster/CreateEdit.cshtml";

        public GeneralOccupationMasterController(IGeneralOccupationAgent generalOccupationAgent)
        {
            _generalOccupationAgent = generalOccupationAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralOccupationListViewModel list = _generalOccupationAgent.GetOccupationList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralOccupationMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralOccupationMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralOccupationViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralOccupationViewModel generalOccupationViewModel)
        {
            if (ModelState.IsValid)
            {
                generalOccupationViewModel = _generalOccupationAgent.CreateOccupation(generalOccupationViewModel);
                if (!generalOccupationViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    if (string.Equals(generalOccupationViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { generalOccupationId = generalOccupationViewModel.GeneralOccupationMasterId });
                    }
                    else if (string.Equals(generalOccupationViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToList);
                    }
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalOccupationViewModel.ErrorMessage));
            return View(createEdit, generalOccupationViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short generalOccupationId)
        {
            GeneralOccupationViewModel generalOccupationViewModel = _generalOccupationAgent.GetOccupation(generalOccupationId);
            return ActionView(createEdit, generalOccupationViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralOccupationViewModel generalOccupationViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalOccupationAgent.UpdateOccupation(generalOccupationViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(generalOccupationViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { generalOccupationId = generalOccupationViewModel.GeneralOccupationMasterId });
                }
                else if (string.Equals(generalOccupationViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(createEdit, generalOccupationViewModel);
        }

        public virtual ActionResult Delete(string OccupationIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(OccupationIds))
            {
                status = _generalOccupationAgent.DeleteOccupation(OccupationIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralOccupationMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralOccupationMasterController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}
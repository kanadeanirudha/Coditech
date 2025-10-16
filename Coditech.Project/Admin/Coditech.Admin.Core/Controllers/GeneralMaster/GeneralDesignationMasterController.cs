using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralDesignationMasterController : BaseController
    {
        private readonly IGeneralDesignationAgent _generalDesignationAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralDesignationMaster/CreateEdit.cshtml";

        public GeneralDesignationMasterController(IGeneralDesignationAgent generalDesignationAgent)
        {
            _generalDesignationAgent = generalDesignationAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralDesignationListViewModel list = _generalDesignationAgent.GetDesignationList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralDesignationMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralDesignationMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralDesignationViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralDesignationViewModel generalDesignationViewModel)
        {
            if (ModelState.IsValid)
            {
                generalDesignationViewModel = _generalDesignationAgent.CreateDesignation(generalDesignationViewModel);
                if (!generalDesignationViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    if (string.Equals(generalDesignationViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { designationId = generalDesignationViewModel.EmployeeDesignationMasterId });
                    }
                    else if (string.Equals(generalDesignationViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToList);
                    }
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalDesignationViewModel.ErrorMessage));
            return View(createEdit, generalDesignationViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short designationId)
        {
            GeneralDesignationViewModel generalDesignationViewModel = _generalDesignationAgent.GetDesignation(designationId);
            return ActionView(createEdit, generalDesignationViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralDesignationViewModel generalDesignationViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalDesignationAgent.UpdateDesignation(generalDesignationViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(generalDesignationViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { designationId = generalDesignationViewModel.EmployeeDesignationMasterId });
                }
                else if (string.Equals(generalDesignationViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(createEdit, generalDesignationViewModel);
        }

        public virtual ActionResult Delete(string designationIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(designationIds))
            {
                status = _generalDesignationAgent.DeleteDesignation(designationIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralDesignationMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralDesignationMasterController>(x => x.List(null));
        }
        #region Protected

        #endregion
    }
}
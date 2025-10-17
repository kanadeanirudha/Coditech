using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralTaxMasterController : BaseController
    {
        private readonly IGeneralTaxMasterAgent _generalTaxMasterAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralTaxMaster/CreateEdit.cshtml";

        public GeneralTaxMasterController(IGeneralTaxMasterAgent generalTaxMasterAgent)
        {
            _generalTaxMasterAgent = generalTaxMasterAgent;
        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralTaxMasterListViewModel list = _generalTaxMasterAgent.GetTaxMasterList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralTaxMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralTaxMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralTaxMasterViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralTaxMasterViewModel generalTaxMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                generalTaxMasterViewModel = _generalTaxMasterAgent.CreateTaxMaster(generalTaxMasterViewModel);
                if (!generalTaxMasterViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    if (string.Equals(generalTaxMasterViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { taxMasterId = generalTaxMasterViewModel.GeneralTaxMasterId });
                    }
                    else if (string.Equals(generalTaxMasterViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToList);
                    }
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalTaxMasterViewModel.ErrorMessage));
            return View(createEdit, generalTaxMasterViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short taxMasterId)
        {
            GeneralTaxMasterViewModel generalTaxMasterViewModel = _generalTaxMasterAgent.GetTaxMaster(taxMasterId);
            return ActionView(createEdit, generalTaxMasterViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralTaxMasterViewModel generalTaxMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalTaxMasterAgent.UpdateTaxMaster(generalTaxMasterViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(generalTaxMasterViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { taxMasterId = generalTaxMasterViewModel.GeneralTaxMasterId });
                }
                else if (string.Equals(generalTaxMasterViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(createEdit, generalTaxMasterViewModel);
        }

        public virtual ActionResult Delete(string taxMasterIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(taxMasterIds))
            {
                status = _generalTaxMasterAgent.DeleteTaxMaster(taxMasterIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralTaxMasterController>(x => x.List(null));
            }
            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralTaxMasterController>(x => x.List(null));
        }
        #region Protected
        #endregion
    }
}
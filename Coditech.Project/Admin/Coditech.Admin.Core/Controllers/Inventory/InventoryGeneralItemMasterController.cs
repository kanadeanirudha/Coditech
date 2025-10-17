using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class InventoryGeneralItemMasterController : BaseController
    {
        private readonly IInventoryGeneralItemMasterAgent _inventoryGeneralItemMasterAgent;
        private const string createEdit = "~/Views/Inventory/InventoryGeneralItemMaster/CreateEdit.cshtml";

        public InventoryGeneralItemMasterController(IInventoryGeneralItemMasterAgent inventoryGeneralItemMasterAgent)
        {
            _inventoryGeneralItemMasterAgent = inventoryGeneralItemMasterAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            InventoryGeneralItemMasterListViewModel list = _inventoryGeneralItemMasterAgent.GetInventoryGeneralItemMasterList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Inventory/InventoryGeneralItemMaster/_List.cshtml", list);
            }
            return View($"~/Views/Inventory/InventoryGeneralItemMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new InventoryGeneralItemMasterViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(InventoryGeneralItemMasterViewModel inventoryGeneralItemMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                inventoryGeneralItemMasterViewModel = _inventoryGeneralItemMasterAgent.CreateInventoryGeneralItemMaster(inventoryGeneralItemMasterViewModel);
                if (!inventoryGeneralItemMasterViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    if (string.Equals(inventoryGeneralItemMasterViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { inventoryGeneralItemMasterId = inventoryGeneralItemMasterViewModel.InventoryGeneralItemMasterId });
                    }
                    else if (string.Equals(inventoryGeneralItemMasterViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToList);
                    }
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(inventoryGeneralItemMasterViewModel.ErrorMessage));
            return View(createEdit, inventoryGeneralItemMasterViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int inventoryGeneralItemMasterId)
        {
            InventoryGeneralItemMasterViewModel inventoryGeneralItemMasterViewModel = _inventoryGeneralItemMasterAgent.GetInventoryGeneralItemMaster(inventoryGeneralItemMasterId);
            return ActionView(createEdit, inventoryGeneralItemMasterViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(InventoryGeneralItemMasterViewModel inventoryGeneralItemMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_inventoryGeneralItemMasterAgent.UpdateInventoryGeneralItemMaster(inventoryGeneralItemMasterViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(inventoryGeneralItemMasterViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { inventoryGeneralItemMasterId = inventoryGeneralItemMasterViewModel.InventoryGeneralItemMasterId });
                }
                else if (string.Equals(inventoryGeneralItemMasterViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(createEdit, inventoryGeneralItemMasterViewModel);
        }

        public virtual ActionResult Delete(string InventoryGeneralItemMasterIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(InventoryGeneralItemMasterIds))
            {
                status = _inventoryGeneralItemMasterAgent.DeleteInventoryGeneralItemMaster(InventoryGeneralItemMasterIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<InventoryGeneralItemMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<InventoryGeneralItemMasterController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}
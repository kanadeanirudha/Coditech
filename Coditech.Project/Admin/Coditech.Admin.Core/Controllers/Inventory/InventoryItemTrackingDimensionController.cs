using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class InventoryItemTrackingDimensionController : BaseController
    {
        private readonly IInventoryItemTrackingDimensionAgent _inventoryItemTrackingDimensionAgent;
        private const string createEdit = "~/Views/Inventory/InventoryItemTrackingDimension/CreateEdit.cshtml";

        public InventoryItemTrackingDimensionController(IInventoryItemTrackingDimensionAgent inventoryItemTrackingDimensionAgent)
        {
            _inventoryItemTrackingDimensionAgent = inventoryItemTrackingDimensionAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            InventoryItemTrackingDimensionListViewModel list = _inventoryItemTrackingDimensionAgent.GetInventoryItemTrackingDimensionList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Inventory/InventoryItemTrackingDimension/_List.cshtml", list);
            }
            return View($"~/Views/Inventory/InventoryItemTrackingDimension/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new InventoryItemTrackingDimensionViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(InventoryItemTrackingDimensionViewModel inventoryItemTrackingDimensionViewModel)
        {
            if (ModelState.IsValid)
            {
                inventoryItemTrackingDimensionViewModel = _inventoryItemTrackingDimensionAgent.CreateInventoryItemTrackingDimension(inventoryItemTrackingDimensionViewModel);
                if (!inventoryItemTrackingDimensionViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    if (string.Equals(inventoryItemTrackingDimensionViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { inventoryItemTrackingDimensionId = inventoryItemTrackingDimensionViewModel.InventoryItemTrackingDimensionId });
                    }
                    else if (string.Equals(inventoryItemTrackingDimensionViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToList);
                    }
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(inventoryItemTrackingDimensionViewModel.ErrorMessage));
            return View(createEdit, inventoryItemTrackingDimensionViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short inventoryItemTrackingDimensionId)
        {
            InventoryItemTrackingDimensionViewModel inventoryItemTrackingDimensionViewModel = _inventoryItemTrackingDimensionAgent.GetInventoryItemTrackingDimension(inventoryItemTrackingDimensionId);
            return ActionView(createEdit, inventoryItemTrackingDimensionViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(InventoryItemTrackingDimensionViewModel inventoryItemTrackingDimensionViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_inventoryItemTrackingDimensionAgent.UpdateInventoryItemTrackingDimension(inventoryItemTrackingDimensionViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(inventoryItemTrackingDimensionViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { inventoryItemTrackingDimensionId = inventoryItemTrackingDimensionViewModel.InventoryItemTrackingDimensionId });
                }
                else if (string.Equals(inventoryItemTrackingDimensionViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(createEdit, inventoryItemTrackingDimensionViewModel);
        }

        public virtual ActionResult Delete(string inventoryItemTrackingDimensionIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(inventoryItemTrackingDimensionIds))
            {
                status = _inventoryItemTrackingDimensionAgent.DeleteInventoryItemTrackingDimension(inventoryItemTrackingDimensionIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<InventoryItemTrackingDimensionController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<InventoryItemTrackingDimensionController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}
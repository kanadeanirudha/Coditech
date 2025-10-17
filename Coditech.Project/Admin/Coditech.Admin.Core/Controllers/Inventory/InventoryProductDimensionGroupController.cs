using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class InventoryProductDimensionGroupController : BaseController
    {
        private readonly IInventoryProductDimensionGroupAgent _inventoryProductDimensionGroupAgent;
        private const string createEdit = "~/Views/Inventory/InventoryProductDimensionGroup/CreateEdit.cshtml";

        public InventoryProductDimensionGroupController(IInventoryProductDimensionGroupAgent inventoryProductDimensionGroupAgent)
        {
            _inventoryProductDimensionGroupAgent = inventoryProductDimensionGroupAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            InventoryProductDimensionGroupListViewModel list = _inventoryProductDimensionGroupAgent.GetInventoryProductDimensionGroupList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Inventory/InventoryProductDimensionGroup/_List.cshtml", list);
            }
            return View($"~/Views/Inventory/InventoryProductDimensionGroup/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            InventoryProductDimensionGroupViewModel inventoryProductDimensionGroupViewModel = _inventoryProductDimensionGroupAgent.GetInventoryProductDimensionGroup(0);
            return View(createEdit, inventoryProductDimensionGroupViewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(InventoryProductDimensionGroupViewModel inventoryProductDimensionGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                inventoryProductDimensionGroupViewModel = _inventoryProductDimensionGroupAgent.CreateInventoryProductDimensionGroup(inventoryProductDimensionGroupViewModel);
                if (!inventoryProductDimensionGroupViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    if (string.Equals(inventoryProductDimensionGroupViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { inventoryProductDimensionGroupId = inventoryProductDimensionGroupViewModel.InventoryProductDimensionGroupId });
                    }
                    else if (string.Equals(inventoryProductDimensionGroupViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToList);
                    }
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(inventoryProductDimensionGroupViewModel.ErrorMessage));
            return View(createEdit, inventoryProductDimensionGroupViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int inventoryProductDimensionGroupId)
        {
            InventoryProductDimensionGroupViewModel inventoryProductDimensionGroupViewModel = _inventoryProductDimensionGroupAgent.GetInventoryProductDimensionGroup(inventoryProductDimensionGroupId);
            return ActionView(createEdit, inventoryProductDimensionGroupViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(InventoryProductDimensionGroupViewModel inventoryProductDimensionGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_inventoryProductDimensionGroupAgent.UpdateInventoryProductDimensionGroup(inventoryProductDimensionGroupViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(inventoryProductDimensionGroupViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { inventoryProductDimensionGroupId = inventoryProductDimensionGroupViewModel.InventoryProductDimensionGroupId });
                }
                else if (string.Equals(inventoryProductDimensionGroupViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(createEdit, inventoryProductDimensionGroupViewModel);
        }

        public virtual ActionResult Delete(string inventoryProductDimensionGroupIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(inventoryProductDimensionGroupIds))
            {
                status = _inventoryProductDimensionGroupAgent.DeleteInventoryProductDimensionGroup(inventoryProductDimensionGroupIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<InventoryProductDimensionGroupController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<InventoryProductDimensionGroupController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}
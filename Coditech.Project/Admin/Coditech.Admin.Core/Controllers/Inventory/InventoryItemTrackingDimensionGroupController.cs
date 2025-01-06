using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class InventoryItemTrackingDimensionGroupController : BaseController
    {
        private readonly IInventoryItemTrackingDimensionGroupAgent _inventoryItemTrackingDimensionGroupAgent;
        private const string createEdit = "~/Views/Inventory/InventoryItemTrackingDimensionGroup/CreateEdit.cshtml";

        public InventoryItemTrackingDimensionGroupController(IInventoryItemTrackingDimensionGroupAgent inventoryItemTrackingDimensionGroupAgent)
        {
            _inventoryItemTrackingDimensionGroupAgent = inventoryItemTrackingDimensionGroupAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            InventoryItemTrackingDimensionGroupListViewModel list = _inventoryItemTrackingDimensionGroupAgent.GetInventoryItemTrackingDimensionGroupList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Inventory/InventoryItemTrackingDimensionGroup/_List.cshtml", list);
            }
            return View($"~/Views/Inventory/InventoryItemTrackingDimensionGroup/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            InventoryItemTrackingDimensionGroupViewModel inventoryItemTrackingDimensionGroupViewModel = _inventoryItemTrackingDimensionGroupAgent.GetInventoryItemTrackingDimensionGroup(0);
            return View(createEdit, inventoryItemTrackingDimensionGroupViewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(InventoryItemTrackingDimensionGroupViewModel inventoryItemTrackingDimensionGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                inventoryItemTrackingDimensionGroupViewModel = _inventoryItemTrackingDimensionGroupAgent.CreateInventoryItemTrackingDimensionGroup(inventoryItemTrackingDimensionGroupViewModel);
                if (!inventoryItemTrackingDimensionGroupViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(inventoryItemTrackingDimensionGroupViewModel.ErrorMessage));
            return View(createEdit, inventoryItemTrackingDimensionGroupViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int inventoryItemTrackingDimensionGroupId)
        {
            InventoryItemTrackingDimensionGroupViewModel inventoryItemTrackingDimensionGroupViewModel = _inventoryItemTrackingDimensionGroupAgent.GetInventoryItemTrackingDimensionGroup(inventoryItemTrackingDimensionGroupId);
            return ActionView(createEdit, inventoryItemTrackingDimensionGroupViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(InventoryItemTrackingDimensionGroupViewModel inventoryItemTrackingDimensionGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_inventoryItemTrackingDimensionGroupAgent.UpdateInventoryItemTrackingDimensionGroup(inventoryItemTrackingDimensionGroupViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { inventoryItemTrackingDimensionGroupId = inventoryItemTrackingDimensionGroupViewModel.InventoryItemTrackingDimensionGroupId });
            }
            return View(createEdit, inventoryItemTrackingDimensionGroupViewModel);
        }

        public virtual ActionResult Delete(string inventoryItemTrackingDimensionGroupIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(inventoryItemTrackingDimensionGroupIds))
            {
                status = _inventoryItemTrackingDimensionGroupAgent.DeleteInventoryItemTrackingDimensionGroup(inventoryItemTrackingDimensionGroupIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<InventoryItemTrackingDimensionGroupController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<InventoryItemTrackingDimensionGroupController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}
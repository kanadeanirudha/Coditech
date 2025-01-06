using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class InventoryStorageDimensionGroupController : BaseController
    {
        private readonly IInventoryStorageDimensionGroupAgent _inventoryStorageDimensionGroupAgent;
        private const string createEdit = "~/Views/Inventory/InventoryStorageDimensionGroup/CreateEdit.cshtml";

        public InventoryStorageDimensionGroupController(IInventoryStorageDimensionGroupAgent inventoryStorageDimensionGroupAgent)
        {
            _inventoryStorageDimensionGroupAgent = inventoryStorageDimensionGroupAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            InventoryStorageDimensionGroupListViewModel list = _inventoryStorageDimensionGroupAgent.GetInventoryStorageDimensionGroupList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Inventory/InventoryStorageDimensionGroup/_List.cshtml", list);
            }
            return View($"~/Views/Inventory/InventoryStorageDimensionGroup/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            InventoryStorageDimensionGroupViewModel inventoryStorageDimensionGroupViewModel = _inventoryStorageDimensionGroupAgent.GetInventoryStorageDimensionGroup(0);
            return View(createEdit, inventoryStorageDimensionGroupViewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(InventoryStorageDimensionGroupViewModel inventoryStorageDimensionGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                inventoryStorageDimensionGroupViewModel = _inventoryStorageDimensionGroupAgent.CreateInventoryStorageDimensionGroup(inventoryStorageDimensionGroupViewModel);
                if (!inventoryStorageDimensionGroupViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(inventoryStorageDimensionGroupViewModel.ErrorMessage));
            return View(createEdit, inventoryStorageDimensionGroupViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int inventoryStorageDimensionGroupId)
        {
            InventoryStorageDimensionGroupViewModel inventoryStorageDimensionGroupViewModel = _inventoryStorageDimensionGroupAgent.GetInventoryStorageDimensionGroup(inventoryStorageDimensionGroupId);
            return ActionView(createEdit, inventoryStorageDimensionGroupViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(InventoryStorageDimensionGroupViewModel inventoryStorageDimensionGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_inventoryStorageDimensionGroupAgent.UpdateInventoryStorageDimensionGroup(inventoryStorageDimensionGroupViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { inventoryStorageDimensionGroupId = inventoryStorageDimensionGroupViewModel.InventoryStorageDimensionGroupId });
            }
            return View(createEdit, inventoryStorageDimensionGroupViewModel);
        }

        public virtual ActionResult Delete(string inventoryStorageDimensionGroupIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(inventoryStorageDimensionGroupIds))
            {
                status = _inventoryStorageDimensionGroupAgent.DeleteInventoryStorageDimensionGroup(inventoryStorageDimensionGroupIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<InventoryStorageDimensionGroupController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<InventoryStorageDimensionGroupController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}
using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class InventoryItemGroupController : BaseController
    {
        private readonly IInventoryItemGroupAgent _inventoryItemGroupAgent;
        private const string createEdit = "~/Views/Inventory/InventoryItemGroup/CreateEdit.cshtml";

        public InventoryItemGroupController(IInventoryItemGroupAgent inventoryItemGroupAgent)
        {
            _inventoryItemGroupAgent = inventoryItemGroupAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            InventoryItemGroupListViewModel list = _inventoryItemGroupAgent.GetInventoryItemGroupList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Inventory/InventoryItemGroup/_List.cshtml", list);
            }
            return View($"~/Views/Inventory/InventoryItemGroup/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new InventoryItemGroupViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(InventoryItemGroupViewModel inventoryItemGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                inventoryItemGroupViewModel = _inventoryItemGroupAgent.CreateInventoryItemGroup(inventoryItemGroupViewModel);
                if (!inventoryItemGroupViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(inventoryItemGroupViewModel.ErrorMessage));
            return View(createEdit, inventoryItemGroupViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short inventoryItemGroupId)
        {
            InventoryItemGroupViewModel inventoryItemGroupViewModel = _inventoryItemGroupAgent.GetInventoryItemGroup(inventoryItemGroupId);
            return ActionView(createEdit, inventoryItemGroupViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(InventoryItemGroupViewModel inventoryItemGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_inventoryItemGroupAgent.UpdateInventoryItemGroup(inventoryItemGroupViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { inventoryItemGroupId = inventoryItemGroupViewModel.InventoryItemGroupId });
            }
            return View(createEdit, inventoryItemGroupViewModel);
        }

        public virtual ActionResult Delete(string inventoryItemGroupIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(inventoryItemGroupIds))
            {
                status = _inventoryItemGroupAgent.DeleteInventoryItemGroup(inventoryItemGroupIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<InventoryItemGroupController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<InventoryItemGroupController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}
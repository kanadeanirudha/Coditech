using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class InventoryItemModelGroupController : BaseController
    {
        private readonly IInventoryItemModelGroupAgent _inventoryItemModelGroupAgent;
        private const string createEdit = "~/Views/Inventory/InventoryItemModelGroup/CreateEdit.cshtml";

        public InventoryItemModelGroupController(IInventoryItemModelGroupAgent inventoryItemModelGroupAgent)
        {
            _inventoryItemModelGroupAgent = inventoryItemModelGroupAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            InventoryItemModelGroupListViewModel list = _inventoryItemModelGroupAgent.GetInventoryItemModelGroupList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Inventory/InventoryItemModelGroup/_List.cshtml", list);
            }
            return View($"~/Views/Inventory/InventoryItemModelGroup/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new InventoryItemModelGroupViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(InventoryItemModelGroupViewModel inventoryItemModelGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                inventoryItemModelGroupViewModel = _inventoryItemModelGroupAgent.CreateInventoryItemModelGroup(inventoryItemModelGroupViewModel);
                if (!inventoryItemModelGroupViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(inventoryItemModelGroupViewModel.ErrorMessage));
            return View(createEdit, inventoryItemModelGroupViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short inventoryItemModelGroupId)
        {
            InventoryItemModelGroupViewModel inventoryItemModelGroupViewModel = _inventoryItemModelGroupAgent.GetInventoryItemModelGroup(inventoryItemModelGroupId);
            return ActionView(createEdit, inventoryItemModelGroupViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(InventoryItemModelGroupViewModel inventoryItemModelGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_inventoryItemModelGroupAgent.UpdateInventoryItemModelGroup(inventoryItemModelGroupViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { inventoryItemModelGroupId = inventoryItemModelGroupViewModel.InventoryItemModelGroupId });
            }
            return View(createEdit, inventoryItemModelGroupViewModel);
        }

        public virtual ActionResult Delete(string inventoryItemModelGroupIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(inventoryItemModelGroupIds))
            {
                status = _inventoryItemModelGroupAgent.DeleteInventoryItemModelGroup(inventoryItemModelGroupIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<InventoryItemModelGroupController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<InventoryItemModelGroupController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}
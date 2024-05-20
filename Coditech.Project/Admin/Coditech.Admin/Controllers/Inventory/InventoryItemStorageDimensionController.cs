using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class InventoryItemStorageDimensionController : BaseController
    {
        private readonly IInventoryItemStorageDimensionAgent _InventoryItemStorageDimensionService;
        private const string createEdit = "~/Views/Inventory/InventoryItemStorageDimension/CreateEdit.cshtml";

        public InventoryItemStorageDimensionController(IInventoryItemStorageDimensionAgent inventoryItemStorageDimensionAgent)
        {
            _InventoryItemStorageDimensionService = inventoryItemStorageDimensionAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            InventoryItemStorageDimensionListViewModel list = _InventoryItemStorageDimensionService.GetInventoryItemStorageDimensionList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Inventory/InventoryItemStorageDimension/_List.cshtml", list);
            }
            return View($"~/Views/Inventory/InventoryItemStorageDimension/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new InventoryItemStorageDimensionViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(InventoryItemStorageDimensionViewModel inventoryItemStorageDimensionViewModel)
        {
            if (ModelState.IsValid)
            {
                inventoryItemStorageDimensionViewModel = _InventoryItemStorageDimensionService.CreateInventoryItemStorageDimension(inventoryItemStorageDimensionViewModel);
                if (!inventoryItemStorageDimensionViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(inventoryItemStorageDimensionViewModel.ErrorMessage));
            return View(createEdit, inventoryItemStorageDimensionViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short inventoryItemStorageDimensionId)
        {
            InventoryItemStorageDimensionViewModel inventoryItemStorageDimensionViewModel = _InventoryItemStorageDimensionService.GetInventoryItemStorageDimension(inventoryItemStorageDimensionId);
            return ActionView(createEdit, inventoryItemStorageDimensionViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(InventoryItemStorageDimensionViewModel inventoryItemStorageDimensionViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_InventoryItemStorageDimensionService.UpdateInventoryItemStorageDimension(inventoryItemStorageDimensionViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { InventoryItemStorageDimensionId = inventoryItemStorageDimensionViewModel.InventoryItemStorageDimensionId });
            }
            return View(createEdit, inventoryItemStorageDimensionViewModel);
        }

        public virtual ActionResult Delete(string inventoryItemStorageDimensionIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(inventoryItemStorageDimensionIds))
            {
                status = _InventoryItemStorageDimensionService.DeleteInventoryItemStorageDimension(inventoryItemStorageDimensionIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<InventoryItemStorageDimensionController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<InventoryItemStorageDimensionController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}
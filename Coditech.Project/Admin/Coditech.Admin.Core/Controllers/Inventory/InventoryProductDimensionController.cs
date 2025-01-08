using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class InventoryProductDimensionController : BaseController
    {
        private readonly IInventoryProductDimensionAgent _inventoryProductDimensionAgent;
        private const string createEdit = "~/Views/Inventory/InventoryProductDimension/CreateEdit.cshtml";

        public InventoryProductDimensionController(IInventoryProductDimensionAgent inventoryProductDimensionAgent)
        {
            _inventoryProductDimensionAgent = inventoryProductDimensionAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            InventoryProductDimensionListViewModel list = _inventoryProductDimensionAgent.GetInventoryProductDimensionList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Inventory/InventoryProductDimension/_List.cshtml", list);
            }
            return View($"~/Views/Inventory/InventoryProductDimension/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new InventoryProductDimensionViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(InventoryProductDimensionViewModel inventoryProductDimensionViewModel)
        {
            if (ModelState.IsValid)
            {
                inventoryProductDimensionViewModel = _inventoryProductDimensionAgent.CreateInventoryProductDimension(inventoryProductDimensionViewModel);
                if (!inventoryProductDimensionViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(inventoryProductDimensionViewModel.ErrorMessage));
            return View(createEdit, inventoryProductDimensionViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short inventoryProductDimensionId)
        {
            InventoryProductDimensionViewModel inventoryProductDimensionViewModel = _inventoryProductDimensionAgent.GetInventoryProductDimension(inventoryProductDimensionId);
            return ActionView(createEdit, inventoryProductDimensionViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(InventoryProductDimensionViewModel inventoryProductDimensionViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_inventoryProductDimensionAgent.UpdateInventoryProductDimension(inventoryProductDimensionViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { InventoryProductDimensionId = inventoryProductDimensionViewModel.InventoryProductDimensionId });
            }
            return View(createEdit, inventoryProductDimensionViewModel);
        }

        public virtual ActionResult Delete(string inventoryProductDimensionIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(inventoryProductDimensionIds))
            {
                status = _inventoryProductDimensionAgent.DeleteInventoryProductDimension(inventoryProductDimensionIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<InventoryProductDimensionController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<InventoryProductDimensionController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}

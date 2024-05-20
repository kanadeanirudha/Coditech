using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class InventoryUoMMasterController : BaseController
    {
        private readonly IInventoryUoMMasterAgent _inventoryUoMMasterAgent;
        private const string createEdit = "~/Views/Inventory/InventoryUoMMaster/CreateEdit.cshtml";

        public InventoryUoMMasterController(IInventoryUoMMasterAgent inventoryUoMMasterAgent)
        {
            _inventoryUoMMasterAgent = inventoryUoMMasterAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            InventoryUoMMasterListViewModel list = _inventoryUoMMasterAgent.GetInventoryUoMMasterList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Inventory/InventoryUoMMaster/_List.cshtml", list);
            }
            return View($"~/Views/Inventory/InventoryUoMMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new InventoryUoMMasterViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(InventoryUoMMasterViewModel inventoryUoMMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                inventoryUoMMasterViewModel = _inventoryUoMMasterAgent.CreateInventoryUoMMaster(inventoryUoMMasterViewModel);
                if (!inventoryUoMMasterViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(inventoryUoMMasterViewModel.ErrorMessage));
            return View(createEdit, inventoryUoMMasterViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short inventoryUoMMasterId)
        {
            InventoryUoMMasterViewModel inventoryUoMMasterViewModel = _inventoryUoMMasterAgent.GetInventoryUoMMaster(inventoryUoMMasterId);
            return ActionView(createEdit, inventoryUoMMasterViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(InventoryUoMMasterViewModel inventoryUoMMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_inventoryUoMMasterAgent.UpdateInventoryUoMMaster(inventoryUoMMasterViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { InventoryUoMMasterId = inventoryUoMMasterViewModel.InventoryUoMMasterId });
            }
            return View(createEdit, inventoryUoMMasterViewModel);
        }

        public virtual ActionResult Delete(string inventoryUoMMasterIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(inventoryUoMMasterIds))
            {
                status = _inventoryUoMMasterAgent.DeleteInventoryUoMMaster(inventoryUoMMasterIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<InventoryUoMMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<InventoryUoMMasterController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}

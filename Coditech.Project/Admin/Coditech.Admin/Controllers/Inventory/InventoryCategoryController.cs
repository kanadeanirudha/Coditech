using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class InventoryCategoryController : BaseController
    {
        private readonly IInventoryCategoryAgent _inventoryCategoryAgent;
        private const string createEdit = "~/Views/Inventory/InventoryCategory/CreateEdit.cshtml";

        public InventoryCategoryController(IInventoryCategoryAgent inventoryCategoryAgent)
        {
            _inventoryCategoryAgent = inventoryCategoryAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            InventoryCategoryListViewModel list = _inventoryCategoryAgent.GetInventoryCategoryList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Inventory/InventoryCategory/_List.cshtml", list);
            }
            return View($"~/Views/Inventory/InventoryCategory/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new InventoryCategoryViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(InventoryCategoryViewModel inventoryCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                inventoryCategoryViewModel = _inventoryCategoryAgent.CreateInventoryCategory(inventoryCategoryViewModel);
                if (!inventoryCategoryViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(inventoryCategoryViewModel.ErrorMessage));
            return View(createEdit, inventoryCategoryViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short inventoryCategoryId)
        {
            InventoryCategoryViewModel inventoryCategoryViewModel = _inventoryCategoryAgent.GetInventoryCategory(inventoryCategoryId);
            return ActionView(createEdit, inventoryCategoryViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(InventoryCategoryViewModel inventoryCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_inventoryCategoryAgent.UpdateInventoryCategory(inventoryCategoryViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { InventoryCategoryId = inventoryCategoryViewModel.InventoryCategoryId });
            }
            return View(createEdit, inventoryCategoryViewModel);
        }

        public virtual ActionResult Delete(string inventoryCategoryIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(inventoryCategoryIds))
            {
                status = _inventoryCategoryAgent.DeleteInventoryCategory(inventoryCategoryIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<InventoryCategoryController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<InventoryCategoryController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}
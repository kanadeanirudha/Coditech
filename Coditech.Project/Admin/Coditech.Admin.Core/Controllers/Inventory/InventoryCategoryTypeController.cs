using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class InventoryCategoryTypeController : BaseController
    {
        private readonly IInventoryCategoryTypeAgent _inventoryCategoryTypeAgent;
        private const string createEdit = "~/Views/Inventory/InventoryCategoryTypeMaster/CreateEdit.cshtml";

        public InventoryCategoryTypeController(IInventoryCategoryTypeAgent inventoryCategoryTypeAgent)
        {
            _inventoryCategoryTypeAgent = inventoryCategoryTypeAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            InventoryCategoryTypeListViewModel list = _inventoryCategoryTypeAgent.GetInventoryCategoryTypeList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Inventory/InventoryCategoryTypeMaster/_List.cshtml", list);
            }
            return View($"~/Views/Inventory/InventoryCategoryTypeMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new InventoryCategoryTypeViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(InventoryCategoryTypeViewModel inventoryCategoryTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                inventoryCategoryTypeViewModel = _inventoryCategoryTypeAgent.CreateInventoryCategoryType(inventoryCategoryTypeViewModel);
                if (!inventoryCategoryTypeViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(inventoryCategoryTypeViewModel.ErrorMessage));
            return View(createEdit, inventoryCategoryTypeViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(byte inventoryCategoryTypeMasterId)
        {
            InventoryCategoryTypeViewModel inventoryCategoryTypeViewModel = _inventoryCategoryTypeAgent.GetInventoryCategoryType(inventoryCategoryTypeMasterId);
            return ActionView(createEdit, inventoryCategoryTypeViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(InventoryCategoryTypeViewModel inventoryCategoryTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_inventoryCategoryTypeAgent.UpdateInventoryCategoryType(inventoryCategoryTypeViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { inventoryCategoryTypeMasterId = inventoryCategoryTypeViewModel.InventoryCategoryTypeMasterId });
            }
            return View(createEdit, inventoryCategoryTypeViewModel);
        }

        public virtual ActionResult Delete(string inventoryCategoryTypeMasterIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(inventoryCategoryTypeMasterIds))
            {
                status = _inventoryCategoryTypeAgent.DeleteInventoryCategoryType(inventoryCategoryTypeMasterIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<InventoryCategoryTypeController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<InventoryCategoryTypeController>(x => x.List(null));
        }
    }
}
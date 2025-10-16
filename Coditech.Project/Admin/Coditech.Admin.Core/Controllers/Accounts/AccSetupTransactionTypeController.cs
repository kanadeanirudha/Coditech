using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;
namespace Coditech.Admin.Controllers
{
    public class AccSetupTransactionTypeController : BaseController
    {
        private readonly IAccSetupTransactionTypeAgent _accSetupTransactionTypeAgent;
        private const string createEdit = "~/Views/Accounts/AccSetupTransactionType/CreateEdit.cshtml";

        public AccSetupTransactionTypeController(IAccSetupTransactionTypeAgent accSetupTransactionTypeAgent)
        {
            _accSetupTransactionTypeAgent = accSetupTransactionTypeAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            AccSetupTransactionTypeListViewModel list = _accSetupTransactionTypeAgent.GetTransactionTypeList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Accounts/AccSetupTransactionType/_List.cshtml", list);
            }
            return View($"~/Views/Accounts/AccSetupTransactionType/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new AccSetupTransactionTypeViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(AccSetupTransactionTypeViewModel accSetupTransactionTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                accSetupTransactionTypeViewModel = _accSetupTransactionTypeAgent.CreateTransactionType(accSetupTransactionTypeViewModel);
                if (!accSetupTransactionTypeViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    if (string.Equals(accSetupTransactionTypeViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { accSetupTransactionTypeId = accSetupTransactionTypeViewModel.AccSetupTransactionTypeId });
                    }
                    else if (string.Equals(accSetupTransactionTypeViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToList);
                    }
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(accSetupTransactionTypeViewModel.ErrorMessage));
            return View(createEdit, accSetupTransactionTypeViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short accSetupTransactionTypeId)
        {
            AccSetupTransactionTypeViewModel accSetupTransactionTypeViewModel = _accSetupTransactionTypeAgent.GetTransactionType(accSetupTransactionTypeId);
            return ActionView(createEdit, accSetupTransactionTypeViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(AccSetupTransactionTypeViewModel accSetupTransactionTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_accSetupTransactionTypeAgent.UpdateTransactionType(accSetupTransactionTypeViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(accSetupTransactionTypeViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { accSetupTransactionTypeId = accSetupTransactionTypeViewModel.AccSetupTransactionTypeId });
                }
                else if (string.Equals(accSetupTransactionTypeViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(createEdit, accSetupTransactionTypeViewModel);
        }

        public virtual ActionResult Delete(string AccSetupTransactionTypeIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(AccSetupTransactionTypeIds))
            {
                status = _accSetupTransactionTypeAgent.DeleteTransactionType(AccSetupTransactionTypeIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<AccSetupTransactionTypeController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<AccSetupTransactionTypeController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}
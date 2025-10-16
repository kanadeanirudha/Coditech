using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coditech.Admin.Controllers
{
    public class GeneralTaxGroupMasterController : BaseController
    {
        IGeneralTaxGroupAgent _generalTaxGroupAgent;
        IGeneralTaxMasterAgent _generalTaxMasterAgent = null;
        private const string createEdit = "~/Views/GeneralMaster/GeneralTaxGroupMaster/CreateEdit.cshtml";

        public GeneralTaxGroupMasterController(IGeneralTaxGroupAgent generalTaxGroupAgent, IGeneralTaxMasterAgent generalTaxMasterAgent)
        {
            _generalTaxGroupAgent = generalTaxGroupAgent;
            _generalTaxMasterAgent= generalTaxMasterAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralTaxGroupMasterListViewModel list = _generalTaxGroupAgent.GetTaxGroupMasterList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralTaxGroupMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralTaxGroupMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {

            GeneralTaxGroupMasterViewModel taxGroupMasterViewModel = new GeneralTaxGroupMasterViewModel();
            BindDropDown(taxGroupMasterViewModel);
            return View(createEdit, taxGroupMasterViewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralTaxGroupMasterViewModel generalTaxGroupMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                generalTaxGroupMasterViewModel = _generalTaxGroupAgent.CreateTaxGroupMaster(generalTaxGroupMasterViewModel);
                if (!generalTaxGroupMasterViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    if (string.Equals(generalTaxGroupMasterViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { taxGroupMasterId = generalTaxGroupMasterViewModel.GeneralTaxGroupMasterId });
                    }
                    else if (string.Equals(generalTaxGroupMasterViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToList);
                    }
                }
            }
             BindDropDown(generalTaxGroupMasterViewModel);
            SetNotificationMessage(GetErrorNotificationMessage(generalTaxGroupMasterViewModel.ErrorMessage));
            return View(createEdit, generalTaxGroupMasterViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short taxGroupMasterId)
        {
            GeneralTaxGroupMasterViewModel generalTaxGroupMasterViewModel = _generalTaxGroupAgent.GetTaxGroupMaster(taxGroupMasterId);
             BindDropDown(generalTaxGroupMasterViewModel);
            return ActionView(createEdit, generalTaxGroupMasterViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralTaxGroupMasterViewModel generalTaxGroupMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalTaxGroupAgent.UpdateTaxGroupMaster(generalTaxGroupMasterViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(generalTaxGroupMasterViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { taxGroupMasterId = generalTaxGroupMasterViewModel.GeneralTaxGroupMasterId });
                }
                else if (string.Equals(generalTaxGroupMasterViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            BindDropDown(generalTaxGroupMasterViewModel);
            return View(createEdit, generalTaxGroupMasterViewModel);
        }

        public virtual ActionResult Delete(string taxGroupMasterIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(taxGroupMasterIds))
            {
                status = _generalTaxGroupAgent.DeleteTaxGroupMaster(taxGroupMasterIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralTaxGroupMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralTaxGroupMasterController>(x => x.List(null));
        }
        #region Private
        protected virtual void BindDropDown(GeneralTaxGroupMasterViewModel taxGroupMasterViewModel)
        {
            taxGroupMasterViewModel.AllTaxList = _generalTaxMasterAgent.GetAllTaxList().GeneralTaxMasterList;
        }
        #endregion
    }
}
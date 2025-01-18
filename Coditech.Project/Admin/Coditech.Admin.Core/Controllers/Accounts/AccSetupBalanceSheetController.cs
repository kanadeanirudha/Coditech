using System.Collections.Generic;
using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class AccSetupBalanceSheetController : BaseController
    {
        private readonly IAccSetupBalanceSheetAgent _accSetupBalanceSheetAgent;
        private const string createEdit = "~/Views/Accounts/AccSetupBalanceSheet/CreateEdit.cshtml";

        public AccSetupBalanceSheetController(IAccSetupBalanceSheetAgent accSetupBalanceSheetAgent)
        {
            _accSetupBalanceSheetAgent = accSetupBalanceSheetAgent;
        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            AccSetupBalanceSheetListViewModel list = new AccSetupBalanceSheetListViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && !string.IsNullOrEmpty(dataTableModel.SelectedParameter1))
            {
                list = _accSetupBalanceSheetAgent.GetBalanceSheetList(dataTableModel, Convert.ToByte(dataTableModel.SelectedParameter1));
            }
            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;
            list.SelectedParameter1 = dataTableModel.SelectedParameter1;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Accounts/AccSetupBalanceSheet/_List.cshtml", list);
            }
            return View($"~/Views/Accounts/AccSetupBalanceSheet/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new AccSetupBalanceSheetViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(AccSetupBalanceSheetViewModel accSetupBalanceSheetViewModel)
        {
            if (ModelState.IsValid)
            {
                accSetupBalanceSheetViewModel = _accSetupBalanceSheetAgent.CreateBalanceSheet(accSetupBalanceSheetViewModel);
                if (!accSetupBalanceSheetViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<AccSetupBalanceSheetController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(accSetupBalanceSheetViewModel.ErrorMessage));
            return View(createEdit, accSetupBalanceSheetViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int balanceSheetId)
        {
            AccSetupBalanceSheetViewModel accSetupBalanceSheetViewModel = _accSetupBalanceSheetAgent.GetBalanceSheet(balanceSheetId);
            return ActionView(createEdit, accSetupBalanceSheetViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(AccSetupBalanceSheetViewModel accSetupBalanceSheetViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_accSetupBalanceSheetAgent.UpdateBalanceSheet(accSetupBalanceSheetViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { balanceSheetId = accSetupBalanceSheetViewModel.AccSetupBalanceSheetId });
            }
            return View(createEdit, accSetupBalanceSheetViewModel);
        }
        public virtual ActionResult Delete(string balanceSheetIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(balanceSheetIds))
            {
                status = _accSetupBalanceSheetAgent.DeleteBalanceSheet(balanceSheetIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<AccSetupBalanceSheetController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<AccSetupBalanceSheetController>(x => x.List(null));
        }
        #region Protected

        #endregion
    }
}
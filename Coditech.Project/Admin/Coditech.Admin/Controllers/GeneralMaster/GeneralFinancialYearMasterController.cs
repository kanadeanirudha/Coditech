using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralFinancialYearMasterController : BaseController
    {
        private readonly IGeneralFinancialYearAgent _generalFinancialYearAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralFinancialYearMaster/CreateEdit.cshtml";

        public GeneralFinancialYearMasterController(IGeneralFinancialYearAgent generalFinancialYearAgent)
        {
            _generalFinancialYearAgent = generalFinancialYearAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralFinancialYearListViewModel list = _generalFinancialYearAgent.GetFinancialYearList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralFinancialYearMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralFinancialYearMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralFinancialYearViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralFinancialYearViewModel generalFinancialYearViewModel)
        {
            if (ModelState.IsValid)
            {
                generalFinancialYearViewModel = _generalFinancialYearAgent.CreateFinancialYear(generalFinancialYearViewModel);
                if (!generalFinancialYearViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GeneralFinancialYearMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalFinancialYearViewModel.ErrorMessage));
            return View(createEdit, generalFinancialYearViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short generalFinancialYearId)
        {
            GeneralFinancialYearViewModel generalFinancialYearViewModel = _generalFinancialYearAgent.GetFinancialYear(generalFinancialYearId);
            return ActionView(createEdit, generalFinancialYearViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralFinancialYearViewModel generalFinancialYearViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalFinancialYearAgent.UpdateFinancialYear(generalFinancialYearViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { generalFinancialYearId = generalFinancialYearViewModel.GeneralFinancialYearMasterId });
            }
            return View(createEdit, generalFinancialYearViewModel);
        }

        public virtual ActionResult Delete(string FinancialYearIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(FinancialYearIds))
            {
                status = _generalFinancialYearAgent.DeleteFinancialYear(FinancialYearIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralFinancialYearMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralFinancialYearMasterController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}